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
using DevExpress.XtraGrid.Columns;
using System.Diagnostics;

namespace ProjectGroup.view
{
    public partial class bookGridControl : DevExpress.XtraEditors.XtraForm
    {
        private BookData bookData = new BookData();
        private AuthorData authorData = new AuthorData();
        private CategoryData categoryData = new CategoryData();
        private StatusBookData StatusBookData = new StatusBookData();
        public bookGridControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {


            List<Author> listAuthor = authorData.GetAuthors();
            bsAuthor.DataSource = listAuthor;

            List<Category> categories = categoryData.GetCategories();
            bsCategory.DataSource = categories;

            List<BookStatus> bookStatuses = StatusBookData.getStatusBook();
            bsStatus.DataSource = bookStatuses;

            //dtCategory = categoryData.GetCategoriesTable();
            //bsCategory.DataSource = dtCategory;

            //dtStatus = StatusBookData.getStatusBookTable();
            //bsStatus.DataSource = dtStatus;
            List<Book> listBook = bookData.GetBooks();
            //dtBook = bookData.GetBooks();
            //bsBooks.DataSource = dtBook;
            gridControl.DataSource = listBook;
            gridView1.PopulateColumns();
            gridView1.Columns.Remove(gridView1.Columns["Author"]);
            gridView1.Columns.Remove(gridView1.Columns["Category"]);
            gridView1.Columns.Remove(gridView1.Columns["Status"]);

            //gridView1.Columns.Add(gridView1.Columns["Delete"]);
            GridColumn idCol = new GridColumn() { Caption = "Delete", Visible = true, FieldName = "Delete" };
            gridView1.Columns.Add(idCol);

            //add column edit
            GridColumn editCol = new GridColumn() { Caption = "Edit", Visible = true, FieldName = "Edit" };
            gridView1.Columns.Add(editCol);
            ////idCol.VisibleIndex = 0;
            ////gridControl.DataSource = dtBook;


            gridView1.Columns["AuthorID"].ColumnEdit = repositoryItemLookUpEdit1;
            gridView1.Columns["AuthorID"].ColumnEdit = repositoryItemGridLookUpEdit1;
            gridView1.Columns["CategoryID"].ColumnEdit = repositoryItemLookUpEdit2;
            gridView1.Columns["StatusId"].ColumnEdit = repositoryItemLookUpEdit3;
            gridView1.Columns["Delete"].ColumnEdit = btnDelete;
            gridView1.Columns["Edit"].ColumnEdit = btnEdit;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(XtraMessageBox.Show($"Do you really want to delete {(gridView1.GetFocusedRow() as Book).Title} ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bookData.DeleteBook((gridView1.GetFocusedRow() as Book).Id))
                {
                    XtraMessageBox.Show("Delete successful!");
                    LoadData();
                }
            } 
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (XtraMessageBox.Show($"Do you really want to delete {(gridView1.GetFocusedRow() as Book).Title} ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(bookData.DeleteBook((gridView1.GetFocusedRow() as Book).Id))
                {
                    XtraMessageBox.Show("Delete successful!");
                    LoadData();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Book b = gridView1.GetFocusedRow() as Book;
            bookDetail bookDetails = new bookDetail(false, b);
            DialogResult r = bookDetails.ShowDialog();

            if(r == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}