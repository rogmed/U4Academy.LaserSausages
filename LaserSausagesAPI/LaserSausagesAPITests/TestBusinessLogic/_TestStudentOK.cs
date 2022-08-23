using LaserSausagesAPI;
using Moq;

namespace LaserSausagesAPITests.TestBusinessLogic
{
    [TestClass]
    public class _TestStudentOK
    {
        Mock<IDBConnector> mock = new Mock<IDBConnector>();

        [DataRow("TESTnospace", "Test", "Create", 1996, "Creating", 1997)]
        [DataRow("TEST with space", "Test ", "Create", 1996, "Creating", 1997)]
        [DataRow("TEST with space", "Test", "Create ", 1996, "Creating", 1997)]
        [DataRow("TEST with space", "Test", "Create", 1996, "Creating ", 1997)]
        [DataRow("TEST with space", " Test", "Create", 1996, "Creating", 1997)]
        [DataRow("TEST with space", "Test", " Create", 1996, "Creating", 1997)]
        [DataRow("TEST with space", " Test ", " Create", 1996, " Creating", 1997)]
        [DataRow("TESTnospace_PL", "ąćęłńóśźż", "Create", 1996, "Creating", 1997)]
        [DataRow("TESTnospace_PL", "Test", "ąćęłńóśźż", 1996, "Creating", 1997)]
        [DataRow("TESTnospace_PL", "Test", "Create", 1996, "ąćęłńóśźż", 1997)]
        [DataRow("TESTnospace_PT", "ãáàâçéêíõóôúü", "Create", 1996, "Creating", 1997)]
        [DataRow("TESTnospace_PT", "Test", "ãáàâçéêíõóôúü", 1996, "Creating", 1997)]
        [DataRow("TESTnospace_PT", "Test", "Create", 1996, "ãáàâçéêíõóôúü", 1997)]
        [DataRow("TESTnospace_ES", "áéíñóúü", "Create", 1996, "Creating", 1997)]
        [DataRow("TESTnospace_ES", "Test", "áéíñóúü", 1996, "Creating", 1997)]
        [DataRow("TESTnospace_ES", "Test", "Create", 1996, "áéíñóúü", 1997)]
        [DataRow("TESTnospace_AllLang", "ãáàâçéêíõóôúüąáéíñóúüćęłńóśźż", "Create", 1996, "Creating", 1997)]
        [DataRow("TESTnospace_AllLang", "Test", "ãáàâçéêíõóôúüąáéíñóúüćęłńóśźż", 1996, "Creating", 1997)]
        [DataRow("TESTnospace_AllLang", "Test", "Create", 1996, "ãáàâçéêíõóôúüąáéíñóúüćęłńóśźż", 1997)]
        [TestMethod]
        public void _StudentOK_ShouldReturnTrue(string pk, string fn, string sn, int bdate, string c, int fdate)
        {
            //Arrange  
            Student student = new Student(
                pk, 
                fn,
                sn, 
                new DateTime(bdate, 9, 3).ToUniversalTime(),
                c, 
                new DateTime(fdate, 9, 20).ToUniversalTime());

            //Act
            var result = BusinessLogic._StudentOk(student);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void _StudentOK_NullStudentShouldReturnFalse()
        {
            //Arrange
            Student? student = null;

            //Act
            var result = BusinessLogic._StudentOk(student);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("", "Test", "Create", 1997, "Creating", 2022)]
        [DataRow("TEST Create Invalid", "", "Create", 1997, "Creating", 2022)]
        [DataRow("TEST Create Invalid", "Test", "", 1997, "Creating", 2022)]
        [DataRow("TEST Create Invalid", "Test", "Create", 1997, "", 2022)]
        [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Test", "Create", 1997, "Creating", 2022)]
        [DataRow("TEST Create Invalid", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Create", 1997, "Creating", 2022)]
        [DataRow("TEST Create Invalid", "Test", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 1997, "Creating", 2022)]
        [DataRow("TEST Create Invalid", "Test", "Create", 1997, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 2022)]
        [DataRow("TEST Create Invalid", "Test!@@$%", "Create", 1996, "Creating", 1997)]
        [DataRow("TEST Create Invalid", "Test", "Create!@@$%", 1996, "Creating", 1997)]
        [DataRow("TEST Create Invalid", "Test", "Create", 1996, "Creating!@@$%", 1997)]
        [DataRow("TEST Create Invalid", "Test", "Create", 1997, "Creating", 1996)]
        public void _StudentOK_WithInvalidInputShouldReturnFalse(string pk, string fn, string sn, int bdate, string c, int fdate)
        {
            // Arrange
            Student student = new Student(pk, fn, sn, new DateTime(bdate, 1, 1).ToUniversalTime(), c, new DateTime(fdate, 1, 1).ToUniversalTime());

            // Act
            var result = BusinessLogic._StudentOk(student);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
