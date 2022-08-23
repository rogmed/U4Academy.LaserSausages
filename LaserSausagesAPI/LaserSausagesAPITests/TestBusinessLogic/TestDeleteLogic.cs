using LaserSausagesAPI;
using Moq;

namespace LaserSausagesAPITests.TestBusinessLogic
{
    [TestClass]
    public class TestDeleteLogic
    {
        Mock<IDBConnector> mock = new Mock<IDBConnector>();

        [TestMethod]
        public void DeleteStudent_StudentDoesNotExist()
        {
            // Arrange
            Student? studentToDelete = null;

            mock.Setup(mock => mock.DeleteStudent(It.IsAny<Student>())).Returns(false);
            BusinessLogic businessLogic = new BusinessLogic(mock.Object);

            // Act
            var result = businessLogic.DeleteStudent(studentToDelete);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteStudent_SuccessfullyDeletesStudent()
        {
            // Arrange
            var studentToDelete = new Student
                (
                "UW",
                "Spooder",
                "Man",
                new DateTime(2000, 4, 23),
                "Economics",
                new DateTime(2023, 6, 19)
                );

            mock.Setup(mock => mock.DeleteStudent(It.IsAny<Student>())).Returns(true);
            BusinessLogic businessLogic = new BusinessLogic(mock.Object);

            // Act
            var result = businessLogic.DeleteStudent(studentToDelete);

            // Assert
            Assert.IsTrue(result);
            mock.Verify(x => x.DeleteStudent(studentToDelete));
        }
    }
}
