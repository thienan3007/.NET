using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGroup.entities;

namespace ProjectGroup.data
{
    public class AuthorData
    {
        string strConnection;
        public AuthorData()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }

        public DataTable GetAuthorsTable()
        {
            string SQL = "select * from Author";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtAuthor = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                da.Fill(dtAuthor);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally { cnn.Close(); }
            return dtAuthor;
        }

        public List<Author> GetAuthors()
        {
            string SQL = "select * from Author";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            List<Author> listAuthor = new List<Author>();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        listAuthor.Add(new Author
                        {
                            AuthorID = int.Parse(read["id"].ToString()),
                            AuthorName = read["name"].ToString(),
                            Email = read["email"].ToString(),
                            Phone = read["phone"].ToString()
                        });
                    }
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally { cnn.Close(); }
            return listAuthor;
        }


        public Author GetAuthor(int id)
        {
            string SQL = "select * from Author where id = @ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("ID", id);
            Author author = new Author();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        author.AuthorID = id;
                        author.AuthorName = (read["name"].ToString());
                        author.Email = (read["email"].ToString());
                        author.Phone = (read["phone"].ToString());
                    }
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally { cnn.Close(); }
            return author;
        }


        public bool AddAuthor(Author s)
        {
            bool result;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Insert Author values(@name,@email,@phone)";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@name", s.AuthorName);
            cmd.Parameters.AddWithValue("@email", s.Email);
            cmd.Parameters.AddWithValue("@phone", s.Phone);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool UpdateAuthor(Author s)
        {
            bool result;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Update Author set name=@name,Email=@email,Phone=@phone where id=@id";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@id", s.AuthorID);
            cmd.Parameters.AddWithValue("@name", s.AuthorName);
            cmd.Parameters.AddWithValue("@email", s.Email);
            cmd.Parameters.AddWithValue("@phone", s.Phone);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool DeleteAuthor(int AuthorID)
        {
            bool result;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Delete Author where id=@id";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@id", AuthorID);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
