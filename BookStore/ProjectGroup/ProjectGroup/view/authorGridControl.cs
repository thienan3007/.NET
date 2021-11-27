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
using ProjectGroup.data;
using ProjectGroup.entities;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ProjectGroup.view
{
    public partial class authorGridControl : DevExpress.XtraEditors.XtraForm
    {
        AuthorData authorData = new AuthorData();
        public authorGridControl()
        {
            InitializeComponent();
            InitData();
        }

        private void XtraForm3_Load(object sender, EventArgs e)
        {
            InitData();
        }

        public void InitData()
        {
            List<Author> authors = authorData.GetAuthors();
            bsAuthor.DataSource = authors;
            gridControl1.DataSource = bsAuthor;
            gridView1.PopulateColumns();

            GridColumn deleteCol = new GridColumn() {Caption="Delete", Visible = true, FieldName="Delete" };
            gridView1.Columns.Add(deleteCol);

            GridColumn editCol = new GridColumn() { Caption = "Edit", Visible = true, FieldName = "Edit" };
            gridView1.Columns.Add(editCol);

            gridView1.Columns["Delete"].ColumnEdit = btnDelete;
            gridView1.Columns["Edit"].ColumnEdit = btnEdit;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Author author = (gridView1.GetFocusedRow() as Author);
            authorDetails authorDetails = new authorDetails(false, author);
            DialogResult r = authorDetails.ShowDialog();
            if(r == DialogResult.OK)
            {
                InitData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(XtraMessageBox.Show($"Do you really want to delete author {(gridView1.GetFocusedRow() as Author).AuthorName} ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(authorData.DeleteAuthor((gridView1.GetFocusedRow() as Author).AuthorID))
                {
                    XtraMessageBox.Show("Delete successful!");
                    InitData();
                }
            }
        }
    }
}