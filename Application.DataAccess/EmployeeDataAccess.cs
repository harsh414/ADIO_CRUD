using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using System.Data.SqlClient;

namespace Application.DataAccess
{
    public class EmployeeDataAccess : IDataAccess<Employee, int>
    {
        SqlConnection Conn;
        SqlCommand Cmd;

        public EmployeeDataAccess()
        {
            Conn = new SqlConnection("Data Source=IN-9RVTJM3;Initial Catalog=Ucompany;Integrated Security=SSPI");
        }

        Employee IDataAccess<Employee, int>.Create(Employee entity)
        {
            Employee employee= new Employee();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"Insert into Employee Values({entity.EmpNo}, '{entity.EmpName}', '{entity.Designation}', {entity.Salary},{entity.DeptNo})";
                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }
            return employee;
        }

        Employee IDataAccess<Employee, int>.Delete(int id)
        {
            Employee employee= null;
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"Delete from Employee where EmpNo={id}";
                int result = Cmd.ExecuteNonQuery ();
                if (result != 0)
                {
                   
                    employee = new Employee();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }

            return employee;
        }

        IEnumerable<Employee> IDataAccess<Employee, int>.Get()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                
                Conn.Open();
                Cmd = new SqlCommand();
                
                Cmd.Connection = Conn;  
           
                Cmd.CommandType = System.Data.CommandType.Text;
            
                Cmd.CommandText = "Select * from Employee";
              
                SqlDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                   
                    employees.Add(new Employee()
                    { 
                      EmpNo = Convert.ToInt32(Reader["EmpNo"]),
                      EmpName = Reader["EmpName"].ToString(),
                      Designation = Reader["Designation"].ToString(),
                      Salary = Convert.ToInt32(Reader["Salary"]),
                      DeptNo = Convert.ToInt32(Reader["DeptNo"])
                    });
                }
               
                Reader.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                Conn.Close();
            }
            return employees;
        }

        Employee IDataAccess<Employee, int>.Get(int id)
        {
            Employee employee= null;
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"Select * from Employee where EmpNo={id}";
                SqlDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    employee = new Employee()
                    {
                      EmpNo = Convert.ToInt32(Reader["EmpNo"]),
                      EmpName = Reader["EmpName"].ToString(),
                      Designation = Reader["Designation"].ToString(),
                      Salary = Convert.ToInt32(Reader["Salary"]),
                      DeptNo = Convert.ToInt32(Reader["DeptNo"])
                    };
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {Conn.Close(); }
            return employee;
        }

        Employee IDataAccess<Employee, int>.Update(int id, Employee entity)
        {
            Employee employee= new Employee();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"Update Department Set DeptName ='{entity.EmpName}', Location='{entity.Designation}', Capacity={entity.Salary}, DeptNo={entity.DeptNo} where DeptNo={entity.DeptNo}";
                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }
            return employee;
        }
    }
}
