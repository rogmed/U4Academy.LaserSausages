using LaserSausagesAPI;
using Moq;

namespace LaserSausagesAPITests.TestBusinessLogic
{
    [TestClass]
    public class TestCreateLogic
    {
        List<Student> students = new List<Student>() {
            new Student("MIT", "Shaun", "White", new DateTime(1986, 9, 3).ToUniversalTime(), "Business", new DateTime(2023, 9, 20).ToUniversalTime()),
            new Student("UCLA", "Simone", "Bilies", new DateTime(1997, 3, 14).ToUniversalTime(), "Chemistry", new DateTime(2024, 5, 5).ToUniversalTime()),
            new Student("UGR", "Usain", "Bolt", new DateTime(1986, 8, 21).ToUniversalTime(), "Theoretical Physics", new DateTime(2024, 5, 15).ToUniversalTime()),
            new Student("UNC", "Michael", "Jordan", new DateTime(1963, 2, 17).ToUniversalTime(), "Math", new DateTime(2023, 1, 18).ToUniversalTime()),
            new Student("UNC", "Michael", "Phelps", new DateTime(1985, 6, 30).ToUniversalTime(), "Astronomy", new DateTime(2022, 11, 21).ToUniversalTime()),
            new Student("UW", "Chloe", "Kim", new DateTime(2000, 4, 23).ToUniversalTime(), "Economics", new DateTime(2023, 6, 19).ToUniversalTime()),
        };

        Mock<IDBConnector> mock = new Mock<IDBConnector>();

        [TestMethod]
        public void CreateStudent_ShouldReturnTrue()
        {
            //Arrange  
            mock.Setup(mock => mock.CreateStudent(It.IsAny<Student>())).Returns(true);
            BusinessLogic businessLogic = new BusinessLogic(mock.Object);

            //Act
            var result = businessLogic.CreateStudent(students[0]);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateStudent_WithInvalidInputShouldReturnFalse()
        {
            //Arrange  
            BusinessLogic businessLogic = new BusinessLogic(mock.Object);
            Student student = new Student("TEST Create Invalid", "Test!@@$%", "Create",
                new DateTime(1996,1,1).ToUniversalTime(), "Creating", new DateTime(1997,1,1).ToUniversalTime());

            //Act
            var result = businessLogic.CreateStudent(student);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
