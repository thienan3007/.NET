using ProjectGroup.entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjectGroup.data
{
    public class CategoryData
    {
        string strConnection;
        public CategoryData()
        {
            getConnectionString();
        }
        public string getConnectionString ()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }

        public Category GetCategory(int id)
        {
            string SQL = "select * from Category where categoryID = @ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("ID", id);
            Category cat = new Category();
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
                        cat.CategoryID = int.Parse(read["categoryID"].ToString());
                        cat.CategoryName = read["categoryName"].ToString();
                    }
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally { cnn.Close(); }
            return cat;
        }

        public DataTable GetCategoriesTable()
        {
            DataTable dtCat = new DataTable();
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from Category";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                da.Fill(dtCat);
            } catch(SqlException e)
            {
                throw new Exception(e.Message);
            } finally { conn.Close(); }
            return dtCat;
        }
        
        public List<Category> GetCategories()
        {
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from Category";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            List<Category> listCate = new List<Category>();
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
                        listCate.Add(new Category
                        {
                            CategoryID = int.Parse(read["categoryId"].ToString()),
                            CategoryName = read["categoryName"].ToString()
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
            return listCate;
        }
    }
}
