namespace LaserSausagesAPI
{
    public interface IDBConnector
    {
        public List<Student> GetStudents();
        public Student GetStudentById(string id);
        public bool CreateStudent(Student student);
        public bool DeleteStudent(Student student);
        public bool UpdateStudent(Student student);
    }
}
