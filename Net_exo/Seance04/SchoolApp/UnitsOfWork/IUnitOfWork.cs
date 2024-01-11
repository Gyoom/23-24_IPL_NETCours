using SchoolApp.Repositories;
using SchoolApp.Models;

namespace SchoolApp.UntitsOfWork
{
    interface IUnitOfWork
    {
        // Définis les repositories accessibles via Unit of Work
        // Chaque repository ici doit avoir une classe MemoryRepository (car contrairement aux SQLRepository, les memory n'ont pas de baseMemory qui peux agir en tant que substitut)
        // Si les classes memoryRepository n'ont pas de classe c'est par ce que toute les methodes 
        IRepository<Student> StudentRepository { get; }
        IRepository<Section> SectionRepository { get; }
    }
}
