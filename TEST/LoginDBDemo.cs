using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LT_Support;

namespace TEST
{
    public partial class LoginDBDemo : Form
    {
        public List<PersonModel> people = new List<PersonModel>();
        public LoginDBDemo()
        {
            InitializeComponent();
            LoadPeopleList();
        }

        private void LoadPeopleList()
        {
            people.Add(new PersonModel { FirstName = "Le Anh", LastName = "Tu" });
            people.Add(new PersonModel { FirstName = "Le Tuan", LastName = "Anh" });
            UpdateListBoxPeople();
        }

        private void UpdateListBoxPeople()
        {
            listBoxPeople.DataSource = null;
            listBoxPeople.DataSource = people;
            listBoxPeople.DisplayMember = "FullName";
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            UpdateListBoxPeople();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            PersonModel p = new PersonModel();
            p.FirstName = txtFirstNameInput.Text;
            p.LastName = txtLastNameInput.Text;
            people.Add(p);
            UpdateListBoxPeople();
            txtFirstNameInput.Text = "";
            txtLastNameInput.Text = "";
        }
    }
}
