using LaserSausagesAPI;

namespace LaserSausagesAPITests.TestDBConnector
{
    [TestClass]
    public class TestUpdateDB
    {
        DBConnector dbConnector = new DBConnector("DefaultEndpointsProtocol=https;AccountName=lasersausage;AccountKey=/Kq0wUdk7SdN8oKqy7GLjHvAcqdn6Fu1qLhw7XD/m/CN9imhrv6AmJc3lc2Zi9yVautpd0LRreJvBbTDHyXgIQ==;BlobEndpoint=https://lasersausage.blob.core.windows.net/;TableEndpoint=https://lasersausage.table.core.windows.net/;QueueEndpoint=https://lasersausage.queue.core.windows.net/;FileEndpoint=https://lasersausage.file.core.windows.net/");
        string TestUpdateStudentId = "23e2f638a3c14742a965629e9189d90a";

        [TestMethod]
        public void UpdateStudentTest()
        {
            //Arrange
            Student student = dbConnector.GetStudentById(TestUpdateStudentId);
            Student originalStudent = student.Clone();

            student.FirstName = "John";
            student.SurName = "Smith";
            DateTime now = DateTime.UtcNow;
            student.BirthDate = now;
            student.FinishDate = now;
            student.Course = "New course";

            //Act
            dbConnector.UpdateStudent(student);

            Student modifiedStudent = dbConnector.GetStudentById(TestUpdateStudentId);

            //Assert
            Assert.AreEqual(student.FirstName, modifiedStudent.FirstName);
            Assert.AreEqual(student.SurName, modifiedStudent.SurName);
            Assert.AreEqual(student.BirthDate, modifiedStudent.BirthDate);
            Assert.AreEqual(student.FinishDate, modifiedStudent.FinishDate);
            Assert.AreEqual(student.Course, modifiedStudent.Course);

            dbConnector.UpdateStudent(originalStudent);
        }
    }
}
