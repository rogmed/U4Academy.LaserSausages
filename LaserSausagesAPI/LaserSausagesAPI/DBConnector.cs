using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace LaserSausagesAPI
{
    public class DBConnector : IDBConnector
    {
        private readonly string ConnectionString;

        public DBConnector(string ConnectionString)
        {
            if (ConnectionString == null)
                throw new ArgumentNullException(ConnectionString);

            this.ConnectionString = ConnectionString;
        }

        public TableServiceClient serviceClient()
        {
            return new TableServiceClient(ConnectionString);
        }

        public TableClient TableClient(string TableName)
        {
            return new TableClient(this.ConnectionString, TableName);
        }

        public List<Student> GetStudents()
        {
            Pageable<Student> queryResultsFilter = TableClient("Student").
                Query<Student>(filter: $"RowKey ge '0'");

            return queryResultsFilter.ToList<Student>();
        }

        public Student GetStudentById(string id)
        {
            Pageable<Student> queryResultsFilter = TableClient("Student").
                        Query<Student>(filter: $"RowKey eq '{id}'");

            return queryResultsFilter.First<Student>();
        }

        public bool CreateStudent(Student student)
        {
            var response = TableClient("Student").AddEntity<Student>(student);

            return !response.IsError;
        }

        public bool DeleteStudent(Student student)
        {
            var tableClient = TableClient("Student");
            var response = tableClient.DeleteEntity(student.PartitionKey, student.RowKey);
            if (response == null || response.IsError)
                return false;
  
            return true;
        }

        public bool UpdateStudent(Student student)
        {
            var tableClient = TableClient("Student").UpdateEntity<Student>
                (student, ETag.All, TableUpdateMode.Replace);

            return !tableClient.IsError;
        }
    }
}
