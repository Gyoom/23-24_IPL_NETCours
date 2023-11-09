
using SchoolApp.Repositories;
using SchoolApp.Models;
using SchoolApp.UntitsOfWork;
using static System.Console;
using SchoolApp.UnitsOfWork;

WriteLine("Séance 4 : Patterns Repository & Units Of Work");


SchoolContext context = new SchoolContext();
var stockage = "0";
while (stockage == "1" || stockage == "2") 
{
    WriteLine("En SQL (1) ou en Memory (2) ?");
    stockage = ReadLine();
}

IUnitOfWork unitOfWork;

if (stockage == "1")
    unitOfWork = new UnitOfWorkSQLServer(context); // on utilise SQL ici
else
    unitOfWork = new UnitOfWorkMemory(); // mais pas ici


WriteLine("\nB : Pattern Repository");
WriteLine("\n B.2.a On ajoute 2 sections :");


Section sectInfo = new Section { Name = "sectInfo" };
Section sectDiet = new Section { Name = "sectDiet" };

unitOfWork.SectionRepository.Insert(sectInfo);
unitOfWork.SectionRepository.Insert(sectDiet);
unitOfWorkSQL.SectionRepository.GetById

WriteLine("\n B.2.b La table section contient :");
List<Section> sections = unitOfWork.SectionRepository.GetAll().ToList();

foreach (Section section in sections)
{
    WriteLine(section.SectionId + " : " + section.Name);
}



WriteLine("\n B.2.c On ajoute 3 étudiants :");

unitOfWork.StudentRepository.Insert(new Student { Name = "DUPOND", Firstname = "Ted", YearResult = 100, Section = sectInfo });
unitOfWork.StudentRepository.Insert(new Student { Name = "DUPOND", Firstname = "Fred", YearResult = 120, Section = sectDiet });
unitOfWork.StudentRepository.Insert(new Student { Name = "WINDSOR", Firstname = "Elizabeth", YearResult = 110, Section = sectInfo });



WriteLine("\n B.2.b La table student contient :");
List<Student> students = unitOfWork.StudentRepository.GetAll().ToList();

foreach (Student student in students)
{
    WriteLine(student.Section?.Name + " : " + student.Name + " " + student.Firstname);
}



WriteLine("\n B.3 Afficher la liste par section trié par resultat :");
var sortedStudents = students.OrderByDescending(s => s.YearResult).GroupBy(s => s.Section);

foreach (IGrouping<Section, Student> section in sortedStudents)
{
    WriteLine("  Section " + section.Key.Name + " : ");
    foreach (Student student in section)
    {
        WriteLine("   " + student.Name + " " + student.Firstname + " : " + student.YearResult);
    }

}


