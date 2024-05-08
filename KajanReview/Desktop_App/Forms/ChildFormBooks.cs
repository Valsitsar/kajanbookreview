using BusinessLogicLayer.Entities;
using BusinessLogicLayer.EntityManagers;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_App.Forms
{
    public partial class ChildFormBooks : Form
    {
        private IBookManager _bookManager;
        public ChildFormBooks(IBookManager bookManager)
        {
            InitializeComponent();

            _bookManager = bookManager;

            tbxTitle.Text = "Test title";
            tbxDescription.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras blandit tempus enim, id laoreet tortor tempor vitae. Sed et dolor quis lectus vehicula dictum nec at ex. Aenean condimentum urna augue. Vivamus volutpat, purus vel egestas placerat, libero ligula suscipit metus, nec porta arcu leo et felis. Etiam scelerisque odio est. Integer efficitur tellus in risus tristique molestie.";
            tbxNoOfPages.Text = "420";
            tbxISBN.Text = "1234567890000";
            tbxPublisher.Text = "Test publisher";
            tbxLanguage.Text = "Test language";
        }

        private void btnCreateBook_Click(object sender, EventArgs e)
        {
            _bookManager.CreateBook(new Book {
                Title = tbxTitle.Text,
                Description = tbxDescription.Text,
                PageCount = Convert.ToInt32(tbxNoOfPages.Text),
                ISBN = tbxISBN.Text,
                Publisher = tbxPublisher.Text,
                PubDate = dtpPubDate.Value,
                Language = tbxLanguage.Text,
            });
        }
    }
}
