namespace AGomProject
{
    partial class AIChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbAIChat = new System.Windows.Forms.RichTextBox();
            this.pbBookImage = new System.Windows.Forms.PictureBox();
            this.pbAILOGO = new System.Windows.Forms.PictureBox();
            this.rtbUserChat = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbBookImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAILOGO)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbAIChat
            // 
            this.rtbAIChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbAIChat.Location = new System.Drawing.Point(211, 12);
            this.rtbAIChat.Name = "rtbAIChat";
            this.rtbAIChat.ReadOnly = true;
            this.rtbAIChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbAIChat.Size = new System.Drawing.Size(500, 230);
            this.rtbAIChat.TabIndex = 57;
            this.rtbAIChat.Text = "";
            // 
            // pbBookImage
            // 
            this.pbBookImage.BackColor = System.Drawing.Color.White;
            this.pbBookImage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pbBookImage.Location = new System.Drawing.Point(12, 12);
            this.pbBookImage.Name = "pbBookImage";
            this.pbBookImage.Size = new System.Drawing.Size(165, 230);
            this.pbBookImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBookImage.TabIndex = 56;
            this.pbBookImage.TabStop = false;
            // 
            // pbAILOGO
            // 
            this.pbAILOGO.Image = global::AGomProject.Properties.Resources.AIChat1;
            this.pbAILOGO.Location = new System.Drawing.Point(734, 41);
            this.pbAILOGO.Name = "pbAILOGO";
            this.pbAILOGO.Size = new System.Drawing.Size(138, 162);
            this.pbAILOGO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAILOGO.TabIndex = 55;
            this.pbAILOGO.TabStop = false;
            // 
            // rtbUserChat
            // 
            this.rtbUserChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbUserChat.Location = new System.Drawing.Point(211, 314);
            this.rtbUserChat.Name = "rtbUserChat";
            this.rtbUserChat.Size = new System.Drawing.Size(500, 214);
            this.rtbUserChat.TabIndex = 58;
            this.rtbUserChat.Text = "";
            this.rtbUserChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbUserChat_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(717, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 51);
            this.button1.TabIndex = 59;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // AIChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtbUserChat);
            this.Controls.Add(this.pbBookImage);
            this.Controls.Add(this.pbAILOGO);
            this.Controls.Add(this.rtbAIChat);
            this.Name = "AIChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AIChat";
            ((System.ComponentModel.ISupportInitialize)(this.pbBookImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAILOGO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbAILOGO;
        private System.Windows.Forms.PictureBox pbBookImage;
        private System.Windows.Forms.RichTextBox rtbAIChat;
        private System.Windows.Forms.RichTextBox rtbUserChat;
        private System.Windows.Forms.Button button1;
    }
}