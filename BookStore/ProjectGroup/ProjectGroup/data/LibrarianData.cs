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
    public class LibrarianData
    {
        string strConnection;
        public LibrarianData()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }
        public DataTable GetLibrarianTable()
        {
            string SQL = "select * from Librarian";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtLibrarian = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                da.Fill(dtLibrarian);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally { cnn.Close(); }
            return dtLibrarian;
        }

        public Librarian Login(string userName, string password)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "select * from Librarian where " +
                " id = @id and Password = @password";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@id", userName);
            cmd.Parameters.AddWithValue("@password", password);
            try
            {
                if(cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if(read.Read())
                    {
                        return new Librarian
                        {
                            Address = read["address"].ToString(),
                            LibrarianName = read["name"].ToString(),
                            Email = read["email"].ToString(),
                            Phone = read["phone"].ToString(),
                            LibrarianID = int.Parse(read["id"].ToString()),
                            Status = GetLibrarianStatus(int.Parse(read["statusID"].ToString())),
                            StatusID = int.Parse(read["statusID"].ToString())
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return null;
        }

        public LibrarianStatus GetLibrarianStatus(int librarianID)
        {
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from LibrarianStatus where librarianStatusID = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", librarianID);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        return new LibrarianStatus
                        {
                            Id = int.Parse(read["librarianStatusId"].ToString()),
                            Name = read["librarianStatusName"].ToString()
                        };
                    }
                }
            } catch(SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                conn.Close();
            }
            return null;
        }

        public Librarian GetLibrarianById(int id)
        {
            string SQL = "select * from Librarian where id = @ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("ID", id);
            Librarian librarian = new Librarian();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        librarian.LibrarianID = int.Parse(read["id"].ToString());
                        librarian.LibrarianName = read["name"].ToString();
                        librarian.Address = read["address"].ToString();
                        librarian.Email = read["email"].ToString();
                        librarian.Phone = read["phone"].ToString();
                    }
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally { cnn.Close(); }
            return librarian;
        }

        public List<Librarian> GetLibrarian()
        {
            string SQL = "select * from Librarian";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            List<Librarian> librarians = new List<Librarian>();
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
                        librarians.Add(new Librarian
                        {
                            LibrarianID = int.Parse(read["id"].ToString()),
                            LibrarianName = read["name"].ToString(),
                            Address = read["address"].ToString(),
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
            return librarians;
        }
    }
}
