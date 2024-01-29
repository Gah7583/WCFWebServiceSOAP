using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace WCFWebServiceSOAP
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "Service1" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione Service1.svc ou Service1.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class StudentService : IStudentService
    {
        private readonly MySqlConnection _connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
        public void AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }
            using (MySqlCommand cmd = new MySqlCommand("proc_InsertStudent", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@varName", student.Name);
                cmd.Parameters.AddWithValue("@varRoll", student.Roll);
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int id)
        {
            using(MySqlCommand cmd = new MySqlCommand("proc_DeleteStudent", _connection))
            {
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@varIdStudent", id);
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public Student GetStudentById(int id)
        {
            using (MySqlCommand cmd = new MySqlCommand("proc_SelectStudent", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@varIdStudent", id);
                cmd.Parameters.Add("@varName", MySqlDbType.VarChar, 40).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@varRoll", MySqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                cmd.ExecuteNonQuery();

                return new Student(Convert.ToInt32(cmd.Parameters["@varIdStudent"].Value),cmd.Parameters["@varName"].Value.ToString(), cmd.Parameters["@varRoll"].Value.ToString());
            }
        }

        public DataSet GetStudents()
        {
            using (MySqlCommand cmd = new MySqlCommand("proc_SelectAllStudents", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }

        public void UpdateStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }
            using (MySqlCommand cmd = new MySqlCommand("proc_UpdateStudent", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@varIdStudent", student.Id);
                cmd.Parameters.AddWithValue("@varName", student.Name);
                cmd.Parameters.AddWithValue("@varRoll", student.Roll);
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }
    }
}
