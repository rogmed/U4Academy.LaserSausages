using LaserSausagesAPI;

namespace LaserSausagesAPITests.TestDBConnector
{
    [TestClass]
    public class TestCreateDB
    {
        DBConnector dbConnector = new DBConnector("DefaultEndpointsProtocol=https;AccountName=lasersausage;AccountKey=/Kq0wUdk7SdN8oKqy7GLjHvAcqdn6Fu1qLhw7XD/m/CN9imhrv6AmJc3lc2Zi9yVautpd0LRreJvBbTDHyXgIQ==;BlobEndpoint=https://lasersausage.blob.core.windows.net/;TableEndpoint=https://lasersausage.table.core.windows.net/;QueueEndpoint=https://lasersausage.queue.core.windows.net/;FileEndpoint=https://lasersausage.file.core.windows.net/");

        [TestMethod]
        public void CreateAStudent()
        {
            Student student = new Student("University", "Test", "Create", new DateTime(1986, 9, 3).ToUniversalTime(), "Course", DateTime.UtcNow);

            Assert.IsTrue(dbConnector.CreateStudent(student));

            dbConnector.DeleteStudent(student);
        }
    }
}
