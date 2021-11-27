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

namespace ProjectGroup.view
{
    public partial class bookReturnGridControl : DevExpress.XtraEditors.XtraForm
    {
        public bookReturnGridControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            BookReturnData bookReturnData = new BookReturnData();
            bsReturnBook.DataSource = bookReturnData.GetBookReturns();
            gridControl1.DataSource = bsReturnBook;
            gridView1.PopulateColumns();

            StudentData studentData = new StudentData();
            bsStudent.DataSource = studentData.GetStudent();

            LibrarianData librarianData = new LibrarianData();
            bsLibrarian.DataSource = librarianData.GetLibrarian();

            gridView1.Columns.Remove(gridView1.Columns["Student"]);
            gridView1.Columns.Remove(gridView1.Columns["Librarian"]);
            gridView1.Columns.Remove(gridView1.Columns["BookReturnDetails"]);

            gridView1.Columns["StudentID"].ColumnEdit = repositoryItemLookUpEdit1;
            gridView1.Columns["LibrarianID"].ColumnEdit = repositoryItemLookUpEdit2;
        }
    }
}