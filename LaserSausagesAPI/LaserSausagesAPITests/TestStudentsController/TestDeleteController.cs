using LaserSausagesAPI;
using LaserSausagesAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LaserSausagesAPITests.TestStudentController
{

    [TestClass]
    public class TestDeleteController
    {
        private HttpClient _httpClient;

        public TestDeleteController()
        {
            var webAppFactory = new ApiWebApplicationFactory();
            _httpClient = webAppFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [TestMethod]
        public async Task DeleteStudent_StudentDoesNotExist()
        {
            // Arrange
            var getResponse = await _httpClient.GetAsync("/api/Students/id/delete");
            Assert.IsTrue(getResponse.StatusCode == HttpStatusCode.InternalServerError);

            // Act
            var response = await _httpClient.PatchAsync("/api/Students?id=delete", null);

            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task DeleteStudent_SuccessfullyDeletesStudent()
        {
            // Arrange
            var student = new Student() { RowKey = "delete", PartitionKey = "dd", FirstName = "dd", SurName = "dd", Course = "dd" };
            var studJson = JsonSerializer.Serialize(student);
            var postResponse = await _httpClient.PostAsync("/api/Students", new StringContent(studJson, Encoding.UTF8, "application/json"));
            Assert.IsTrue(postResponse.IsSuccessStatusCode);
            var getResponse = await _httpClient.GetAsync("/api/Students/id/delete");
            Assert.IsTrue(getResponse.IsSuccessStatusCode);

            // Act
            var response = await _httpClient.PatchAsync("/api/Students?id=delete", null);

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
