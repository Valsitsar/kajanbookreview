using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System.Runtime.CompilerServices;

namespace Desktop_App.Forms
{
    public partial class ChildFormBooks : Form
    {
        private IBookManager _bookManager;
        private IGenreManager _genreManager;
        private IUserManager _userManager;
        private IBookFormatManager _bookFormatManager;

        private Dictionary<string, Genre> _genreMap = new Dictionary<string, Genre>();
        private Dictionary<string, User> _authorMap = new Dictionary<string, User>();
        private Dictionary<string, BookFormat> _bookFormatMap = new Dictionary<string, BookFormat>();

        public ChildFormBooks(IBookManager bookManager, IGenreManager genreManager, IUserManager userManager, IBookFormatManager bookFormatManager)
        {
            InitializeComponent();


            _bookManager = bookManager;
            _genreManager = genreManager;
            _userManager = userManager;
            _bookFormatManager = bookFormatManager;

        }

        private async void ChildFormBooks_Load(object sender, EventArgs e)
        {
            tbxTitle.Text = "Test title";
            tbxDescription.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras blandit tempus enim, id laoreet tortor tempor vitae. Sed et dolor quis lectus vehicula dictum nec at ex. Aenean condimentum urna augue. Vivamus volutpat, purus vel egestas placerat, libero ligula suscipit metus, nec porta arcu leo et felis. Etiam scelerisque odio est. Integer efficitur tellus in risus tristique molestie.";
            tbxPageCount.Text = "420";
            tbxISBN.Text = "1234567890000";
            tbxPublisher.Text = "Test publisher";
            tbxLanguage.Text = "Test language";

            dgvBooks.Columns.Add("ID", "ID");
            dgvBooks.Columns.Add("Title", "Title");
            dgvBooks.Columns.Add("Description", "Description");
            dgvBooks.Columns.Add("PageCount", "Page Count");
            dgvBooks.Columns.Add("ISBN", "ISBN");
            dgvBooks.Columns.Add("Publisher", "Publisher");
            dgvBooks.Columns.Add("PubDate", "Publication Date");
            dgvBooks.Columns.Add("Language", "Language");
            dgvBooks.Columns.Add("Format", "Format");

            dgvGenres.Columns.Add("ID", "ID");
            dgvGenres.Columns.Add("Name", "Name");


            await LoadAllDataAsync();
        }

        private async Task LoadAllDataAsync()
        {
            // Load books from DB
            var allBooks = await _bookManager.GetAllBooksAsync();
            // Clear existing books
            dgvBooks.Rows.Clear();
            foreach (var book in allBooks)
            {
                int rowIndex = dgvBooks.Rows.Add();
                DataGridViewRow newRow = dgvBooks.Rows[rowIndex];
                newRow.Cells["ID"].Value = book.ID;
                newRow.Cells["Title"].Value = book.Title;
                newRow.Cells["Description"].Value = book.Description;
                newRow.Cells["PageCount"].Value = book.PageCount;
                newRow.Cells["ISBN"].Value = book.ISBN;
                newRow.Cells["Publisher"].Value = book.Publisher;
                newRow.Cells["PubDate"].Value = book.PubDate;
                newRow.Cells["Language"].Value = book.Language;
                newRow.Cells["Format"].Value = book.Format.Name;
            }


            // Load users from DB
            var allUsers = await _userManager.GetAllUsersAsync();
            // Load genres from DB
            var genres = await _genreManager.GetAllGenresAsync();
            // Load book formats from DB
            var bookFormats = await _bookFormatManager.GetAllBookFormatsAsync();

            foreach (var genre in genres)
            {
                int rowIndex = dgvGenres.Rows.Add();
                DataGridViewRow newRow = dgvGenres.Rows[rowIndex];
                newRow.Cells["ID"].Value = genre.ID;
                newRow.Cells["Name"].Value = genre.Name;
            }

            // Clear existing authors
            lbxAuthors.Items.Clear();
            _authorMap.Clear();
            // Clear existing genres
            lbxGenres.Items.Clear();
            _genreMap.Clear();
            // Clear existing book formats
            cbbxBookFormat.Items.Clear();
            _bookFormatMap.Clear();

            // Populate authors and map
            foreach (var user in allUsers)
            {
                if (user.Role.Name == "Author")
                {
                    string displayText = $"{user.FirstName} {user.MiddleNames} {user.LastName} - {user.ID}";
                    lbxAuthors.Items.Add(displayText, false);
                    _authorMap[displayText] = user;
                }
            }

            // Populate genres and map
            foreach (var genre in genres)
            {
                string displayText = $"{genre.Name} - {genre.ID}";
                lbxGenres.Items.Add(displayText, false);
                _genreMap[displayText] = genre;
            }

            // Populate book formats and map
            foreach (var bookFormat in bookFormats)
            {
                string displayText = $"{bookFormat.Name} - {bookFormat.ID}";
                cbbxBookFormat.Items.Add(displayText);
                _bookFormatMap[displayText] = bookFormat;
            }
        }

        private async void btnCreateBook_Click(object sender, EventArgs e)
        {
            var selectedAuthors = new List<User>();
            foreach (var item in lbxAuthors.CheckedItems)
            {
                string displayText = item.ToString();
                if (_authorMap.TryGetValue(displayText, out User user))
                {
                    selectedAuthors.Add(user);
                }
            }

            var selectedGenres = new List<Genre>();
            foreach (var item in lbxGenres.CheckedItems)
            {
                string displayText = item.ToString();
                if (_genreMap.TryGetValue(displayText, out Genre genre))
                {
                    selectedGenres.Add(genre);
                }
            }

            var selectedBookFormat = new BookFormat();
            if (cbbxBookFormat.SelectedItem != null)
            {
                string displayText = cbbxBookFormat.SelectedItem.ToString();
                if (_bookFormatMap.TryGetValue(displayText, out BookFormat bookFormat))
                {
                    selectedBookFormat = bookFormat;
                }
            }

            var book = new Book()
            {
                Title = tbxTitle.Text,
                Description = tbxDescription.Text,
                PageCount = int.Parse(tbxPageCount.Text),
                Publisher = tbxPublisher.Text,
                PubDate = dtpPubDate.Value,
                Language = tbxLanguage.Text,
                ISBN = tbxISBN.Text,
                Genres = selectedGenres,
                Authors = selectedAuthors,
                Format = selectedBookFormat
            };

            await _bookManager.CreateBookAsync(book);

            MessageBox.Show("Book created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearAllFields();
            await LoadAllDataAsync();
        }

        private void ClearAllFields()
        {
            tbxTitle.Text = string.Empty;
            tbxDescription.Text = string.Empty;
            tbxPageCount.Text = string.Empty;
            tbxPublisher.Text = string.Empty;
            dtpPubDate.Value = DateTime.Now;
            tbxLanguage.Text = string.Empty;
            tbxISBN.Text = string.Empty;
            lbxAuthors.ClearSelected();
            lbxGenres.ClearSelected();
            cbbxBookFormat.SelectedIndex = -1;
        }

        private async void btnAddGenre_Click(object sender, EventArgs e)
        {
            var genre = new Genre()
            {
                Name = tbxGenre.Text
            };

            await _genreManager.CreateGenreAsync(genre);
            tbxGenre.Text = string.Empty;
            await LoadAllDataAsync();

            MessageBox.Show("Genre created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
