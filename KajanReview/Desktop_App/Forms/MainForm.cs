using BusinessLogicLayer.EntityManagers;
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
        public MainForm()
        {
            InitializeComponent();

            _bookDataAccess = new BookDataAccess();
            _bookManager = new BookManager(_bookDataAccess);
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            ChildFormBooks childFormBooks = new ChildFormBooks(_bookManager);
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
