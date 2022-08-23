namespace LaserSausagesAPI
{  
    public class StudentVM
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string BirthDate { get; set; }
        public string Course { get; set; }
        public string FinishDate { get; set; }

        public StudentVM(Student student)
        {
            this.PartitionKey = student.PartitionKey;
            this.RowKey = student.RowKey;
            this.FirstName = student.FirstName;
            this.SurName = student.SurName;
            this.BirthDate = student.BirthDate.ToString("yyyy-MM-dd");
            this.Course = student.Course;
            this.FinishDate = student.FinishDate.ToString("yyyy-MM-dd");
        }
    }
}
