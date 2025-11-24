using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGomProject
{
    public partial class AIChatForm : Form
    {
        private static readonly string OPENROUTER_API_KEY = "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";
        private int memberId;

        public AIChatForm(int memberId)
        {
            this.memberId = memberId;
            InitializeComponent();

            rtbAIChat.AppendText("🤖 AI 어시스턴트에 오신 걸 환영합니다.\n도서 추천이나 정보가 필요하시면 물어보세요!\n\n");
        }

        // 🔹 Enter 키 입력 시 전송
        private async void rtbUserChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                string userMessage = rtbUserChat.Text.Trim();

                if (string.IsNullOrWhiteSpace(userMessage))
                    return;

                AppendUserMessage(userMessage);
                rtbUserChat.Clear();

                string aiResponse = await GenerateRAGResponse(userMessage);
                aiResponse = CleanResponse(aiResponse);
                AppendAIMessage(aiResponse);
            }
        }

        private void AppendUserMessage(string message)
        {
            rtbAIChat.SelectionColor = System.Drawing.Color.Blue;
            rtbAIChat.AppendText($"👤 사용자: {message}\n");
            rtbAIChat.SelectionColor = System.Drawing.Color.Black;
        }

        private void AppendAIMessage(string message)
        {
            rtbAIChat.SelectionColor = System.Drawing.Color.DarkGreen;
            rtbAIChat.AppendText($"🤖 AI: {message}\n\n");
            rtbAIChat.SelectionColor = System.Drawing.Color.Black;
            rtbAIChat.ScrollToCaret();
        }

        // 🧹 비정상 문자 제거
        private string CleanResponse(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            text = Regex.Replace(text, @"[^\uAC00-\uD7A3\u3131-\u318E\u1100-\u11FFa-zA-Z0-9\s.,!?\""\-()']", "");
            return text.Trim();
        }

        // ✅ RAG 핵심
        private async Task<string> GenerateRAGResponse(string userQuestion)
        {
            try
            {
                string keyword = ExtractKeyword(userQuestion);
                var relatedBooks = SearchBooksFromDB(keyword, memberId);
                string prompt = BuildPrompt(userQuestion, relatedBooks);

                string aiAnswer = await QueryOpenRouter(prompt);
                return aiAnswer;
            }
            catch (Exception ex)
            {
                return $"⚠️ 오류 발생: {ex.Message}";
            }
        }

        // 🔎 불용어 제거
        private string ExtractKeyword(string input)
        {
            string[] stopWords = { "작가의", "저자의", "책", "도서", "알려줘", "찾아", "찾고", "주세요", "좀", "무엇", "어떤", "은", "는", "이", "가", "의", "에", "에서", "을", "를" };
            foreach (var word in stopWords)
                input = Regex.Replace(input, Regex.Escape(word), "", RegexOptions.IgnoreCase);

            input = Regex.Replace(input, @"\s{2,}", " ").Trim();
            return input;
        }

        // ✅ 전역/개인화 모드 분리된 도서 검색
        private List<BookInfo> SearchBooksFromDB(string keyword, int memberId)
        {
            var list = new List<BookInfo>();
            bool hasKeyword = !string.IsNullOrWhiteSpace(keyword);

            string sql;

            if (hasKeyword)
            {
                // 🔍 전역 검색 모드
                sql = @"
SELECT TOP 20
    b.title,
    b.author,
    ISNULL(c.category_name, N'미분류') AS category_name,
    ISNULL(bd.description, N'')       AS description
FROM Books b
LEFT JOIN Categories  c  ON b.category_id = c.category_id
LEFT JOIN BookDetails bd ON b.isbn       = bd.isbn
WHERE
    b.title       LIKE N'%' + @kw + N'%' OR
    b.author      LIKE N'%' + @kw + N'%' OR
    bd.description LIKE N'%' + @kw + N'%'
ORDER BY
    CASE WHEN b.isbn IN (
        SELECT lc.isbn
        FROM Borrows br
        JOIN LibraryCollections lc ON br.library_collection_id = lc.library_collection_id
        WHERE br.member_id = @memberId
    ) THEN 0 ELSE 1 END,
    b.title;
";
            }
            else
            {
                // 👤 개인화 모드
                sql = @"
SELECT TOP 10
    b.title,
    b.author,
    ISNULL(c.category_name, N'미분류') AS category_name,
    ISNULL(bd.description, N'')       AS description
FROM Books b
JOIN LibraryCollections lc ON lc.isbn = b.isbn
JOIN Borrows br ON br.library_collection_id = lc.library_collection_id AND br.member_id = @memberId
LEFT JOIN Categories  c  ON b.category_id = c.category_id
LEFT JOIN BookDetails bd ON b.isbn       = bd.isbn
ORDER BY b.title;
";
            }

            using (var conn = new SqlConnection(DatabaseConfig.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (hasKeyword)
                    cmd.Parameters.Add("@kw", System.Data.SqlDbType.NVarChar).Value = keyword;

                cmd.Parameters.AddWithValue("@memberId", memberId);

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new BookInfo
                        {
                            Title = r["title"].ToString(),
                            Author = r["author"].ToString(),
                            Category = r["category_name"].ToString(),
                            Description = r["description"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // ✅ 프롬프트 생성
        private string BuildPrompt(string question, List<BookInfo> books)
        {
            var sb = new StringBuilder();
            sb.AppendLine("아래는 데이터베이스에서 조회된 도서 목록입니다.\n");

            if (books.Count == 0)
            {
                sb.AppendLine("⚠️ (검색 결과가 없습니다)");
                sb.AppendLine("따라서 사용자의 질문에는 '검색 결과가 없습니다.' 라고만 대답하세요.");
            }
            else
            {
                foreach (var b in books)
                {
                    sb.AppendLine($"제목: {b.Title}");
                    sb.AppendLine($"저자: {b.Author}");
                    sb.AppendLine($"장르: {b.Category}");
                    sb.AppendLine($"설명: {b.Description}\n");
                }
            }

            sb.AppendLine($"\n사용자 질문: {question}");
            return sb.ToString();
        }


        // ✅ OpenRouter API 호출
        private async Task<string> QueryOpenRouter(string prompt)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + OPENROUTER_API_KEY);
                    client.DefaultRequestHeaders.Add("HTTP-Referer", "https://github.com/minhyeok-AGom");
                    client.DefaultRequestHeaders.Add("X-Title", "AGom Library RAG Chatbot");

                    var payload = new
                    {
                        model = "meta-llama/llama-3.3-8b-instruct:free",
                        messages = new[]
                        {
                            new {
  role = "system",
  content =
  "당신은 AGom 도서관리 시스템의 AI 사서입니다. 아래에 제시된 도서 목록은 데이터베이스에서 직접 조회된 결과입니다.  반드시 이 목록 안의 도서 정보만 근거로 대답해야 하며,  목록에 없는 도서, 작가, 또는 외부 지식(인터넷, 일반 상식 등)은 절대 말하지 마세요.  목록에 아무것도 없다면, '검색 결과가 없습니다.' 라고만 말하세요. 모든 답변은 자연스럽고 공손한 한국어로 작성하세요."
},

                            new { role = "user", content = prompt }
                        },
                        max_tokens = 600,
                        temperature = 0.3
                    };

                    var body = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://openrouter.ai/api/v1/chat/completions", body);
                    string result = await response.Content.ReadAsStringAsync();

                    dynamic data = JsonConvert.DeserializeObject(result);
                    string message = data?.choices?[0]?.message?.content?.ToString();

                    if (string.IsNullOrEmpty(message))
                        message = "(응답 파싱 실패)\n\n" + result;

                    return message.Trim();
                }
            }
            catch (Exception ex)
            {
                return $"⚠️ API 요청 중 오류 발생: {ex.Message}";
            }
        }
    }

    // 📘 BookInfo 클래스
    public class BookInfo
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
