using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ProjectGroup.entities;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ProjectGroup.data
{
    public class BookReturnData
    {
        string strConnection;
        public BookReturnData()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }
        public int InsertBookReturn(BookReturn b)
        {
            int result;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "insert into BookReturn(dueDate, librarianID, returnDate, studentID) output inserted.id values(@duedate, @librarianID, @returnDate, @studentID)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("duedate", b.DueDate);
            cmd.Parameters.AddWithValue("librarianID", b.LibrarianID);
            cmd.Parameters.AddWithValue("returnDate", b.ReturnDate);
            cmd.Parameters.AddWithValue("studentID", b.Student.StudentID);
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = (int)cmd.ExecuteScalar();
            } catch(SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                conn.Close();
            }
            return result;
        }

        public List<BookReturnDetail> GetBookReturnDetailsByReturnBookID(int returnBookID)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = " select * from BookReturnDetail where BookReturnID = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            List<BookReturnDetail> bookReturnDetails = new List<BookReturnDetail>();
            BookData bookData = new BookData();
            cmd.Parameters.AddWithValue("id", returnBookID);
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using(SqlDataReader read = cmd.ExecuteReader())
                {
                    while(read.Read())
                    {
                        bookReturnDetails.Add(new BookReturnDetail
                        {
                            BookId = int.Parse(read["BookID"].ToString()),
                            BookReturnID = int.Parse(read["BookReturnId"].ToString()),
                            ID = int.Parse(read["id"].ToString()),
                            Quantity = int.Parse(read["quantity"].ToString()),
                            Price = double.Parse(read["price"].ToString()),
                            Book = bookData.GetBookById(int.Parse(read["BookID"].ToString()))
                        });
                    }
                }
            } catch(SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                conn.Close();
            }
            return bookReturnDetails;
        }

        public List<BookReturn> GetBookReturns()
        {
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from BookReturn";
            SqlCommand cmd = new SqlCommand(sql, conn);
            List<BookReturn> listBookReturn = new List<BookReturn>();
            StudentData studentData = new StudentData();
            LibrarianData librarianData = new LibrarianData();
            try
            {
                if(conn.State ==ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while(read.Read())
                    {
                        listBookReturn.Add(new BookReturn
                        {
                            //DueDate = DateTime.ParseExact(read["dueDate"].ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
                            DueDate = Convert.ToDateTime(read["dueDate"].ToString()),
                            //ReturnDate = DateTime.ParseExact(read["returnDate"].ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
                            ReturnDate = Convert.ToDateTime(read["returnDate"].ToString()),
                            ID = int.Parse(read["id"].ToString()),
                            Student = studentData.GetStudentById(int.Parse(read["studentID"].ToString())),
                            StudentID = int.Parse(read["studentID"].ToString()),
                            Librarian = librarianData.GetLibrarianById(int.Parse(read["librarianID"].ToString())),
                            LibrarianID = int.Parse(read["librarianID"].ToString()),
                            BookReturnDetails = GetBookReturnDetailsByReturnBookID(int.Parse(read["id"].ToString()))
                        });
                    }
                }
            } catch(SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                conn.Close();
            }
            return listBookReturn;
        }
    }
}
