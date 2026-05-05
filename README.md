빠른 요약
만든 이유 : 요즘 애들이 책을 안 읽음
3인 프로젝트
C# + WINFORM + MicrosftSQL 기반 + LLM
극한의 데이터베이스 운용
회원가입, 도서 검색부터 도서 입력까지
API로 지도에서 도서관을 찾아 오프라인 도서관 예약
RAG 기반 챗봇 (사서) 운용중. 입력된 데이터 베이스 기반이라 아는 것만 답함. 


🛠 기술 스택 (Tech Stack)
Language & Framework: C#, Winform (.NET Framework)
Database: Microsoft SQL Server
AI & API: RAG (Retrieval-Augmented Generation), OpenRouter API (LLM)
IDE: Visual Studio 2022
----------------------------------------------------------------------------------------------------------------------------------
🐻 AGOM Libraries: Winform & SQL 기반 도서관 통합 관리 및 AI 사서 시스템
📖 프로젝트 개요 (About the Project)
AGOM Libraries는 현대 사회의 낮은 독서율 문제와 온라인 정보 접근 요구를 충족하기 위해 개발된 윈폼(Winform) 기반의 도서 관리 시스템입니다. 
관리와 사용자 기능(전자책, 예약 등)이 하나의 프로그램으로 통합된 올인원(All-in-One) 관리 프로그램으로 설계되었습니다. MS SQL의 기능을 극한으로 활용하여 안정적인 데이터베이스를 구축하였으며 RAG 기반의 AI 챗봇을 도입하여 사용자 편의성을 크게 증대시켰습니다.

개발 기간: 2024.08.20 ~ 2024.12.11
팀 규모: 3인 팀 프로젝트
🧑‍💻 나의 역할 (My Contribution)
이승호 (Backend & Database Developer) 안드로이드 및 지도 API, 전체 UI 디자인을 제외한 시스템의 백엔드 아키텍처 및 데이터베이스 통신을 총괄했습니다.
데이터베이스 아키텍처 설계: MS SQL을 활용하여 스키마 설계부터 회원 관리, 도서 관리 테이블 구축 전담.
핵심 백엔드 로직 구현: 회원 정보 관리, 오프라인 도서 대여 및 반납 로직, 데이터 무결성 보장 처리.
온라인 리딩(E-book) 시스템 구축: 도서의 이미지 및 전자책 파일 경로를 안전하게 관리하고 뷰어로 연결하는 로직 구현.
RAG 기반 AI 사서 파이프라인 개발: 팀원과 2인 1조로 협업하여 도서관 DB 정보를 기반으로 답변하는 지능형 LLM 챗봇의 검색-증강-생성 로직 공동 구현.

✨ 주요 기능 및 특징 (Key Features)
1. 올인원 통합 관리 (All-in-One Management)
사용자를 위한 별도의 클라이언트 프로그램을 설치할 필요 없이 이 프로그램 하나로 관리자 모드의 통계/재고 관리부터 일반 사용자의 도서 검색, 장바구니 기반 오프라인 예약, 그리고 E-Book 온라인 책 읽기까지 모두 가능하도록 구현했습니다.
2. 데이터 무결성 확보를 위한 트랜잭션(Roll-Back) 관리
도서 등록 시 Books, BookDetails, LibraryCollections 등 여러 테이블에 다단계로 데이터를 삽입합니다.
이 과정에서 SqlTransaction을 적용하여, 중간에 예외(ISBN 중복 등)가 발생하면 즉시 전체 작업을 취소(Rollback)하는 '모두 성공 또는 모두 실패(Atomicity)' 원칙을 확립했습니다.
3. RAG 기반 지능형 AI 사서 (LLM)
단순한 텍스트 검색을 넘어 OpenRouter API(Llama-3.3-8b)를 연동한 챗봇을 도입했습니다.
사용자의 질문에서 불용어를 제거하고 핵심 키워드를 추출하여 도서관 DB를 조회(Retrieval)한 후 그 결과를 프롬프트에 포함(Augmentation)해 환각 현상 없는 정확한 도서 추천 및 정보를 제공합니다.
4. 선행적 데이터베이스 초기화
프로그램 최초 실행 시 CreateDatabaseIfNotExists() 메서드를 호출하여 로컬 환경에 데이터베이스와 필수 테이블, 초기 관리자 계정이 존재하지 않으면 자동으로 생성합니다.


🚀 실행 가이드 (Getting Started)
DB 환경: 로컬 PC에 Microsoft SQL Server가 설치되어 있어야 합니다.
자동 초기화: 프로그램을 처음 실행하면 CreateDatabaseIfNotExists() 로직에 의해 AGOMDB 및 필수 테이블이 자동 생성됩니다.
AI 사서 연동: RAG 기반 챗봇 기능을 정상적으로 사용하려면 소스 코드 내에 유효한 OpenRouter API Key 설정이 필요합니다.
