using APIDay2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIDay2.DTOs
{
    public class DepartmentsDTO
    {
        public int Dept_Id { get; set; }

       
        public string Dept_Name { get; set; }

        
        public string Dept_Desc { get; set; }

       
        public string Dept_Location { get; set; }

        public int? ManagerId { get; set; }
        public int NoStudents { get; set; }
       
    }
}
