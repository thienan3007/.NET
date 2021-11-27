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

namespace ProjectGroup.view
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
        }

        private void ViewChildForm(Form form)
        {
            //check before open
            if (!IsFormActived(form))
            {
                form.MdiParent = this;
                form.Show();
            }
        }

        private bool IsFormActived(Form form)
        {
            bool IsOpened = false;
            if (MdiChildren.Count() > 0)
            {
                foreach (var item in MdiChildren)
                {
                    if (form.Name == item.Name)
                    {
                        xtraTabbedMdiManager1.Pages[item].MdiChild.Activate();
                        IsOpened = true;
                    }
                }
            }
            return IsOpened;
        }

        private void CloseForm(Form form)
        {
            foreach (var item in MdiChildren)
            {
                if (form.Name == item.Name)
                {
                    xtraTabbedMdiManager1.Pages[item].MdiChild.Close();
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Book b = new Book();
            bookDetail bookDetails = new bookDetail(true, b);
            DialogResult r = bookDetails.ShowDialog();
            if(r == DialogResult.OK)
            {
                bookGridControl bookGridControl = new bookGridControl();
                CloseForm(bookGridControl);
                ViewChildForm(bookGridControl);
            }
        }

        private void showAllBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bookGridControl bookGridControl = new bookGridControl();
            //bookGridControl.MdiParent = this;
            //bookGridControl.Show();
            ViewChildForm(bookGridControl);
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            studentDetails studentDetails = new studentDetails(true, s);
            DialogResult r = studentDetails.ShowDialog();
            if(r == DialogResult.OK)
            {
                studentGridControl studentGridControl = new studentGridControl();
                CloseForm(studentGridControl);
                ViewChildForm(studentGridControl);
            }
        }

        private void showAllStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            studentGridControl studentGridControl = new studentGridControl();
            ViewChildForm(studentGridControl);
        }

        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Author a = new Author();
            authorDetails authorDetails = new authorDetails(true, a);
            DialogResult r = authorDetails.ShowDialog();
            if(r == DialogResult.OK)
            {
                authorGridControl xtraForm3 = new authorGridControl();
                CloseForm(xtraForm3);
                xtraForm3.InitData();
                ViewChildForm(xtraForm3);
            }
        }

        private void showAllAuthorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authorGridControl xtraForm3 = new authorGridControl();
            ViewChildForm(xtraForm3);
        }

        private void addToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            borrowBookDetails borrowBookDetails = new borrowBookDetails();
            DialogResult r = borrowBookDetails.ShowDialog();
            if(r == DialogResult.OK)
            {
                bookborrowGridControl bookborrowGridControl = new bookborrowGridControl();
                CloseForm(bookborrowGridControl);
                ViewChildForm(bookborrowGridControl);
            }

        }

        private void showAllBorrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bookborrowGridControl bookborrowGridControl = new bookborrowGridControl();
            ViewChildForm(bookborrowGridControl);
        }

        private void addToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            returnBookDetails returnBookDetails = new returnBookDetails();
            DialogResult r = returnBookDetails.ShowDialog();
            if(r == DialogResult.OK)
            {

            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Do you really want to log out?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(XtraMessageBox.Show("Do you really want to exit?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void showAllReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bookReturnGridControl bookReturnGridControl = new bookReturnGridControl();
            ViewChildForm(bookReturnGridControl);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authorlistview authorlistview = new authorlistview();
            ViewChildForm(authorlistview);
        }
    }
}