using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using ProjectGroup.entities;
using ProjectGroup.data;
using System.Diagnostics;

namespace ProjectGroup.data
{
    public class BookData
    {
        string strConnection;
        public BookData()
        {
            getConnectionString();
        }

        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }

        //get all books 
        public DataTable GetBooksTable()
        {
            string sql = "select * from Book";
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtBook = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                da.Fill(dtBook);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return dtBook;
        }

        public List<Book> GetBooks()
        {
            string sql = "select * from Book";
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, conn);
            List<Book> listBook = new List<Book>();
            AuthorData authorData = new AuthorData();
            CategoryData categoryData = new CategoryData();
            StatusBookData statusBookData = new StatusBookData();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using(SqlDataReader read = cmd.ExecuteReader())
                {
                    while(read.Read())
                    {
                        listBook.Add(new Book { 
                            Id = int.Parse(read["id"].ToString()),
                            Title = read["title"].ToString(),
                            Language = read["language"].ToString(),
                            NoOfPages = int.Parse(read["noOfPages"].ToString()),
                            Author = authorData.GetAuthor(int.Parse(read["authorID"].ToString())),
                            AuthorID = int.Parse(read["authorID"].ToString()),
                            Category = categoryData.GetCategory(int.Parse(read["categoryID"].ToString())),
                            CategoryID = int.Parse(read["categoryID"].ToString()),
                            Description = read["description"].ToString(),
                            QuantityLeft = int.Parse(read["quantityLeft"].ToString()),
                            Status = statusBookData.GetBookStatus(int.Parse(read["statusID"].ToString())),
                            StatusId = int.Parse(read["statusID"].ToString())
                        });
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
            return listBook;
        }

        public Book GetBookById(int id)
        {
            string sql = "select * from Book where id = @id";
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);
            AuthorData authorData = new AuthorData();
            CategoryData categoryData = new CategoryData();
            StatusBookData statusBookData = new StatusBookData();
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
                        return new Book
                        {
                            Id = int.Parse(read["id"].ToString()),
                            Title = read["title"].ToString(),
                            Language = read["language"].ToString(),
                            NoOfPages = int.Parse(read["noOfPages"].ToString()),
                            Author = authorData.GetAuthor(int.Parse(read["authorID"].ToString())),
                            AuthorID = int.Parse(read["authorID"].ToString()),
                            Category = categoryData.GetCategory(int.Parse(read["categoryID"].ToString())),
                            CategoryID = int.Parse(read["categoryID"].ToString()),
                            Description = read["description"].ToString(),
                            QuantityLeft = int.Parse(read["quantityLeft"].ToString()),
                            Status = statusBookData.GetBookStatus(int.Parse(read["statusID"].ToString())),
                            StatusId = int.Parse(read["statusID"].ToString())
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

        public List<Book> GetBooksByTitle(string bookTitle)
        {
            string sql = "select * from Book where title like @title";
            SqlConnection conn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, conn);
            List<Book> listBook = new List<Book>();
            AuthorData authorData = new AuthorData();
            CategoryData categoryData = new CategoryData();
            StatusBookData statusBookData = new StatusBookData();
            cmd.Parameters.AddWithValue("title", "%" + bookTitle + "%");
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
                        listBook.Add(new Book
                        {
                            Id = int.Parse(read["id"].ToString()),
                            Title = read["title"].ToString(),
                            Language = read["language"].ToString(),
                            NoOfPages = int.Parse(read["noOfPages"].ToString()),
                            Author = authorData.GetAuthor(int.Parse(read["authorID"].ToString())),
                            AuthorID = int.Parse(read["authorID"].ToString()),
                            Category = categoryData.GetCategory(int.Parse(read["categoryID"].ToString())),
                            CategoryID = int.Parse(read["categoryID"].ToString()),
                            Description = read["description"].ToString(),
                            QuantityLeft = int.Parse(read["quantityLeft"].ToString()),
                            Status = statusBookData.GetBookStatus(int.Parse(read["statusID"].ToString())),
                            StatusId = int.Parse(read["statusID"].ToString())
                        });
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
            return listBook;
        }

        //add book
        public bool AddBook(Book b)
        {
            bool result;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "insert Book(title, authorID, categoryID, language, noOfPages, quantityLeft,description,statusID) " +
                "values(@Title, @AuthorID, @CategoryID, @Language, @NoOfPages, @QuantityLeft,@Description,@StatusID)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Title", b.Title);
            cmd.Parameters.AddWithValue("AuthorID", b.Author.AuthorID);
            cmd.Parameters.AddWithValue("CategoryID", b.Category.CategoryID);
            cmd.Parameters.AddWithValue("Language", b.Language);
            cmd.Parameters.AddWithValue("NoOfPages", b.NoOfPages);
            cmd.Parameters.AddWithValue("QuantityLeft", b.QuantityLeft);
            cmd.Parameters.AddWithValue("Description", b.Description);
            cmd.Parameters.AddWithValue("StatusID", b.Status.ID);
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        //update book
        public bool UpdateBook(Book b)
        {
            bool result;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "update Book set title=@Title, authorID=@AuthorID, categoryID=@CategoryID, language=@Language, noOfPages=@NoOfPages, quantityLeft=@QuantityLeft,description=@Description,statusID=@StatusID where id = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Title", b.Title);
            cmd.Parameters.AddWithValue("AuthorID", b.Author.AuthorID);
            cmd.Parameters.AddWithValue("CategoryID", b.Category.CategoryID);
            cmd.Parameters.AddWithValue("Language", b.Language);
            cmd.Parameters.AddWithValue("NoOfPages", b.NoOfPages);
            cmd.Parameters.AddWithValue("QuantityLeft", b.QuantityLeft);
            cmd.Parameters.AddWithValue("Description", b.Description);
            cmd.Parameters.AddWithValue("StatusID", b.Status.ID);
            cmd.Parameters.AddWithValue("ID", b.Id);
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            } catch(SqlException e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        public bool DeleteBook(int id)
        {
            bool result;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "delete Book where id = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("ID", id);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        public bool UpdateBook(List<BookBorrowDetail> bookBorrowDetails)
        {
            int test = 0;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "update Book set quantityLeft = quantityLeft - @quantity where id =  @id";
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using(SqlTransaction transaction = conn.BeginTransaction())
                {
                    using(SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Transaction = transaction;
                        cmd.Parameters.Add(new SqlParameter("quantity", SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int));
                        foreach(var borrow in bookBorrowDetails)
                        {
                            cmd.Parameters[0].Value = borrow.Quantity;
                            cmd.Parameters[1].Value = borrow.Book.Id;
                            test += cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }
            } catch(SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                conn.Close();
            }
            return test > 0;
        }

        public bool UpdateBookReturn(List<BookReturnDetail> bookReturnDetails)
        {
            int test = 0;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "update Book set quantityLeft = quantityLeft + @quantity where id =  @id";
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Transaction = transaction;
                        cmd.Parameters.Add(new SqlParameter("quantity", SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int));
                        foreach (var returnBook in bookReturnDetails)
                        {
                            cmd.Parameters[0].Value = returnBook.Quantity;
                            cmd.Parameters[1].Value = returnBook.Book.Id;
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
    }
}
