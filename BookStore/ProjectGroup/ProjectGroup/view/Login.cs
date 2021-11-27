using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using ProjectGroup.data;
using ProjectGroup.entities;
using ProjectGroup.service;
using System.Text.RegularExpressions;

namespace ProjectGroup.view
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var id = txtUsername.Text.Trim();
            var password = txtPassword.Text.Trim();
            LibrarianData librarianData = new LibrarianData();
            Librarian librarian = new Librarian();
            bool foundError = false;
            if (!Regex.Match(id, "^[0-9]*$").Success)
            {
                foundError = true;
                dxErrorProvider1.SetError(txtUsername, "Librarian must be a number!");
            }
            else if (id.Length == 0 || password.Length == 0)
            {
                foundError = true;
                dxErrorProvider1.SetError(txtUsername, "Librarian ID must not be emptied");
                dxErrorProvider1.SetError(txtPassword, "Librarian Password must not be emptied");
            }
            if (foundError == false)
            {
                dxErrorProvider1.SetError(txtUsername, "");
                dxErrorProvider1.SetError(txtPassword, "");
                librarian = librarianData.Login(id, password);
                if (librarian != null)
                {
                    LoginAccount.Librarian = librarian;
                    XtraForm1 xtraForm1 = new XtraForm1();
                    xtraForm1.Show();
                    XtraMessageBox.Show($"Hello {librarian.LibrarianName}");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login fail");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(XtraMessageBox.Show("Do you really want to exit?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
                Application.Exit();
            }
        }
    }
}