using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ProjectGroup.entities;
using System.Data.SqlClient;

namespace ProjectGroup.data
{
    public class BookBorrowDetailData
    {
        string strConnection;
        public BookBorrowDetailData()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }

        public bool updateStatusBookDetail(int bookBorrowId)
        {
            bool result;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "update BookBorrowDetail set status = 'False' where BookBorrowID = @id and status = 'True'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", bookBorrowId);
            try
            {
                if(conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            } catch(SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                conn.Close();
            }
            return result;
        }

        public bool InsertBookDetail(BookBorrow b)
        {
            int test = 0;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "insert into BookBorrowDetail(BookID, BookBorrowID, price, quantity, status) values(@bookID,@bookBorrowID,@price,@quantity, @status)";
            //SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {   
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
        
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandText = sql;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("bookID", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("bookBorrowID", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("price", System.Data.SqlDbType.Float));
                        cmd.Parameters.Add(new SqlParameter("quantity", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("status", System.Data.SqlDbType.Bit));
                        foreach (var detail in b.ListBookBorrowDetail)
                        {
                            cmd.Parameters[0].Value = detail.Book.Id;
                            cmd.Parameters[1].Value = b.Id;
                            cmd.Parameters[2].Value = detail.Price;
                            cmd.Parameters[3].Value = detail.Quantity;
                            cmd.Parameters[4].Value = true;
                            test += cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    
                }
            } catch (SqlException e )
            {
                throw new Exception(e.Message);
            } finally
            {
                conn.Close();
            }
            return test > 0;
        }

        public bool InsertBookDetail(List<BookBorrowDetail> b, int bookBorrowID)
        {
            int test = 0;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "insert into BookBorrowDetail(BookID, BookBorrowID, price, quantity, status) values(@bookID,@bookBorrowID,@price,@quantity, @status)";
            //SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandText = sql;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("bookID", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("bookBorrowID", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("price", System.Data.SqlDbType.Float));
                        cmd.Parameters.Add(new SqlParameter("quantity", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("status", System.Data.SqlDbType.Bit));
                        foreach (var detail in b)
                        {
                            cmd.Parameters[0].Value = detail.Book.Id;
                            cmd.Parameters[1].Value = bookBorrowID;
                            cmd.Parameters[2].Value = detail.Price;
                            cmd.Parameters[3].Value = detail.Quantity;
                            cmd.Parameters[4].Value = true;
                            test += cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
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
            return test > 0;
        }

        public List<BookBorrowDetail> GetBookBorrowDetailsByBorrowID(int id)
        {
            List<BookBorrowDetail> bookBorrowDetails = new List<BookBorrowDetail>();
            BookData bookData = new BookData();
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from BookBorrowDetail where BookBorrowID = @id and status = @status";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("status", true);
            try
            {
                if(conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        bookBorrowDetails.Add(new BookBorrowDetail
                        {
                            Book = bookData.GetBookById(int.Parse(read["BookID"].ToString())),
                            BookBorrowID = int.Parse(read["BookBorrowID"].ToString()),
                            Price = double.Parse(read["price"].ToString()),
                            Quantity = int.Parse(read["quantity"].ToString()),
                            Id = int.Parse(read["id"].ToString()),
                            BookId = int.Parse(read["BookID"].ToString()),
                            Status = bool.Parse(read["status"].ToString())
                        });
                    }
                }
            } catch (SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                conn.Close();
            }
            return bookBorrowDetails;
        }
    }
}
