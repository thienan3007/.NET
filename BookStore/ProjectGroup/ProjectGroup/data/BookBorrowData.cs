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
    public class BookBorrowData
    {
        string strConnection;
        public BookBorrowData()
        {
            getConnectionString();
        }
        private string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }

        public int InsertBookBorrow(BookBorrow b)
        {
            int result;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "insert into BookBorrow(librarianID, studentId, totalPrice, dueDate, borrowDate, status) output inserted.id values(@librarianID, @studentID, @totalPrice, @dueDate, @borrowDate, @status)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("librarianID", b.LibrarianID);
            cmd.Parameters.AddWithValue("studentID", b.StudentID);
            cmd.Parameters.AddWithValue("totalPrice", b.TotalPrice);
            cmd.Parameters.AddWithValue("dueDate", b.DueDate);
            cmd.Parameters.AddWithValue("borrowDate", b.BorrowDate);
            cmd.Parameters.AddWithValue("status", true);
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = (int)cmd.ExecuteScalar();
            } catch (SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                conn.Close();
            }
            return result;
        }

        public List<BookBorrow> GetBookBorrows()
        {
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from BookBorrow";
            SqlCommand cmd = new SqlCommand(sql, conn);
            StudentData studentData = new StudentData();
            LibrarianData librarianData = new LibrarianData();
            List<BookBorrow> bookBorrows = new List<BookBorrow>();
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while(read.Read())
                    {
                        bookBorrows.Add(new BookBorrow
                        {
                            Id = int.Parse(read["id"].ToString()),
                            //BorrowDate = DateTime.ParseExact(read["borrowDate"].ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
                            BorrowDate = Convert.ToDateTime(read["borrowDate"].ToString()),
                            //DueDate = DateTime.ParseExact(read["dueDate"].ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
                            DueDate = Convert.ToDateTime(read["dueDate"].ToString()),
                            TotalPrice = double.Parse(read["totalPrice"].ToString()),
                            Student = studentData.GetStudentById(int.Parse(read["studentID"].ToString())),
                            StudentID = int.Parse(read["studentID"].ToString()),
                            LibrarianID = int.Parse(read["librarianID"].ToString()),
                            Librarian = librarianData.GetLibrarianById(int.Parse(read["librarianID"].ToString())),
                            Status = bool.Parse(read["status"].ToString())
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
            return bookBorrows;
        }

        public BookBorrow GetBookBorrowById(int id)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from BookBorrow where id = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);
            StudentData studentData = new StudentData();
            LibrarianData librarianData = new LibrarianData();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        return new BookBorrow
                        {
                            Id = int.Parse(read["id"].ToString()),
                            BorrowDate = Convert.ToDateTime(read["borrowDate"].ToString()),
                            DueDate = Convert.ToDateTime(read["dueDate"].ToString()),
                            TotalPrice = double.Parse(read["totalPrice"].ToString()),
                            Student = studentData.GetStudentById(int.Parse(read["studentID"].ToString())),
                            StudentID = int.Parse(read["studentID"].ToString()),
                            LibrarianID = int.Parse(read["librarianID"].ToString()),
                            Librarian = librarianData.GetLibrarianById(int.Parse(read["librarianID"].ToString())),
                            Status = bool.Parse(read["status"].ToString())
                        };
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return null;
        }
    }
}
