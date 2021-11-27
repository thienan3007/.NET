using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectGroup.entities;
using ProjectGroup.data;
using DevExpress.XtraGrid.Columns;
using ProjectGroup.service;

namespace ProjectGroup.view
{
    public partial class returnBookDetails : DevExpress.XtraEditors.XtraForm
    {
        private bool studentIdFlag = false;
        private bool borrowIdFlag = false;
        Student student = new Student();
        List<BookReturnDetail> bookReturnDetails = null;
        List<BookBorrowDetail> bookBorrows = new List<BookBorrowDetail>();
        List<BookBorrowDetail> bookBorrowsOld = new List<BookBorrowDetail>();
        public returnBookDetails()
        {
            InitializeComponent();
            //InitBookBorrow();
        }

        //valid student id
        private bool ValidStudentID(string studentID, out string errorMsg)
        {
            if (studentID.Length == 0)
            {
                errorMsg = "Student's ID must not be emptied!";
                return false;
            }
            if (Regex.Match(studentID.Trim(), "^[0-9]*$").Success)
            {
                errorMsg = "";
                return true;
            }
            errorMsg = "Student's ID must be a number";
            return false;
        }
        private void txtStudentID_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtStudentID, "");
            studentIdFlag = true;
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

        //valid student id
        private bool ValidBorrowID(string borrowID, out string errorMsg)
        {
            if (borrowID.Length == 0)
            {
                errorMsg = "Student's ID must not be emptied!";
                return false;
            }
            if (Regex.Match(borrowID.Trim(), "^[0-9]*$").Success)
            {
                errorMsg = "";
                return true;
            }
            errorMsg = "Student's ID must be a number";
            return false;
        }
        private void txtBorrowID_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtBorrowID, "");
            borrowIdFlag = true;
        }

        private void txtBorrowID_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidBorrowID(txtBorrowID.Text, out errorMsg))
            {
                e.Cancel = true;
                txtBorrowID.Select(0, txtBorrowID.Text.Length);

                this.dxErrorProvider1.SetError(txtBorrowID, errorMsg);
            }
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (studentIdFlag == false || borrowIdFlag == false)
            {
                txtBorrowID_Validating(null, new CancelEventArgs());
                txtStudentID_Validating(null, new CancelEventArgs());
            }
            else
            {
                StudentData studentData = new StudentData();
                BookBorrowData bookBorrowData = new BookBorrowData();
                BookBorrowDetailData bookBorrowDetailData = new BookBorrowDetailData();

                student = studentData.GetStudentById(int.Parse(txtStudentID.Text));
                BookBorrow bookBorrow = bookBorrowData.GetBookBorrowById(int.Parse(txtBorrowID.Text));
                bookBorrows = bookBorrowDetailData.GetBookBorrowDetailsByBorrowID(int.Parse(txtBorrowID.Text));
                bookBorrowsOld = bookBorrowDetailData.GetBookBorrowDetailsByBorrowID(int.Parse(txtBorrowID.Text));

                if (student != null && bookBorrow != null)
                {
                    txtStudentName.Text = student.StudentName;
                    txtStudentEmail.Text = student.Email;
                    txtStudentPhone.Text = student.Phone;

                    dateBorrow.Text = bookBorrow.BorrowDate.ToString();
                    dateDue.Text = bookBorrow.DueDate.ToString();
                    txtTotalPrice.Text = bookBorrow.TotalPrice.ToString();
                    if(bookBorrows == null)
                    {
                        XtraMessageBox.Show("Has no book!");
                    }
                    if (bookBorrows.Count > 0)
                    {
                        LoadDataBookBorrrow(bookBorrows);
                        bookReturnDetails = null;
                        ReloadReturnBook(bookReturnDetails);
                    }
                    else
                    {
                        XtraMessageBox.Show("Has no book!");
                    }
                }
                else
                {
                    XtraMessageBox.Show("Load fail");
                }
            }
        }

        private void ReloadReturnBook(List<BookReturnDetail> bookReturnDetails)
        {
            bsBookReturnDetail.DataSource = bookReturnDetails;
        }

        private void LoadDataBookBorrrow(List<BookBorrowDetail> bookBorrows)
        {
            if (bookBorrows.Count(b => b.Quantity > 0) > 0)
            {
                BookData bookData = new BookData();
                bsBorrowDetail.DataSource = bookBorrows.Where(d => d.Quantity > 0);

                List<Book> books = bookData.GetBooks();
                bsBook.DataSource = books;
                gridView1.Columns.Remove(gridView1.Columns["Add"]);
                GridColumn addCol = new GridColumn() { Caption = "Add", Visible = true, FieldName = "Add" };
                gridView1.Columns.Add(addCol);
                gridView1.Columns["Id"].OptionsColumn.AllowEdit = false;
                gridView1.Columns["BookBorrowID"].OptionsColumn.AllowEdit = false;
                gridView1.Columns["BookId"].OptionsColumn.AllowEdit = false;
                gridView1.Columns["Price"].OptionsColumn.AllowEdit = false;
                gridView1.Columns["Quantity"].OptionsColumn.AllowEdit = false;

                gridView1.Columns.Remove(gridView1.Columns["Book"]);
                gridView1.Columns.Remove(gridView1.Columns["Status"]);

                gridView1.Columns["BookId"].ColumnEdit = repositoryItemLookUpEdit3;
                gridView1.Columns["Add"].ColumnEdit = btnAdd;
            }
            else
            {
                bsBorrowDetail.DataSource = null;
                gridView1.Columns.Clear();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.Disable;
            this.Close();
        }

        private void LoadDataBookReturn(List<BookReturnDetail> bookReturnDetails)
        {
            if(bookReturnDetails.Count > 0)
            {
                bsBookReturnDetail.DataSource = bookReturnDetails;
                gridBookReturn.DataSource = bsBookReturnDetail;

                gridView2.Columns.Remove(gridView2.Columns["Book"]);
                gridView2.Columns.Remove(gridView2.Columns["BookReturnID"]);
                gridView2.Columns.Remove(gridView2.Columns["ID"]);

                gridView2.Columns["BookId"].ColumnEdit = repositoryItemLookUpEdit4;
            } else
            {
                bsBookReturnDetail.DataSource = null;
                gridView2.Columns.Clear();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            var detail = gridView1.GetFocusedRow() as BookBorrowDetail;
            var book = detail.Book;
            txtBookID.Text = book.Id.ToString();
            txtBookTitle.Text = book.Title;
            memoBookDes.Text = book.Description;
            txtBorrowQuantity.Text = detail.Quantity.ToString();
            txtReturnQuantity.Text = detail.Quantity.ToString();
            bool foundError = false;
            int returnQuantity = 0;
            if (txtBorrowQuantity.Text.Length != 0 && txtReturnQuantity.Text.Length != 0)
            {
                returnQuantity = int.Parse(txtReturnQuantity.Text);
                int borrowQuantity = int.Parse(txtBorrowQuantity.Text);
                if (returnQuantity > borrowQuantity)
                {
                    foundError = true;
                    dxErrorProvider1.SetError(txtReturnQuantity, "Return quantity cannot be greater than borrow quantity!");
                }
            }
            else
            {
                foundError = true;
            }
            if (foundError == false)
            {
                //no error occurs
                dxErrorProvider1.SetError(txtReturnQuantity, "");
                if (bookReturnDetails == null)
                {
                    bookReturnDetails = new List<BookReturnDetail>();
                    bookReturnDetails.Add(new BookReturnDetail
                    {
                        Book = book,
                        BookId = book.Id,
                        Price = detail.Price,
                        Quantity = returnQuantity
                    });
                    bookBorrows.SingleOrDefault(d => d.Book.Id == detail.Book.Id).Quantity -= returnQuantity;
                    LoadDataBookBorrrow(bookBorrows);
                    LoadDataBookReturn(bookReturnDetails);
                }
                else
                {
                    if (bookReturnDetails.SingleOrDefault(b => b.Book.Id == detail.Book.Id) != null)
                    {
                        //book exist
                        bookReturnDetails.SingleOrDefault(b => b.Book.Id == detail.Book.Id).Quantity += returnQuantity;
                        bookBorrows.SingleOrDefault(d => d.Book.Id == detail.Book.Id).Quantity -= returnQuantity;
                        LoadDataBookBorrrow(bookBorrows);
                        LoadDataBookReturn(bookReturnDetails);
                    }
                    else
                    {
                        //book not exist
                        bookReturnDetails.Add(new BookReturnDetail
                        {
                            Book = book,
                            BookId = book.Id,
                            Price = detail.Price,
                            Quantity = returnQuantity
                        });
                        bookBorrows.SingleOrDefault(d => d.Book.Id == detail.Book.Id).Quantity -= returnQuantity;
                        LoadDataBookBorrrow(bookBorrows);
                        LoadDataBookReturn(bookReturnDetails);
                    }
                }
                //LoadDataBookReturn();
            }
        }

        private void btnReturnBookSave_Click(object sender, EventArgs e)
        {
            var detail = gridView2.GetFocusedRow() as BookReturnDetail;

            var bookBorrow = bookBorrows.SingleOrDefault(d => d.Book.Id == detail.Book.Id);

            var bookBorrowOld = bookBorrowsOld.SingleOrDefault(b => b.BookId == detail.BookId);

            var returnBook = bookReturnDetails.SingleOrDefault(b => b.BookId == detail.BookId);

            var bookReturnDescription = memoBookReturnDescription.Text;

            if (txtBookReturnQuantity.Text.Length != 0)
            {
                int returnQuantity;
                if(Regex.Match(txtBookReturnQuantity.Text.Trim(), "^[0-9]*$").Success)
                {
                    returnQuantity = int.Parse(txtBookReturnQuantity.Text);
                    if (txtBookReturnID.Text.Length != 0)
                    {
                        if (returnQuantity > bookBorrowOld.Quantity)
                        {
                            dxErrorProvider1.SetError(txtBookReturnQuantity, "Return quantity cannot greater than the quantity in borrow order!");
                        }
                        else if (returnQuantity == 0)
                        {
                            bookReturnDetails.Remove(returnBook);
                            bookBorrow.Quantity = bookBorrow.Quantity;
                            LoadDataBookReturn(bookReturnDetails);
                            LoadDataBookBorrrow(bookBorrows);
                        }
                        else if (returnQuantity < 0)
                        {
                            dxErrorProvider1.SetError(txtBookReturnQuantity, "Quantity must be a positive number!");
                        }
                        else
                        {
                            if (returnQuantity + bookBorrow.Quantity < bookBorrowOld.Quantity)
                            {
                                //new = 0
                                dxErrorProvider1.SetError(txtBookReturnQuantity, "");
                                bookBorrow.Quantity = bookBorrowOld.Quantity - returnQuantity;
                                returnBook.Quantity = returnQuantity;
                                returnBook.Book.Description = bookReturnDescription;
                                LoadDataBookReturn(bookReturnDetails);
                                LoadDataBookBorrrow(bookBorrows);
                                //return = 1
                                //old = 2
                            } else if(returnQuantity + bookBorrow.Quantity > bookBorrowOld.Quantity)
                            {
                                dxErrorProvider1.SetError(txtBookReturnQuantity, "");
                                returnBook.Quantity = returnQuantity;
                                bookBorrow.Quantity = bookBorrowOld.Quantity - returnQuantity;
                                returnBook.Book.Description = bookReturnDescription;
                                LoadDataBookBorrrow(bookBorrows);
                                LoadDataBookReturn(bookReturnDetails);
                            }
                            else
                            {
                                dxErrorProvider1.SetError(txtBookReturnQuantity, "");
                                returnBook.Quantity = returnQuantity;
                                bookBorrow.Quantity -= returnQuantity;
                                returnBook.Book.Description = bookReturnDescription;
                                LoadDataBookBorrrow(bookBorrows);
                                LoadDataBookReturn(bookReturnDetails);
                            }
                        }
                    }
                } else
                {
                    dxErrorProvider1.SetError(txtBookReturnQuantity, "Quantity must be a number!");
                }  
            }
            else
            {
                XtraMessageBox.Show("You have not returned book yet!!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var detail = gridView1.GetFocusedRow() as BookBorrowDetail;
            var book = detail.Book;
            txtBookID.Text = book.Id.ToString();
            txtBookTitle.Text = book.Title;
            memoBookDes.Text = book.Description;
            txtBorrowQuantity.Text = detail.Quantity.ToString();
            txtReturnQuantity.Text = detail.Quantity.ToString();
        }

        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (bookReturnDetails != null)
            {
                if(bookReturnDetails.Count > 0)
                {
                    var returnDetail = gridView2.GetFocusedRow() as BookReturnDetail;
                    var book = returnDetail.Book;
                    txtBookReturnID.Text = book.Id.ToString();
                    txtBookReturnTitle.Text = book.Title;
                    txtBookReturnQuantity.Text = returnDetail.Quantity.ToString();
                    memoBookReturnDescription.Text = book.Description;
                } 
            } 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(bookReturnDetails != null)
            {
                if (bookReturnDetails.Count != 0)
                {
                    if (dateReturn.Text.Length != 0)
                    {
                        if(DateTime.Compare(dateReturn.DateTime, dateBorrow.DateTime) > 0)
                        {
                            dxErrorProvider1.SetError(dateReturn, "");
                            bool foundError = false;
                            //BookBorrowData bookBorrowData = new BookBorrowData();
                            BookBorrowDetailData bookBorrowDetailData = new BookBorrowDetailData();
                            BookReturnData bookReturnData = new BookReturnData();
                            BookReturnDetailData bookReturnDetailData = new BookReturnDetailData();
                            BookData bookData = new BookData();
                            dxErrorProvider1.SetError(dateReturn, "");
                            //update old bookBorrowDetail
                            if (!bookBorrowDetailData.updateStatusBookDetail(int.Parse(txtBorrowID.Text)))
                            {
                                foundError = true;
                            }
                            //add new bookBorrowDetail
                            if (!bookBorrowDetailData.InsertBookDetail(bookBorrows, int.Parse(txtBorrowID.Text)))
                            {
                                foundError = true;
                            }
                            //add new return book
                            BookReturn bookReturn = new BookReturn
                            {
                                DueDate = dateDue.DateTime,
                                BookReturnDetails = bookReturnDetails,
                                Student = student,
                                LibrarianID = LoginAccount.Librarian.LibrarianID,
                                ReturnDate = dateReturn.DateTime,
                            };
                            int id = bookReturnData.InsertBookReturn(bookReturn);
                            if (id > 0)
                            {
                                bookReturn.ID = id;
                                //add new returnBookDetail
                                if (!bookReturnDetailData.InsertBookReturnDetail(bookReturn))
                                {
                                    foundError = true;
                                } else
                                {
                                    if(!bookData.UpdateBookReturn(bookReturnDetails))
                                    {
                                        foundError = true;
                                    }
                                }
                            }
                            else
                            {
                                foundError = true;
                            }
                            if (foundError == true)
                            {
                                XtraMessageBox.Show("Save fail!");
                            }
                            else
                            {
                                this.DialogResult = DialogResult.OK;
                                XtraMessageBox.Show("Save successful!");
                                this.Close();
                            }
                        } else
                        {
                            dxErrorProvider1.SetError(dateReturn, "Return date connot be before the borrow date!");
                        }
                    }
                    else
                    {
                        dxErrorProvider1.SetError(dateReturn, "Return date cannot be emptied!");
                    }
                }
                else
                {
                    XtraMessageBox.Show("Your return is emptied!");
                }
            } else
            {
                XtraMessageBox.Show("Your return is emptied!");
            }

            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtBookReturnID.Text.Length != 0)
            {
                var detail = gridView2.GetFocusedRow() as BookReturnDetail;
                var book = detail.Book;
                var bookReturn = bookReturnDetails.SingleOrDefault(b => b.Book.Id == book.Id);
                var bookBorrow = bookBorrows.SingleOrDefault(b => b.Book.Id == book.Id);
                bookBorrow.Quantity = bookBorrowsOld.SingleOrDefault(b => b.Book.Id == book.Id).Quantity;
                bookReturnDetails.Remove(bookReturn);
                LoadDataBookReturn(bookReturnDetails);
                LoadDataBookBorrrow(bookBorrows);
            } else
            {
                XtraMessageBox.Show("You have not chose book return yet!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            if(bookReturnDetails.Count == 0)
            {
                txtBookReturnID.Text = "";
                txtBookReturnTitle.Text = "";
                txtBookReturnQuantity.Text = "";
                memoBookReturnDescription.Text = "";
            }
        }
    }
}