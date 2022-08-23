using LaserSausagesAPI;

namespace LaserSausagesAPITests.TestDBConnector
{
    [TestClass]
    public class TestDeleteDB
    {
        DBConnector dbConnector = new DBConnector("DefaultEndpointsProtocol=https;AccountName=lasersausage;AccountKey=/Kq0wUdk7SdN8oKqy7GLjHvAcqdn6Fu1qLhw7XD/m/CN9imhrv6AmJc3lc2Zi9yVautpd0LRreJvBbTDHyXgIQ==;BlobEndpoint=https://lasersausage.blob.core.windows.net/;TableEndpoint=https://lasersausage.table.core.windows.net/;QueueEndpoint=https://lasersausage.queue.core.windows.net/;FileEndpoint=https://lasersausage.file.core.windows.net/");


        [TestMethod]
        public void DeleteStudent_StudentDoesNotExist()
        {
            // Arrange
            Student stud = new Student() { RowKey = "TestRowKey" };

            // Act
            var result = dbConnector.DeleteStudent(stud);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteStudent_SuccessfullyDeletesStudent()
        {

            // Arrange
            dbConnector.CreateStudent(new Student() { RowKey = "TestRowKey" });
            var dbStudent  = new Student() { RowKey = "TestRowKey" };

            // Act
            var result = dbConnector.DeleteStudent(dbStudent);

            // Assert
            Assert.IsTrue(result);
        }

    }
}
