using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace LaserSausagesAPI
{
    public class BusinessLogic
    {
        private readonly IDBConnector _connector;

        public BusinessLogic(IDBConnector connector)
        {
            _connector = connector;
        }

        public List<Student> GetStudents()
        {
            return _connector.GetStudents();
        }

        public Student GetStudentById(string id)
        {
            return _connector.GetStudentById(id);
        }

        public List<Student> GetStudentByName(string searchString)
        {
            var trimSearchString = searchString.Trim();
            List<Student> students = _connector.GetStudents();

            Regex nameRegex = new Regex($"(?i){trimSearchString}(?-i)");

            List<Student> filteredStudents = students.Where(x => nameRegex.IsMatch(x.FirstName + " " + x.SurName)).ToList();

            return filteredStudents;
        }

        public bool CreateStudent(Student? student)
        {
            if (_StudentOk(student))
            {
                return _connector.CreateStudent(student!);
            }

            return false;
        }

        public bool DeleteStudent(Student? studentToDelete)
        {
            if (studentToDelete != null)
                return _connector.DeleteStudent(studentToDelete);
            else
                return false;
        }

        public bool UpdateStudent(Student? student)
        {
            if (_StudentOk(student))
            {
                return _connector.UpdateStudent(student!);
            }

            return false;
        }

        public static bool _StudentOk(Student? student)
        {
            if (student == null)
                return false;

            if (student.BirthDate >= student.FinishDate)
                return false;

            if (student.BirthDate >= DateTime.UtcNow)
                return false;

            return _StringOK(student.PartitionKey) && _StringOK_NoSpecialChars(student.FirstName) &&
                   _StringOK_NoSpecialChars(student.SurName) && _StringOK_NoSpecialChars(student.Course);
        }

        private static bool _StringOK(string? s)
        {
            return !string.IsNullOrEmpty(s) && s.Length <= 30;
        }

        private static bool _StringOK_NoSpecialChars(string? s)
        {
            if (!_StringOK(s))
                return false;

            var noSpecialCharRegex = new Regex("^(?:(?!(!@#$%^&*(),.?\\\":{}|<>))[a-zA-Z\\s\\p{L}-])+$");

            return noSpecialCharRegex.IsMatch(s!);
        }

        public static Dictionary<string, string> GetLanguageById(string id)
        {
            FileStream fileStream = File.OpenRead(@"Lang\Language.json");
            var languages = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(fileStream);

            if (languages!.ContainsKey(id))
            {
                return languages![id];
            }
            else return new Dictionary<string, string>();
        }
    }
}
