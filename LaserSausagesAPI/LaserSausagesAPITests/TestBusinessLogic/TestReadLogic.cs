using LaserSausagesAPI;
using Moq;

namespace LaserSausagesAPITests.TestBusinessLogic
{
    [TestClass]
    public class TestReadLogic
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
        public void GetStudents_ShouldReturnAllStudents()
        {
            //Arrange  
            mock.Setup(mock => mock.GetStudents()).Returns(students);
            BusinessLogic businessLogic = new BusinessLogic(mock.Object);

            //Act
            var result = businessLogic.GetStudents();

            //Assert
            Assert.IsFalse(students.Except(result).Any());
        }

        [TestMethod]
        public void GetStudentById_ShouldReturnChosenStudent()
        {
            //Arrange
            mock.Setup(mock => mock.GetStudentById("21052022185625")).Returns(students[0]);
            BusinessLogic businessLogic = new BusinessLogic(mock.Object);

            //Act
            var result = businessLogic.GetStudentById("21052022185625");

            //Assert
            Assert.AreEqual(students[0], result);
        }

        [TestMethod]
        [DataRow("", 6)]
        [DataRow("shaun", 1)]
        [DataRow("jordan", 1)]
        [DataRow("mich", 2)]
        [DataRow("Shaun White", 1)]
        [DataRow("zzzzzzzzzzzzzzzz", 0)]
        public void GetStudentByName_ShouldFilterByFirstNameAndSurName(string search, int count)
        {
            //Arrange
            bool searchIsContainedInStudentName = false;

            mock.Setup(mock => mock.GetStudents()).Returns(students);

            BusinessLogic businessLogic = new BusinessLogic(mock.Object);

            //Act
            var requestedStudents = businessLogic.GetStudentByName(search);

            searchIsContainedInStudentName = requestedStudents.All(student =>
            (student.FirstName + " " + student.SurName).ToLower().Contains(search.ToLower().Trim()));

            //Assert
            Assert.AreEqual(count, requestedStudents.Count);
            Assert.IsTrue(searchIsContainedInStudentName);
        }

    }
}
