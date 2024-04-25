namespace APIDay2.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string St_Address { get; set; }
        public int? St_Age { get; set; }
        public string Dept_Name { get; set; }

        public string Supervisor { get; set; }
    }
}
