using Student_API.Repositories;
using Student_API.DTOModels;

namespace Student_API.UnitsOfWork
{
    internal class UnitOfWorkMemory : IUnitOfWork
    {

        private MemoryStudentRepository _studentRepository;


        public UnitOfWorkMemory()
        {
            this._studentRepository = new MemoryStudentRepository();

        }

        public IRepository<Student> StudentRepository
        {
            get { return this._studentRepository; }
        }
    }
}
