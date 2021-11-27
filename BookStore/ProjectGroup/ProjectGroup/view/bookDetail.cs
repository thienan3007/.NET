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
    public partial class bookDetail : DevExpress.XtraEditors.XtraForm
    {
        public bookDetail()
        {
            InitializeComponent();
        }
        private bool addOrEdit;

        public Book BookAddOrEdit { get; set; }
        public AuthorData authorData = new AuthorData();
        public CategoryData categoryData = new CategoryData();
        public StatusBookData StatusBookData = new StatusBookData();
        bool quantityFlag = false;
        bool titleFlag = false;
        bool NOPFlag = false;
        bool langFlag = false;

        public bookDetail(bool flag, Book b) : this()
        {
            addOrEdit = flag;
            BookAddOrEdit = b;
            if (flag == true)
            {
                //textBoxBookID.Visible = false;
                InitComboboxCategory();
                InitComboboxStatus();
                InitComboboxAuthor();
            }
            else
            {
                txtID.Enabled = false;
                InitData();
            }

        }

        private void InitData()
        {
            txtID.Text = BookAddOrEdit.Id.ToString();
            txtTitle.Text = BookAddOrEdit.Title.ToString();
            txtLanguage.Text = BookAddOrEdit.Language.ToString();
            txtNOP.Text = BookAddOrEdit.NoOfPages.ToString();
            txtQuantity.Text = BookAddOrEdit.QuantityLeft.ToString();
            memoDes.Text = BookAddOrEdit.Description.ToString();
            InitComboboxCategory();

            InitComboboxAuthor();
            InitComboboxStatus();
        }

        private void InitComboboxCategory()
        {
            List<Category> listCate = categoryData.GetCategories();
            cbCategory.DataSource = listCate;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "CategoryID";
            if (addOrEdit == false)
            {
                cbCategory.SelectedIndex = cbCategory.FindStringExact(BookAddOrEdit.Category.CategoryName);
            }
        }

        private void InitComboboxAuthor()
        {
            List<Author> listAuthor = authorData.GetAuthors();
            cbAuthor.DataSource = listAuthor;
            cbAuthor.DisplayMember = "AuthorName";
            cbAuthor.ValueMember = "AuthorID";
            if (addOrEdit == false)
            {
                cbAuthor.SelectedIndex = cbAuthor.FindStringExact(BookAddOrEdit.Author.AuthorName);
            }
        }

        private void InitComboboxStatus()
        {
            List<BookStatus> bookStatuses = StatusBookData.getStatusBook();
            cbStatus.DataSource = bookStatuses;
            cbStatus.DisplayMember = "StatusName";
            cbStatus.ValueMember = "ID";
            if (addOrEdit == false)
            {
                cbStatus.SelectedIndex = cbStatus.FindStringExact(BookAddOrEdit.Status.StatusName);
            }
        }

        //------------------------------------------------------------------------------------------------
        //validate book Title
        private bool ValidTitle(string title, out string errorMSg)
        {
            if(title.Length == 0)
            {
                errorMSg = "Book title must not be emptied!";
                return false;
            } else if (title.Length > 100)
            {
                errorMSg = "Book title must not be larger than 100 characters!";
                return false;
            }
            errorMSg = "";
            return true;
        }

        private void txtTitle_Validated(object sender, EventArgs e)
        {
            titleFlag = true;
            dxErrorProvider1.SetError(txtTitle, "");
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if(!ValidTitle(txtTitle.Text.Trim(), out errorMsg))
            {
                e.Cancel = true;
                this.dxErrorProvider1.SetError(txtTitle, errorMsg);

                txtTitle.Select(0, txtTitle.Text.Length);
            }
        }

        //------------------------------------------------------------------------------------------------
        //validate book Lang
        private bool ValidLanguae(string lang, out string errorMsg)
        {
            if(lang.Length == 0)
            {
                errorMsg = "Book language must not be emptied!";
                return false;
            } else if (lang.Length > 50)
            {
                errorMsg = "Book language must not be larger than 50 characters!";
                return false;
            }
            errorMsg = "";
            return true;
        }

        private void txtLanguage_Validated(object sender, EventArgs e)
        {
            langFlag = true;
            dxErrorProvider1.SetError(txtLanguage, ""); 
        }

        private void txtLanguage_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if(!ValidLanguae(txtLanguage.Text.Trim(), out errorMsg))
            {
                e.Cancel = true;
                this.dxErrorProvider1.SetError(txtLanguage, errorMsg);

                txtLanguage.Select(0, txtLanguage.Text.Length);
            }
        }


        //------------------------------------------------------------------------------------------------
        //validate book Quantiy and NOP
        private bool ValidQuantityOrNOP(string quantity, out string errrorMsg, int choose)
        {
            if(quantity.Length == 0)
            {
                if(choose == 1)
                {
                    errrorMsg = "Quantity must not be emptied!";
                    return false;
                } else if (choose == 2 )
                {
                    errrorMsg = "Number of pages must not be emptied!";
                    return false;
                }
            } else if (!Regex.Match(quantity, "^[0-9]*$").Success)
            {
                if(choose == 1)
                {
                    errrorMsg = "Quantity must not be a number!";
                    return false;
                } else if (choose == 2)
                {
                    errrorMsg = "Number of pages must not be a number!";
                    return false;
                }
            }
            errrorMsg = "";
            return true;
        }

        private void txtNOP_Validated(object sender, EventArgs e)
        {
            NOPFlag = true;
            dxErrorProvider1.SetError(txtNOP, "");
        }

        private void txtNOP_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if(!ValidQuantityOrNOP(txtNOP.Text.Trim(), out errorMsg, 2))
            {
                e.Cancel = true;
                this.dxErrorProvider1.SetError(txtNOP, errorMsg);

                txtNOP.Select(0, txtNOP.Text.Length);
            }
        }

        private void txtQuantity_Validated(object sender, EventArgs e)
        {
            quantityFlag = true;
            dxErrorProvider1.SetError(txtQuantity, "");
        }

        private void txtQuantity_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidQuantityOrNOP(txtQuantity.Text.Trim(), out errorMsg, 1))
            {
                e.Cancel = true;
                this.dxErrorProvider1.SetError(txtQuantity, errorMsg);

                txtQuantity.Select(0, txtQuantity.Text.Length);
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if(NOPFlag == false || titleFlag == false || quantityFlag == false || langFlag ==false)
            {
                txtNOP_Validating(null, new CancelEventArgs());
                txtLanguage_Validating(null, new CancelEventArgs());
                txtTitle_Validating(null, new CancelEventArgs());
                txtQuantity_Validating(null, new CancelEventArgs());
            }
            else
            {
                bool flag;
                //BookAddOrEdit.Id = int.Parse(textBoxBookID.Text);
                BookAddOrEdit.Title = txtTitle.Text;
                BookAddOrEdit.Language = txtLanguage.Text;
                BookAddOrEdit.NoOfPages = int.Parse(txtNOP.Text);
                BookAddOrEdit.QuantityLeft = int.Parse(txtQuantity.Text);
                BookAddOrEdit.Description = memoDes.Text;
                var author = cbAuthor.SelectedItem as Author;
                BookAddOrEdit.Author = author;
                BookAddOrEdit.Category = cbCategory.SelectedItem as Category;
                BookStatus status = cbStatus.SelectedItem as BookStatus;
                BookAddOrEdit.Status = status;
                BookData bookData = new BookData();
                if (addOrEdit == true)
                {
                    flag = bookData.AddBook(BookAddOrEdit);
                }
                else
                {
                    flag = bookData.UpdateBook(BookAddOrEdit);
                }
                if (flag == true)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    XtraMessageBox.Show("Save successfully");
                }
                else
                {
                    XtraMessageBox.Show("save fail");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}