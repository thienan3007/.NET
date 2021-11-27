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
using System.Diagnostics;
using ProjectGroup.service;

namespace ProjectGroup.view
{
    public partial class borrowBookDetails : DevExpress.XtraEditors.XtraForm
    {
        public BookData bookData = new BookData();
        public Book book = null;
        bool studentNameFlag = false;
        bool studentIDFlag = false;
        bool quantityFlag = false;
        bool dueDateFlag = false;
        bool borrowDateFlag = false;
        public List<BookBorrowDetail> bookBorrowDetails = null;
        public borrowBookDetails()
        {
            InitializeComponent();
        }

        //validate student name
        private bool ValidStudentName(string studentName, out string errorMSg)
        {
            if (studentName.Length == 0)
            {
                errorMSg = "Student name cannot be empty!";
                return false;
            }
            if (!Regex.Match(studentName.Trim(), "^[0-9]*$").Success)
            {
                errorMSg = "";
                return true;
            }
            errorMSg = "Student name must be a string!";
            return false;
        }

        private void txtStudentName_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtStudentName, "");
            studentNameFlag = true;
        }

        private void txtStudentName_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidStudentName(txtStudentName.Text, out errorMsg))
            {
                e.Cancel = true;
                txtStudentName.Select(0, txtStudentName.Text.Length);

                dxErrorProvider1.SetError(txtStudentName, errorMsg);
            }
        }

        //validate student id
        private bool ValidStudentID(string studentID, out string errorMsg)
        {
            if (studentID.Length == 0)
            {
                errorMsg = "Student's id must not be emptied!";
                return false;
            }
            if (Regex.Match(studentID, "^[0-9]*$").Success)
            {
                errorMsg = "";
                return true;
            }
            errorMsg = "Student's id must be a number!";
            return false;
        }

        private void txtStudentID_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtStudentID, "");
            studentIDFlag = true;
        }

        private void txtStudentID_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidStudentID(txtStudentID.Text, out errorMsg))
            {
                e.Cancel = true;
                txtStudentID.Select(0, txtStudentID.Text.Length);

                this.dxErrorProvider1.SetError(txtStudentID, errorMsg);
            }
        }

        // booktitle validattion
        private void txtBookTitle_Leave(object sender, EventArgs e)
        {
            List<Book> books = bookData.GetBooksByTitle(txtBookTitle.Text.Trim());
            if (txtBookTitle.Text.Length == 0)
            {
                dxErrorProvider1.SetError(txtBookTitle, "Invalid title");
                txtBookTitle.Select(0, txtBookTitle.Text.Length);
            }
            else if (books.Count > 0)
            {
                cbBook.DataSource = books;
                cbBook.DisplayMember = "Title";
                cbBook.ValueMember = "Id";
                dxErrorProvider1.SetError(txtBookTitle, "");
            } else if (books.Count == 0)
            {
                dxErrorProvider1.SetError(txtBookTitle, "Invalid title");
                txtBookTitle.Select(0, txtBookTitle.Text.Length);
            }
        }

        private void cbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            book = cbBook.SelectedItem as Book;
            txtBookID.Text = (cbBook.SelectedItem as Book).Id.ToString();
            memoBookDes.Text = (cbBook.SelectedItem as Book).Description;
        }

        private void dateDue_Validated_1(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(dateDue, "");
            dueDateFlag = true;
        }

        private void dateDue_Validating_1(object sender, CancelEventArgs e)
        {
            if (DateTime.Compare(dateBorrow.DateTime, dateDue.DateTime) > 0)
            {
                e.Cancel = true;

                dxErrorProvider1.SetError(dateDue, "Due date cannot be before borrow date!");
            }
        }


        //validate quantity 
        private bool ValidQuantity(string quantity, out string errorMsg)
        {
            if (quantity.Length == 0)
            {
                errorMsg = "quantity must not be emptied!";
                return false;
            }
            if (Regex.Match(quantity.Trim(), "^[0-9]*$").Success)
            {
                if (int.Parse(quantity) > book.QuantityLeft)
                {                  
                    errorMsg = "Quantity must not be greater than quantity left of this book!";
                    return false;
                } else if (int.Parse(quantity) > 0) 
                {
                    errorMsg = "";
                    return true;
                }
                else
                {
                    errorMsg = "Quantity must be greater than 0!";
                    return false;
                }
            }
            errorMsg = "Quantity must be a number and greater than 0";
            return false;
        }

        private void txtQuantity_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtQuantity, "");
            quantityFlag = true;
        }

        private void txtQuantity_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidQuantity(txtQuantity.Text, out errorMsg))
            {
                e.Cancel = true;
                txtQuantity.Select(0, txtQuantity.Text.Length);

                dxErrorProvider1.SetError(txtQuantity, errorMsg);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (book == null)
            {
                XtraMessageBox.Show("You have not find book yet!");
            } else if (studentNameFlag == false || studentIDFlag == false || quantityFlag == false)
            {
                txtStudentID_Validating(null, new CancelEventArgs());
                txtStudentName_Validating(null, new CancelEventArgs());
                txtQuantity_Validating(null, new CancelEventArgs());
            }
            else
            {
                double totalPrice = 0;
                if (bookBorrowDetails == null)
                {
                    bookBorrowDetails = new List<BookBorrowDetail>();
                    bookBorrowDetails.Add(new BookBorrowDetail
                    {
                        Book = book,
                        BookId = book.Id,
                        Price = 100 * int.Parse(txtQuantity.Text),
                        Quantity = int.Parse(txtQuantity.Text)
                    });
                    totalPrice += 100 * int.Parse(txtQuantity.Text);
                    LoadGridControl(bookBorrowDetails);
                } else
                {
                    var addBook = bookBorrowDetails.SingleOrDefault(b => b.BookId == book.Id);
                    if(addBook != null)
                    {
                        addBook.Quantity += int.Parse(txtQuantity.Text);
                        addBook.Price = addBook.Quantity * 100;
                        totalPrice = bookBorrowDetails.Sum(b => b.Price);
                        LoadGridControl(bookBorrowDetails);
                    } else
                    {
                        bookBorrowDetails.Add(new BookBorrowDetail
                        {
                            Book = book,
                            BookId = book.Id,
                            Price = 100,
                            Quantity = int.Parse(txtQuantity.Text)
                        });
                        totalPrice = bookBorrowDetails.Sum(b => b.Price);
                        LoadGridControl(bookBorrowDetails);
                    }
                }
                txtTotalPrice.Text = totalPrice.ToString();
            }
        }

        private void LoadGridControl(List<BookBorrowDetail> bookBorrowDetails)
        {
            bsBorrow.DataSource = bookBorrowDetails;
            gridControl1.DataSource = bsBorrow;
            gridView1.PopulateColumns();
            gridView1.Columns.Remove(gridView1.Columns["Book"]);
            gridView1.Columns.Remove(gridView1.Columns["BookBorrowID"]);
            gridView1.Columns.Remove(gridView1.Columns["Id"]);
        }

        private void dateBorrow_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(dateBorrow, "");
            borrowDateFlag = true;
        }

        private void dateBorrow_Validating(object sender, CancelEventArgs e)
        {
            DateTime now = DateTime.Now;
            if(DateTime.Compare(dateBorrow.DateTime, now) < 0)
            {
                e.Cancel = true;

                dxErrorProvider1.SetError(dateBorrow, "Borrow date must be after today!");
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.Disable;
            if(bookBorrowDetails != null)
            {
                if (XtraMessageBox.Show($"You have {bookBorrowDetails.Count} order in your borrow. Do you really want to cancel?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this.Close();
                }
            } else
            {
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(bookBorrowDetails == null)
            {
                XtraMessageBox.Show("Your borrow is emptied!");
            } else if (dueDateFlag == false || borrowDateFlag == false)
            {
                dateDue_Validating_1(null, new CancelEventArgs());
                dateBorrow_Validating(null, new CancelEventArgs());
            } else
            {
                BookBorrowData bookBorrowData = new BookBorrowData();
                BookBorrowDetailData bookBorrowDetailData = new BookBorrowDetailData();
                BookData bookData = new BookData();

                BookBorrow bookBorrow = new BookBorrow {
                    BorrowDate = dateBorrow.DateTime,
                    DueDate = dateDue.DateTime,
                    LibrarianID = LoginAccount.Librarian.LibrarianID,
                    StudentID = int.Parse(txtStudentID.Text),
                    ListBookBorrowDetail = bookBorrowDetails,
                    TotalPrice = double.Parse(txtTotalPrice.Text)
                };

                XtraMessageBox.Show(bookBorrow.ListBookBorrowDetail.Count.ToString());

                int bookBorrowId = bookBorrowData.InsertBookBorrow(bookBorrow);
                bookBorrow.Id = bookBorrowId;

                if(bookBorrowDetailData.InsertBookDetail(bookBorrow))
                {
                    if(bookData.UpdateBook(bookBorrow.ListBookBorrowDetail))
                    {
                        this.DialogResult = DialogResult.OK;
                        XtraMessageBox.Show("Save successful!");
                    }
                    else
                    {
                        XtraMessageBox.Show("Save fail 1!");
                    }
                } else
                {
                    XtraMessageBox.Show("Save fail 2!");
                }
            }
        }
    }
}