using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Models;

namespace SchoolApp.Repositories
{   
    // Si necessaire, permet de réimplémenter une fonction existante ou d'impémenter ou nouvelle fonction pour une Collection/table/class spécifique
    internal class SQLSectionRepository : SQLBaseRepository<Section>
    {
        public SQLSectionRepository(SchoolContext context) : base(context) { }


        // Quand on réimplémente une fonction, la fonction initiale doit être "virtual" et la fonction réimplémntée doit être override
        public override void Insert(Section entity)
        {
            List<Section> match = SearchFor(s => s.Name.Equals(entity.Name)).ToList();
            if (match.Count == 0)
            {
                _dbContext.Set<Section>().Add(entity);
                SaveChanges();
                Console.WriteLine("La section " + entity.Name + " à été ajoutée");
            }
            else 
            {
                Console.WriteLine("La section " + entity.Name + " existe déjà");
            }
        }
    }
}
