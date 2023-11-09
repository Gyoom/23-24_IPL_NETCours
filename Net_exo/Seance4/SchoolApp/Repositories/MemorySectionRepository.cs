using System.Linq.Expressions;
using SchoolApp.Models;

namespace SchoolApp.Repositories
{
    // Permet de stocker en local (temporaire) des données et effectuer les opértions de IRepository dessus (Sert à faire des test sans modifier les données de la vraie db)
    // Ne possède pas de BaseRepository
    class MemorySectionRepository : MemoryBaseRepository<Section>
    {

        public MemorySectionRepository() : base() { }

        public Section GetById(int id)
        {
            return this._collection.Where(s => s.SectionId == id).SingleOrDefault();
        }
    }
}
