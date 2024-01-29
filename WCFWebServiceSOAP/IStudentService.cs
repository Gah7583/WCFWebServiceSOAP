using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFWebServiceSOAP
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IService1" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IStudentService
    {
        [OperationContract]
        void AddStudent(Student student);

        [OperationContract]
        DataSet GetStudents();

        [OperationContract]
        Student GetStudentById(int id);

        [OperationContract]
        void UpdateStudent(Student student);

        [OperationContract]
        void DeleteStudent(int id);
    }


    // Use um contrato de dados como ilustrado no exemplo abaixo para adicionar tipos compostos a operações de serviço.
    [DataContract]
    public class Student
    {
        public Student()
        {
        }

        public Student(int id, string name, string roll)
        {
            Id = id;
            Name = name;
            Roll = roll;
        }

        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Roll { get; set; }
    }
}
