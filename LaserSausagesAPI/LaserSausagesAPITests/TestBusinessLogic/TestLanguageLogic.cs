using LaserSausagesAPI;

namespace LaserSausagesAPITests.TestBusinessLogic
{
    [TestClass]
    public class TestLanguageLogic
    {
        [TestMethod]
        [DataRow("EN", "SUBMIT")]
        [DataRow("ES", "CREAR")]
        [DataRow("PT", "ENVIAR")]
        [DataRow("PL", "ZATWIERDŹ")]
        public void GetLanguageById_ShouldReturnChosenLanguage(string id, string first_name)
        {
            //Act
            var result = BusinessLogic.GetLanguageById(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.ContainsKey("submit"));
            Assert.IsTrue(result.ContainsValue(first_name));
        }

        [TestMethod]
        public void GetLanguageById_WithInvalidIdShouldReturnNull()
        {
            //Act
            var result = BusinessLogic.GetLanguageById("");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }
    }
}
