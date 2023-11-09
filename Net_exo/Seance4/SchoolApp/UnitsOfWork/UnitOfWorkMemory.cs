using SchoolApp.Models;
using SchoolApp.Repositories;
using SchoolApp.UntitsOfWork;

namespace SchoolApp.UnitsOfWork
{
    internal class UnitOfWorkMemory : IUnitOfWork
    {

        private MemoryStudentRepository _studentRepository;

        private MemorySectionRepository _sectionRepository;


        public UnitOfWorkMemory()
        {
            this._studentRepository = new MemoryStudentRepository();
            this._sectionRepository = new MemorySectionRepository();

        }

        public IRepository<Student> StudentRepository
        {
            get { return this._studentRepository; }
        }

        public IRepository<Section> SectionRepository
        {
            get { return this._sectionRepository; }
        }
    }
}
