using ProjectGroup.entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGroup.data
{
        public class StudentData
    {
        string strConnection;
        public StudentData()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }
        public DataTable GetStudentTable()
        {
            string SQL = "select * from Student";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtStudent = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                da.Fill(dtStudent);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally { cnn.Close(); }
            return dtStudent;
        }

        public List<Student> GetStudent()
        {
            string SQL = "select * from Student";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            List<Student> listStudent = new List<Student>();
            StudentStatusData studentStatusData = new StudentStatusData();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                using(SqlDataReader read = cmd.ExecuteReader())
                {
                    while(read.Read())
                    {
                        listStudent.Add(new Student
                        {
                            StudentID = int.Parse(read["id"].ToString()),
                            StudentName = read["name"].ToString(),
                            Email = read["email"].ToString(),
                            Phone = read["phone"].ToString(),
                            Address = read["address"].ToString(),
                            Status = studentStatusData.GetStudentStatus(int.Parse(read["statusID"].ToString())),
                            StatusID = int.Parse(read["statusID"].ToString())
                        });
                    }
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally { cnn.Close(); }
            return listStudent;
        }
        public Student GetStudentById(int id)
        {
            string SQL = "select * from Student where id=@id";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("id", id);
            StudentStatusData studentStatusData = new StudentStatusData();
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
                        return new Student
                        {
                            StudentID = int.Parse(read["id"].ToString()),
                            StudentName = read["name"].ToString(),
                            Email = read["email"].ToString(),
                            Phone = read["phone"].ToString(),
                            Address = read["address"].ToString(),
                            Status = studentStatusData.GetStudentStatus(int.Parse(read["statusID"].ToString())),
                            StatusID = int.Parse(read["statusID"].ToString())
                        };
                    }
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally { cnn.Close(); }
            return null;
        }

        public int AddStudent(Student s)
        {
            int result;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Insert into Student output inserted.ID values(@name,@email,@phone,@address,@statusID)";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@name", s.StudentName);
            cmd.Parameters.AddWithValue("@email", s.Email);
            cmd.Parameters.AddWithValue("@phone", s.Phone);
            cmd.Parameters.AddWithValue("@address", s.Address);
            cmd.Parameters.AddWithValue("@statusID",1);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                result = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            
            return result;
        }

        public bool UpdateStudent(Student s)
        {
            bool result;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Update student set name=@name,Phone=@phone,Email=@email,Address=@address,StatusID=@statusID where id=@id";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@id", s.StudentID);
            cmd.Parameters.AddWithValue("@name", s.StudentName);
            cmd.Parameters.AddWithValue("@email", s.Email);
            cmd.Parameters.AddWithValue("@phone", s.Phone);
            cmd.Parameters.AddWithValue("@address", s.Address);
            cmd.Parameters.AddWithValue("@statusID", s.Status.Id);
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

        public bool DeleteStudent(int StudentID)
        {
            bool result;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Delete Student where id=@id";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@id", StudentID);
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
