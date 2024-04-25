using APIDay2.Models;
using APIDay2.Repos;

namespace APIDay2.Unit_Of_Works
{
    public class UnitOfWork
    {
        ITIContext db;
        GenericRepository<Student> studentRepository;
        GenericRepository<Department> departmentRepository;
        GenericRepository<Instructor> instructorRepository;
        public UnitOfWork(ITIContext _db)
        {
            db = _db;
        }
        public GenericRepository<Student> StudentsRepo
        {
            get
            {
                if (studentRepository == null)
                {
                     studentRepository = new GenericRepository<Student> (db);
                }
                return studentRepository;
            }
        }
        public GenericRepository<Department> DepartmentsRepo
        {
            get
            {
                if (departmentRepository == null)
                {
                     departmentRepository = new GenericRepository<Department>(db);
                }
                return departmentRepository;
            }
        }
        public GenericRepository<Instructor> InstructorsRepo
        {
            get
            {
                if (instructorRepository == null)
                {
                     instructorRepository = new GenericRepository<Instructor>(db);
                }
                return instructorRepository;
            }
        }


    }
}
