namespace Desktop_App.Forms
{
    partial class ChildFormBooks
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
            btnCreateBook = new Button();
            tbxTitle = new TextBox();
            tbxDescription = new TextBox();
            tbxNoOfPages = new TextBox();
            tbxPublisher = new TextBox();
            dtpPubDate = new DateTimePicker();
            tbxLanguage = new TextBox();
            tbxISBN = new TextBox();
            SuspendLayout();
            // 
            // btnCreateBook
            // 
            btnCreateBook.AutoSize = true;
            btnCreateBook.Location = new Point(484, 591);
            btnCreateBook.Name = "btnCreateBook";
            btnCreateBook.Size = new Size(90, 25);
            btnCreateBook.TabIndex = 0;
            btnCreateBook.Text = "Create a book";
            btnCreateBook.UseVisualStyleBackColor = true;
            btnCreateBook.Click += btnCreateBook_Click;
            // 
            // tbxTitle
            // 
            tbxTitle.Location = new Point(125, 95);
            tbxTitle.Name = "tbxTitle";
            tbxTitle.PlaceholderText = "Book title";
            tbxTitle.Size = new Size(233, 23);
            tbxTitle.TabIndex = 1;
            // 
            // tbxDescription
            // 
            tbxDescription.Location = new Point(125, 124);
            tbxDescription.Multiline = true;
            tbxDescription.Name = "tbxDescription";
            tbxDescription.PlaceholderText = "Book description";
            tbxDescription.Size = new Size(715, 235);
            tbxDescription.TabIndex = 2;
            // 
            // tbxNoOfPages
            // 
            tbxNoOfPages.Location = new Point(125, 376);
            tbxNoOfPages.Name = "tbxNoOfPages";
            tbxNoOfPages.PlaceholderText = "Number of pages";
            tbxNoOfPages.Size = new Size(100, 23);
            tbxNoOfPages.TabIndex = 3;
            // 
            // tbxPublisher
            // 
            tbxPublisher.Location = new Point(125, 405);
            tbxPublisher.Name = "tbxPublisher";
            tbxPublisher.PlaceholderText = "Publisher";
            tbxPublisher.Size = new Size(100, 23);
            tbxPublisher.TabIndex = 4;
            // 
            // dtpPubDate
            // 
            dtpPubDate.Location = new Point(125, 434);
            dtpPubDate.Name = "dtpPubDate";
            dtpPubDate.Size = new Size(213, 23);
            dtpPubDate.TabIndex = 5;
            // 
            // tbxLanguage
            // 
            tbxLanguage.Location = new Point(125, 463);
            tbxLanguage.Name = "tbxLanguage";
            tbxLanguage.PlaceholderText = "Language";
            tbxLanguage.Size = new Size(213, 23);
            tbxLanguage.TabIndex = 6;
            // 
            // tbxISBN
            // 
            tbxISBN.Location = new Point(316, 376);
            tbxISBN.MaxLength = 13;
            tbxISBN.Name = "tbxISBN";
            tbxISBN.PlaceholderText = "ISBN (13 digits)";
            tbxISBN.Size = new Size(182, 23);
            tbxISBN.TabIndex = 7;
            // 
            // childFormBook
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 670);
            Controls.Add(tbxISBN);
            Controls.Add(tbxLanguage);
            Controls.Add(dtpPubDate);
            Controls.Add(tbxPublisher);
            Controls.Add(tbxNoOfPages);
            Controls.Add(tbxDescription);
            Controls.Add(tbxTitle);
            Controls.Add(btnCreateBook);
            FormBorderStyle = FormBorderStyle.None;
            Name = "childFormBook";
            Text = "childFormBook";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCreateBook;
        private TextBox tbxTitle;
        private TextBox tbxDescription;
        private TextBox tbxNoOfPages;
        private TextBox tbxPublisher;
        private DateTimePicker dtpPubDate;
        private TextBox tbxLanguage;
        private TextBox tbxISBN;
    }
}