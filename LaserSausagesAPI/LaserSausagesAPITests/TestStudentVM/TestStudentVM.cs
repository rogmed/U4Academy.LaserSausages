using LaserSausagesAPI;
using System.Reflection;

namespace LaserSausagesAPITests.TestStudentVM
{
    [TestClass]
    public class TestStudentVM
    {
        [TestMethod]
        public void StudentVM_Constructor()
        {
            // Arrange
            Student student = new Student
                (
                    "MIT", 
                    "Shaun", 
                    "White", 
                    new DateTime(1986, 9, 3), 
                    "Business", 
                    new DateTime(2023, 9, 20)
                );

            // Act
            var studentVM = new StudentVM(student);

            // Assert
            Assert.IsInstanceOfType(studentVM, typeof(StudentVM));
            Assert.AreEqual(student.PartitionKey, studentVM.PartitionKey);
            Assert.AreEqual(student.RowKey, studentVM.RowKey);
            Assert.AreEqual(student.FirstName, studentVM.FirstName);
            Assert.AreEqual(student.SurName, studentVM.SurName);
            Assert.AreEqual(student.BirthDate.ToString("yyyy-MM-dd"), studentVM.BirthDate);
            Assert.AreEqual(student.Course, studentVM.Course);
            Assert.AreEqual(student.FinishDate.ToString("yyyy-MM-dd"), studentVM.FinishDate);
        }
    }
}
