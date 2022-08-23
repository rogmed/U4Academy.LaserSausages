using LaserSausagesAPI;
using System.Net.Http.Json;
using System.Net;

namespace LaserSausagesAPITests.TestStudentController
{
    [TestClass]
    public class TestReadController
    {
        private readonly ApiWebApplicationFactory _api;
        private readonly HttpClient _client;

        public TestReadController()
        {
            _api = new ApiWebApplicationFactory();
            _client = _api.CreateClient();
        }

        [TestMethod]
        public async Task GetStudentsHttpRequest_ShouldReturnAListOfStudent()
        {
            List<Student>? list = await _client.
                GetFromJsonAsync<List<Student>>("/api/students/name");

            Assert.IsNotNull(list);
            Assert.IsInstanceOfType(list, typeof(List<Student>));
        }

        [TestMethod]
        public async Task GetStudentByIdHttpRequest_ShouldReturnTheChosenStudent()
        {
            Student? student = await _client.
                GetFromJsonAsync<Student>("/api/students/id/e88fd3434bdc4e9eb704ceab40c8b20f");

            Assert.IsNotNull(student);
            Assert.IsInstanceOfType(student, typeof(Student));
            Assert.AreEqual("TEST read", student.PartitionKey);
            Assert.AreEqual("e88fd3434bdc4e9eb704ceab40c8b20f", student.RowKey);
        }

        [TestMethod]
        public async Task GetStudentByIdHttpRequest_ShouldThrowExceptionIfError()
        {
            try
            {
                await _client.GetFromJsonAsync<Student>("/api/students/id/a");
            }
            catch (HttpRequestException e)
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, e.StatusCode);
            }
        }

        [TestMethod]
        [DataRow("test read", 1)]
        [DataRow("test", 2)]
        [DataRow("zzzzzzzzzzzzzzzz", 0)]
        public async Task GetStudentByNameHttpRequest_ShouldReturnTheChosenStudents(string pattern, int count)
        {
            List<Student>? students = await _client.
                GetFromJsonAsync<List<Student>>("/api/students/name/" + pattern);

            Assert.AreEqual(count, students!.Count);
        }
    }
}
