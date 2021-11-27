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
    public partial class bookborrowGridControl : DevExpress.XtraEditors.XtraForm
    {
        BookBorrowData BookBorrowData = new BookBorrowData();
        StudentData studentData = new StudentData();
        LibrarianData librarianData = new LibrarianData();
        public bookborrowGridControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            List<BookBorrow> bookBorrows = BookBorrowData.GetBookBorrows();
            bsBookBorrow.DataSource = bookBorrows;

            List<Student> listStudents = studentData.GetStudent();
            bsStudent.DataSource = listStudents;

            List<Librarian> listLibrarians = librarianData.GetLibrarian();
            bsLibrarian.DataSource = listLibrarians;

            gridControl1.DataSource = bsBookBorrow;
            gridView1.PopulateColumns();

            gridView1.Columns.Remove(gridView1.Columns["Student"]);
            gridView1.Columns.Remove(gridView1.Columns["Librarian"]);

            gridView1.Columns["StudentID"].ColumnEdit = repositoryItemLookUpEdit1;
            gridView1.Columns["LibrarianID"].ColumnEdit = repositoryItemLookUpEdit2;
        }
    }
}