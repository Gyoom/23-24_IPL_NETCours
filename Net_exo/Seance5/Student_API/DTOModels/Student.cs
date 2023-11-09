namespace Student_API.DTOModels
{
    public class Student
    {
        public int ID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}