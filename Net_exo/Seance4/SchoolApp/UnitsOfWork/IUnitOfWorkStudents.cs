using SchoolApp.Repositories;
using SchoolApp.Models;

namespace SchoolApp.UntitsOfWork
{
    interface IUnitOfWorkStudents
    {
        SQLStudentRepository StudentsRepository { get; }
    }
}
