using System.Net;

namespace LaserSausagesAPITests.TestLanguageController
{
    [TestClass]
    public class TestLanguageController
    {
        private readonly ApiWebApplicationFactory _api;
        private readonly HttpClient _Client;

        public TestLanguageController()
        {
           _api = new ApiWebApplicationFactory();
           _Client = _api.CreateClient();
        }

        [TestMethod]
        public async Task GetLanguageHttpRequest_ShouldReturnAdictionary()
        {  
            var response = await _Client.
                GetFromJsonAsync<Dictionary<string, string>>("/api/language/id/ES");

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(Dictionary<string, string>));
            Assert.IsTrue(response.ContainsKey("submit"));
            Assert.IsTrue(response.Any());
        }

        [TestMethod]
        [DataRow("E")]
        [DataRow("USA")]
        [DataRow("353445")]
        [DataRow("@#~#@€")]
        [DataRow("")]
        public async Task GetLanguageHttpResponse_ShouldBeNotFound(string id)
        {
            try
            {
                await _Client.GetFromJsonAsync<Dictionary<string, string>>($"/api/language/id/{id}");
            }
            catch(HttpRequestException e)
            {
                Assert.AreEqual(e.StatusCode, HttpStatusCode.NotFound);
            }
        }
    }
}
