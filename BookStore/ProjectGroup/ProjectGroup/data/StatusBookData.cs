using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ProjectGroup.entities;
using System.Data;
using System.Data.SqlClient;

namespace ProjectGroup.data
{
    public class StatusBookData
    {
        string strConnection;
        public StatusBookData()
        {
            getConnectString();
        }
        public string getConnectString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }

        public List<BookStatus> getStatusBook()
        {
            List<BookStatus> listStatus = new List<BookStatus>();
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from BookStatus";
            SqlCommand cmd = new SqlCommand(sql, conn);
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
                        listStatus.Add(new BookStatus
                        {
                            ID = int.Parse(read["bookStatusID"].ToString()),
                            StatusName = read["bookStatusName"].ToString()
                        });
                    }
                }
            }
            catch (SqlException e) {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return listStatus;
        }

        public DataTable getStatusBookTable()
        {
            DataTable dtStatus = new DataTable();
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from BookStatus";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                da.Fill(dtStatus);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return dtStatus;
        }
        public BookStatus GetBookStatus(int id)
        {
            string SQL = "select * from BookStatus where bookStatusID = @ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("ID", id);
            BookStatus bookStatus = new BookStatus();
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
                        bookStatus.ID = int.Parse(read["bookStatusID"].ToString());
                        bookStatus.StatusName = read["bookStatusName"].ToString();
                    }
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally { cnn.Close(); }
            return bookStatus;
        }

    }
}
