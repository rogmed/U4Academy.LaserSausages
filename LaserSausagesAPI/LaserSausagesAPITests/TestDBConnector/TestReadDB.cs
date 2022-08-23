using LaserSausagesAPI;

namespace LaserSausagesAPITests.TestDBConnector
{
    [TestClass]
    public class TestReadDB
    {
        readonly DBConnector dbConnector = new DBConnector("DefaultEndpointsProtocol=https;AccountName=lasersausage;AccountKey=/Kq0wUdk7SdN8oKqy7GLjHvAcqdn6Fu1qLhw7XD/m/CN9imhrv6AmJc3lc2Zi9yVautpd0LRreJvBbTDHyXgIQ==;BlobEndpoint=https://lasersausage.blob.core.windows.net/;TableEndpoint=https://lasersausage.table.core.windows.net/;QueueEndpoint=https://lasersausage.queue.core.windows.net/;FileEndpoint=https://lasersausage.file.core.windows.net/");

        [TestMethod]
        public void GetStudents()
        {
            var students = dbConnector.GetStudents();

            Assert.IsNotNull(students);
            Assert.IsInstanceOfType(students, typeof(List<Student>));
        }

        [TestMethod]
        public void GetStudentById()
        {
            var student = dbConnector.GetStudentById("e88fd3434bdc4e9eb704ceab40c8b20f");

            Assert.IsNotNull(student);
            Assert.IsInstanceOfType(student, typeof(Student));
            Assert.AreEqual("TEST read", student.PartitionKey);
            Assert.AreEqual("e88fd3434bdc4e9eb704ceab40c8b20f", student.RowKey);
        }
    }
}
