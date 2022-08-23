using Azure.Data.Tables;
using Azure;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LaserSausagesAPI
{  
    public class Student : ITableEntity
    {
        public string PartitionKey { get; set; }
        
        public string RowKey { get; set; }
        [IgnoreDataMember]
        public DateTimeOffset? Timestamp { get; set; }
        [IgnoreDataMember]
        public ETag ETag { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Course { get; set; }
        public DateTime FinishDate { get; set; }

        public Student()
        {
            this.PartitionKey = "";
            this.RowKey = Guid.NewGuid().ToString("N");
            this.FirstName = "";
            this.SurName = "";
            this.BirthDate = DateTime.UtcNow;
            this.Course = "";
            this.FinishDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public Student(string partitionKey, string firstName,
            string surName, DateTime birthDate, string course, DateTime finishDate)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = Guid.NewGuid().ToString("N");
            this.FirstName = firstName;
            this.SurName = surName;
            this.BirthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
            this.Course = course;
            this.FinishDate = DateTime.SpecifyKind(finishDate, DateTimeKind.Utc);
        }

        public Student Clone()
        {
            Student newStudent = new Student();
            newStudent.PartitionKey = PartitionKey;
            newStudent.RowKey = RowKey;
            newStudent.FirstName = FirstName;
            newStudent.SurName = SurName;
            newStudent.BirthDate = BirthDate;
            newStudent.Course = Course;
            newStudent.FinishDate = FinishDate;

            return newStudent;
        }
    }
}

