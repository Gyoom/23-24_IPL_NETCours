using SchoolApp.Repositories;
using SchoolApp.Models;

namespace SchoolApp.UntitsOfWork
{
    interface IUnitOfWorkSections
    {
        SQLSectionRepository SectionsRepository { get; }
    }
}
