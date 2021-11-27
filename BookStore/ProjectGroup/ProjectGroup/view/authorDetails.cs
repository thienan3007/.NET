using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectGroup.entities;
using ProjectGroup.data;
using System.Text.RegularExpressions;

namespace ProjectGroup.view
{
    public partial class authorDetails : DevExpress.XtraEditors.XtraForm
    {
        bool addOrEdit;
        public Author AuthorAddOrEdit { get; set; }
        public authorDetails()
        {
            InitializeComponent();
        }

        public authorDetails(bool flag, Author a) :this()
        {
            addOrEdit = flag;
            AuthorAddOrEdit = a;
            if(flag == false)
            {
                InitData();
            }
        }

        private void InitData()
        {
            txtAuthorname.Text = AuthorAddOrEdit.AuthorName;
            txtAuthorEmail.Text = AuthorAddOrEdit.Email;
            txtAuthorPhone.Text = AuthorAddOrEdit.Phone;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result;
            AuthorData authorData = new AuthorData();
            AuthorAddOrEdit.AuthorName = txtAuthorname.Text;
            AuthorAddOrEdit.Email = txtAuthorEmail.Text;
            AuthorAddOrEdit.Phone = txtAuthorPhone.Text;
            if (addOrEdit == true)
            {
                result = authorData.AddAuthor(AuthorAddOrEdit);
            } else
            {
                result = authorData.UpdateAuthor(AuthorAddOrEdit);
            }
            if(result == true)
            {
                MessageBox.Show("Save successful!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            } else
            {
                MessageBox.Show("Save fail");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        public bool ValidAuthorName(string authorName, out string errorMsg) 
        {
            if(authorName.Length == 0)
            {
                errorMsg = "Author Name must not be empty!";
                return false;
            }

            if(!Regex.Match(authorName, "^[0-9]*$").Success)
            {
                errorMsg = "";
                return true;
            }

            errorMsg = "Author Name must be a string!";
            return false;
        }

        public bool ValidAuthorEmail(string authorEmail, out string errorMsg)
        {
            if (authorEmail.Length == 0)
            {
                errorMsg = "Author Name must not be empty!";
                return false;
            }

            if (authorEmail.Contains("@"))
            {
                errorMsg = "";
                return true;
            }

            errorMsg = "Author Name must be a string!";
            return false;
        }

        public bool ValidAuthorPhone(string authorPhone, out string errorMsg)
        {
            if (authorPhone.Length == 0)
            {
                errorMsg = "Author Name must not be empty!";
                return false;
            }

            if (Regex.Match(authorPhone, "^[0-9]*$").Success)
            {
                errorMsg = "";
                return true;
            }

            errorMsg = "Author phone must be a number!";
            return false;
        }

        private void txtAuthorname_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if(!ValidAuthorName(txtAuthorname.Text, out errorMsg))
            {
                e.Cancel = true;
                txtAuthorname.Select(0, txtAuthorname.Text.Length);

                //this.errorProvider1.SetError(txtAuthorname, errorMsg);
                this.dxErrorProvider1.SetError(txtAuthorname, errorMsg);
            }
        }

        private void txtAuthorname_Validated(object sender, EventArgs e)
        {
            //errorProvider1.SetError(txtAuthorname, "");
            dxErrorProvider1.SetError(txtAuthorname, "");
        }

        private void txtAuthorEmail_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidAuthorEmail(txtAuthorEmail.Text, out errorMsg))
            {
                e.Cancel = true;
                txtAuthorEmail.Select(0, txtAuthorEmail.Text.Length);

                //this.errorProvider1.SetError(txtAuthorname, errorMsg);
                this.dxErrorProvider1.SetError(txtAuthorEmail, errorMsg);
            }
        }

        private void txtAuthorEmail_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtAuthorEmail, "");
        }

        private void txtAuthorPhone_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidAuthorPhone(txtAuthorPhone.Text, out errorMsg))
            {
                e.Cancel = true;
                txtAuthorPhone.Select(0, txtAuthorPhone.Text.Length);

                //this.errorProvider1.SetError(txtAuthorname, errorMsg);
                this.dxErrorProvider1.SetError(txtAuthorPhone, errorMsg);
            }
        }

        private void txtAuthorPhone_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtAuthorPhone, "");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.Disable;
            this.Close();
        }
    }
}