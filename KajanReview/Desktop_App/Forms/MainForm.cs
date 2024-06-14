using BusinessLogicLayer.ManagerClasses;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer;
using Desktop_App.Forms;

namespace Desktop_App
{
    public partial class MainForm : Form
    {
        private Form activeForm;
        private IBookDataAccess _bookDataAccess;
        private IBookManager _bookManager;
        private IGenreDataAccess _genreDataAccess;
        private IGenreManager _genreManager;
        private IUserDataAccess _userDataAccess;
        private IUserManager _userManager;
        private IBookFormatManager _bookFormatManager;
        private IBookFormatDataAccess _bookFormatDataAccess;

        public MainForm()
        {
            InitializeComponent();

            _bookDataAccess = new BookDataAccess();
            _bookManager = new BookManager(_bookDataAccess);
            _genreDataAccess = new GenreDataAccess();
            _genreManager = new GenreManager(_genreDataAccess);
            _userDataAccess = new UserDataAccess();
            _userManager = new UserManager(_userDataAccess);
            _bookFormatDataAccess = new BookFormatDataAccess();
            _bookFormatManager = new BookFormatManager(_bookFormatDataAccess);
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            ChildFormBooks childFormBooks = new ChildFormBooks(_bookManager, _genreManager, _userManager, _bookFormatManager);
            OpenChildForm(childFormBooks);
        }
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelChildForm.Controls.Add(childForm);
            this.panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void pbxCloseWindow_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
