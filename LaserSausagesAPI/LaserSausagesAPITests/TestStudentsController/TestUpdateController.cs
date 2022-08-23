using LaserSausagesAPI;
using System.Net;

namespace LaserSausagesAPITests.TestStudentController
{
    [TestClass]
    public class TestUpdateController
    {
        private readonly ApiWebApplicationFactory _api;
        private readonly HttpClient _client;

        public TestUpdateController()
        {
            _api = new ApiWebApplicationFactory();
            _client = _api.CreateClient();
        }

        [TestMethod]
        public async Task UpdateStudentHttpRequest_ShouldReturnStatusCodeOk()
        {
            // Arrange
            Student? student = await _client.
                GetFromJsonAsync<Student>("/api/students/id/23e2f638a3c14742a965629e9189d90a");
            Student originalStudent = student!.Clone();

            student.FirstName = "Test";
            student.SurName = "Create";
            student.BirthDate = new DateTime(1996, 1, 1).ToUniversalTime();
            student.FinishDate = new DateTime(1997, 1, 1).ToUniversalTime();
            student.Course = "Creating";

            // Act
            var sut = await _client.PutAsJsonAsync<Student>("/api/students/id/23e2f638a3c14742a965629e9189d90a", student!);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, sut.StatusCode);

            var IsSetToOriginalTestStudent = await _client.PutAsJsonAsync<Student>("/api/students/id/23e2f638a3c14742a965629e9189d90a", originalStudent!);

            Assert.AreEqual(HttpStatusCode.OK, IsSetToOriginalTestStudent.StatusCode);
        }

        [TestMethod]
        public async Task UpdateStudentHttpRequest_ShouldReturnExceptionWhenThereIsAnError()
        {
            // Arrange
            Student student =
                new Student("MIT", "Shaun", "White", new DateTime(1986, 9, 3).ToUniversalTime(), "Business", new DateTime(2023, 9, 20).ToUniversalTime());

            // Act
            var sut = await _client.PutAsJsonAsync<Student>("/api/students/id/zzzzzzzz", student);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, sut.StatusCode);
        }

        [TestMethod]
        public async Task UpdateStudentHttpRequest_ShouldReturnBadRequestOnBirthdateLaterThanFinishdate()
        {
            // Arrange
            Student? student = await _client.
                GetFromJsonAsync<Student>("/api/students/id/23e2f638a3c14742a965629e9189d90a");
            student!.FinishDate = student.BirthDate;

            // Act
            var sut = await _client.PutAsJsonAsync<Student>("/api/students/id/23e2f638a3c14742a965629e9189d90a", student);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, sut.StatusCode);
        }

        [TestMethod]
        public async Task UpdateStudentHttpRequest_ShouldReturnBadRequestOnBirthdateLaterThanToday()
        {
            // Arrange
            Student? student = await _client.
                GetFromJsonAsync<Student>("/api/students/id/23e2f638a3c14742a965629e9189d90a");
            student!.BirthDate = DateTime.UtcNow;

            // Act
            var sut = await _client.PutAsJsonAsync<Student>("/api/students/id/23e2f638a3c14742a965629e9189d90a", student);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, sut.StatusCode);
        }

        [DataRow("dudu", "bibi", "Course%!@#&")]
        [TestMethod]
        public async Task UpdateStudentHttpRequest_ShouldReturnBadRequestOnSpecialCharactersInFields(string firstname, string surname, string course)
        {
            Student? student = await _client.
     GetFromJsonAsync<Student>("/api/students/id/23e2f638a3c14742a965629e9189d90a");
            student!.Course = course;
            student!.SurName = surname;
            student!.FirstName = firstname;

            var response = await _client.PutAsJsonAsync<Student>("/api/students/id/23e2f638a3c14742a965629e9189d90a", student);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
