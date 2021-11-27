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
    public class StudentStatusData
    {
        string strConnection;
        public StudentStatusData()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }

        public StudentStatus GetStudentStatus (int id)
        {
            StudentStatus studentStatus = new StudentStatus();
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from StudentStatus where studentStatusID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("ID", id);
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if(read.Read())
                    {
                        studentStatus.Id = int.Parse(read["studentStatusID"].ToString());
                        studentStatus.Name = read["studentStatusName"].ToString();
                    }
                }
            } catch(SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                conn.Close();
            }
            return studentStatus;
        }

        public List<StudentStatus> GetStudentStatuses()
        {
            SqlConnection conn = new SqlConnection(strConnection);
            string sql = "select * from StudentStatus";
            SqlCommand cmd = new SqlCommand(sql, conn);
            List<StudentStatus> studentStatuses = new List<StudentStatus>();
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
                        studentStatuses.Add(new StudentStatus
                        {
                            Id = int.Parse(read["studentStatusID"].ToString()),
                            Name = read["studentStatusName"].ToString()
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
            return studentStatuses;
        }
    }
}
