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
    public class BookReturnDetailData
    {
        string strConnection;
        public BookReturnDetailData()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }
        public bool InsertBookReturnDetail(BookReturn b)
        {
            int test = 0;
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "insert into BookReturnDetail(BookReturnID, BookID, price, quantity) values(@bookReturnID, @bookId, @price, @quantity)";
            //SqlCommand cmd = conn.CreateCommand();
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("bookReturnID", SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("bookId", SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("price", SqlDbType.Float));
                        cmd.Parameters.Add(new SqlParameter("quantity", SqlDbType.Int));
                        foreach(var detail in b.BookReturnDetails)
                        {
                            cmd.Parameters[0].Value = b.ID;
                            cmd.Parameters[1].Value = detail.Book.Id;
                            cmd.Parameters[2].Value = detail.Price;
                            cmd.Parameters[3].Value = detail.Quantity;
                            test += cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }
            } catch(SqlException e)
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
