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
            tabControlBooks = new TabControl();
            tabBookInfo = new TabPage();
            dgvBooks = new DataGridView();
            tabAddBook = new TabPage();
            label8 = new Label();
            cbbxBookFormat = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lbxGenres = new CheckedListBox();
            lbxAuthors = new CheckedListBox();
            tbxDescription = new TextBox();
            tbxISBN = new TextBox();
            btnCreateBook = new Button();
            tbxLanguage = new TextBox();
            tbxTitle = new TextBox();
            dtpPubDate = new DateTimePicker();
            tbxPageCount = new TextBox();
            tbxPublisher = new TextBox();
            tabAddGenre = new TabPage();
            label16 = new Label();
            btnAddGenre = new Button();
            tbxGenre = new TextBox();
            dgvGenres = new DataGridView();
            tabControlBooks.SuspendLayout();
            tabBookInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            tabAddBook.SuspendLayout();
            tabAddGenre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGenres).BeginInit();
            SuspendLayout();
            // 
            // tabControlBooks
            // 
            tabControlBooks.Controls.Add(tabBookInfo);
            tabControlBooks.Controls.Add(tabAddBook);
            tabControlBooks.Controls.Add(tabAddGenre);
            tabControlBooks.Location = new Point(12, 12);
            tabControlBooks.Name = "tabControlBooks";
            tabControlBooks.SelectedIndex = 0;
            tabControlBooks.Size = new Size(1056, 646);
            tabControlBooks.TabIndex = 8;
            // 
            // tabBookInfo
            // 
            tabBookInfo.Controls.Add(dgvBooks);
            tabBookInfo.Location = new Point(4, 24);
            tabBookInfo.Name = "tabBookInfo";
            tabBookInfo.Padding = new Padding(3);
            tabBookInfo.Size = new Size(1048, 618);
            tabBookInfo.TabIndex = 0;
            tabBookInfo.Text = "All books info";
            tabBookInfo.UseVisualStyleBackColor = true;
            // 
            // dgvBooks
            // 
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Location = new Point(6, 6);
            dgvBooks.MultiSelect = false;
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(1036, 606);
            dgvBooks.TabIndex = 0;
            // 
            // tabAddBook
            // 
            tabAddBook.Controls.Add(label8);
            tabAddBook.Controls.Add(cbbxBookFormat);
            tabAddBook.Controls.Add(label7);
            tabAddBook.Controls.Add(label6);
            tabAddBook.Controls.Add(label5);
            tabAddBook.Controls.Add(label4);
            tabAddBook.Controls.Add(label3);
            tabAddBook.Controls.Add(label2);
            tabAddBook.Controls.Add(label1);
            tabAddBook.Controls.Add(lbxGenres);
            tabAddBook.Controls.Add(lbxAuthors);
            tabAddBook.Controls.Add(tbxDescription);
            tabAddBook.Controls.Add(tbxISBN);
            tabAddBook.Controls.Add(btnCreateBook);
            tabAddBook.Controls.Add(tbxLanguage);
            tabAddBook.Controls.Add(tbxTitle);
            tabAddBook.Controls.Add(dtpPubDate);
            tabAddBook.Controls.Add(tbxPageCount);
            tabAddBook.Controls.Add(tbxPublisher);
            tabAddBook.Location = new Point(4, 24);
            tabAddBook.Name = "tabAddBook";
            tabAddBook.Padding = new Padding(3);
            tabAddBook.Size = new Size(1048, 618);
            tabAddBook.TabIndex = 1;
            tabAddBook.Text = "Add a book";
            tabAddBook.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(66, 461);
            label8.Name = "label8";
            label8.Size = new Size(45, 15);
            label8.TabIndex = 28;
            label8.Text = "Format";
            // 
            // cbbxBookFormat
            // 
            cbbxBookFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbxBookFormat.FormattingEnabled = true;
            cbbxBookFormat.Location = new Point(128, 458);
            cbbxBookFormat.Name = "cbbxBookFormat";
            cbbxBookFormat.Size = new Size(219, 23);
            cbbxBookFormat.TabIndex = 27;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(79, 419);
            label7.Name = "label7";
            label7.Size = new Size(32, 15);
            label7.TabIndex = 26;
            label7.Text = "ISBN";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(52, 374);
            label6.Name = "label6";
            label6.Size = new Size(59, 15);
            label6.TabIndex = 25;
            label6.Text = "Language";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(33, 337);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 24;
            label5.Text = "Publishing date";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(55, 297);
            label4.Name = "label4";
            label4.Size = new Size(56, 15);
            label4.TabIndex = 23;
            label4.Text = "Publisher";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 252);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 22;
            label3.Text = "Page count";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 86);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 21;
            label2.Text = "Description";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(82, 44);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 20;
            label1.Text = "Title";
            // 
            // lbxGenres
            // 
            lbxGenres.FormattingEnabled = true;
            lbxGenres.Location = new Point(837, 41);
            lbxGenres.Name = "lbxGenres";
            lbxGenres.Size = new Size(189, 292);
            lbxGenres.TabIndex = 19;
            // 
            // lbxAuthors
            // 
            lbxAuthors.FormattingEnabled = true;
            lbxAuthors.Location = new Point(581, 41);
            lbxAuthors.Name = "lbxAuthors";
            lbxAuthors.Size = new Size(250, 292);
            lbxAuthors.TabIndex = 18;
            // 
            // tbxDescription
            // 
            tbxDescription.Location = new Point(128, 83);
            tbxDescription.Multiline = true;
            tbxDescription.Name = "tbxDescription";
            tbxDescription.PlaceholderText = "Book description";
            tbxDescription.Size = new Size(435, 143);
            tbxDescription.TabIndex = 10;
            // 
            // tbxISBN
            // 
            tbxISBN.Location = new Point(128, 416);
            tbxISBN.MaxLength = 13;
            tbxISBN.Name = "tbxISBN";
            tbxISBN.PlaceholderText = "ISBN (13 digits)";
            tbxISBN.Size = new Size(213, 23);
            tbxISBN.TabIndex = 15;
            // 
            // btnCreateBook
            // 
            btnCreateBook.AutoSize = true;
            btnCreateBook.Location = new Point(496, 439);
            btnCreateBook.Name = "btnCreateBook";
            btnCreateBook.Size = new Size(130, 59);
            btnCreateBook.TabIndex = 8;
            btnCreateBook.Text = "Create a book";
            btnCreateBook.UseVisualStyleBackColor = true;
            btnCreateBook.Click += btnCreateBook_Click;
            // 
            // tbxLanguage
            // 
            tbxLanguage.Location = new Point(128, 371);
            tbxLanguage.Name = "tbxLanguage";
            tbxLanguage.PlaceholderText = "Language";
            tbxLanguage.Size = new Size(213, 23);
            tbxLanguage.TabIndex = 14;
            // 
            // tbxTitle
            // 
            tbxTitle.Location = new Point(128, 41);
            tbxTitle.Name = "tbxTitle";
            tbxTitle.PlaceholderText = "Book title";
            tbxTitle.Size = new Size(435, 23);
            tbxTitle.TabIndex = 9;
            // 
            // dtpPubDate
            // 
            dtpPubDate.Location = new Point(128, 331);
            dtpPubDate.Name = "dtpPubDate";
            dtpPubDate.Size = new Size(213, 23);
            dtpPubDate.TabIndex = 13;
            // 
            // tbxPageCount
            // 
            tbxPageCount.Location = new Point(128, 249);
            tbxPageCount.Name = "tbxPageCount";
            tbxPageCount.PlaceholderText = "Number of pages";
            tbxPageCount.Size = new Size(110, 23);
            tbxPageCount.TabIndex = 11;
            // 
            // tbxPublisher
            // 
            tbxPublisher.Location = new Point(128, 294);
            tbxPublisher.Name = "tbxPublisher";
            tbxPublisher.PlaceholderText = "Publisher";
            tbxPublisher.Size = new Size(213, 23);
            tbxPublisher.TabIndex = 12;
            // 
            // tabAddGenre
            // 
            tabAddGenre.Controls.Add(dgvGenres);
            tabAddGenre.Controls.Add(label16);
            tabAddGenre.Controls.Add(btnAddGenre);
            tabAddGenre.Controls.Add(tbxGenre);
            tabAddGenre.Location = new Point(4, 24);
            tabAddGenre.Name = "tabAddGenre";
            tabAddGenre.Padding = new Padding(3);
            tabAddGenre.Size = new Size(1048, 618);
            tabAddGenre.TabIndex = 2;
            tabAddGenre.Text = "Add a genre";
            tabAddGenre.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(722, 323);
            label16.Name = "label16";
            label16.Size = new Size(71, 15);
            label16.TabIndex = 39;
            label16.Text = "Genre name";
            // 
            // btnAddGenre
            // 
            btnAddGenre.AutoSize = true;
            btnAddGenre.Location = new Point(814, 379);
            btnAddGenre.Name = "btnAddGenre";
            btnAddGenre.Size = new Size(130, 59);
            btnAddGenre.TabIndex = 29;
            btnAddGenre.Text = "Add genre";
            btnAddGenre.UseVisualStyleBackColor = true;
            btnAddGenre.Click += btnAddGenre_Click;
            // 
            // tbxGenre
            // 
            tbxGenre.Location = new Point(799, 320);
            tbxGenre.Name = "tbxGenre";
            tbxGenre.PlaceholderText = "Genre name";
            tbxGenre.Size = new Size(182, 23);
            tbxGenre.TabIndex = 30;
            // 
            // dgvGenres
            // 
            dgvGenres.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGenres.Location = new Point(6, 6);
            dgvGenres.Name = "dgvGenres";
            dgvGenres.Size = new Size(507, 606);
            dgvGenres.TabIndex = 40;
            // 
            // ChildFormBooks
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 670);
            Controls.Add(tabControlBooks);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ChildFormBooks";
            Text = "childFormBook";
            Load += ChildFormBooks_Load;
            tabControlBooks.ResumeLayout(false);
            tabBookInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            tabAddBook.ResumeLayout(false);
            tabAddBook.PerformLayout();
            tabAddGenre.ResumeLayout(false);
            tabAddGenre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGenres).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControlBooks;
        private TabPage tabBookInfo;
        private TabPage tabAddBook;
        private TextBox tbxDescription;
        private TextBox tbxISBN;
        private Button btnCreateBook;
        private TextBox tbxLanguage;
        private TextBox tbxTitle;
        private DateTimePicker dtpPubDate;
        private TextBox tbxPageCount;
        private TextBox tbxPublisher;
        private DataGridView dgvBooks;
        private CheckedListBox lbxGenres;
        private CheckedListBox lbxAuthors;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label8;
        private ComboBox cbbxBookFormat;
        private TabPage tabAddGenre;
        private Label label16;
        private Button btnAddGenre;
        private TextBox tbxGenre;
        private DataGridView dgvGenres;
    }
}