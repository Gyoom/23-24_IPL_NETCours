using Student_API.Repositories;
using Student_API.DTOModels;

namespace Student_API.UnitsOfWork
{
    interface IUnitOfWork
    {
        IRepository<Student> StudentRepository { get; }
    }
}
