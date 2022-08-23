using LaserSausagesAPI;
using System.Net;

namespace LaserSausagesAPITests.TestStudentController
{
    [TestClass]
    public class TestCreateController
    {
        private readonly ApiWebApplicationFactory _api;
        private readonly HttpClient _client;

        public TestCreateController()
        {
            _api = new ApiWebApplicationFactory();
            _client = _api.CreateClient();
        }

        [TestMethod]
        public async Task CreateStudentHttpRequest_ShouldReturnOkTrue()
        {
            Student student = new Student("University", "Test", "Create",
                new DateTime(1986,9,3).ToUniversalTime(), "Creating", DateTime.UtcNow);
            student.RowKey = "create";

            var response = await _client.PostAsJsonAsync<Student>("/api/students/", student);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            await _client.PatchAsync("/api/students?id=create", null);
        }

        [TestMethod]
        public async Task CreateStudentHttpRequest_ShouldReturnBadRequestOnBirthdateLaterThanFinishdate()
        {
            Student student = new Student("University", "Test", "Create",
                new DateTime(2022,9,3).ToUniversalTime(), "Creating", new DateTime(2020,9,3).ToUniversalTime());

            var response = await _client.PostAsJsonAsync<Student>("/api/students/", student);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateStudentHttpRequest_ShouldReturnBadRequestOnBirthdateLaterThanToday()
        {
            Student student = new Student("University", "Test", "Create",
                new DateTime(3000,9,3).ToUniversalTime(), "Creating", new DateTime(3020,9,3).ToUniversalTime());

            var response = await _client.PostAsJsonAsync<Student>("/api/students/", student);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateStudentHttpRequest_ShouldReturnBadRequestOnSpecialCharactersInFields()
        {
            Student student = new Student("University", "dudu", "bibi",
                new DateTime(3000,9,3).ToUniversalTime(), "Course%!@#&", new DateTime(3020,9,3).ToUniversalTime());

            var response = await _client.PostAsJsonAsync<Student>("/api/students/", student);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
