using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_API.DTOModels;
using Student_API.Repositories;
using Student_API.UnitsOfWork;

namespace Student_API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentController : ControllerBase
    {
        private IRepository<Student> _studentRepository;
        public StudentController()
        {
            IUnitOfWork u = new UnitOfWorkMemory();
            _studentRepository = u.StudentRepository;
        }

        [HttpGet("getAll")]
        public ActionResult<IList<Student>> GetAll()
        {
            IList<Student> s = _studentRepository.GetAll();

            if (s == null) return NotFound();
            else
            {

                return Ok(s);
            }
        }

        [HttpGet("getOne")]
        public ActionResult<Student> GetOneById(int ID)
        {
            Student s = _studentRepository.GetById(ID);
            if (s == null) return NotFound();
            else
            {

                return Ok(s);
            }
        }

        [HttpPost("addOne")]
        public ActionResult<Student> AddOne(string firstName, string lastName)
        {
            if (firstName == null || firstName == "") return BadRequest();
            if (lastName == null || lastName == "") return BadRequest();
            DateTime date = DateTime.Now;
            int id = _studentRepository.GetAll().Count + 1;
            Student student = new Student { ID = id, FirstName = firstName, LastName = lastName, Birthdate = date };
            _studentRepository.Insert(student);
            return Ok(student);
        }
    }
}
