
using SchoolApp.Repositories;
using SchoolApp.Models;
using SchoolApp.UntitsOfWork;
using static System.Console;

WriteLine("Séance 4 : Patterns Repository & Units Of Work");

WriteLine("\nB : Pattern Repository");
WriteLine("\n B.2.a On ajoute 2 sections :");

SchoolContext context = new SchoolContext(); 

IUnitOfWorkSections unitOfWorkSections = new UnitOfWorkSectionsSQLServer(context);

Section sectInfo = new Section { Name = "sectInfo" };
Section sectDiet = new Section { Name = "sectDiet" };

// comment savoir quelles valeur sont obligatoire
unitOfWorkSections.SectionsRepository.Insert(sectInfo);
unitOfWorkSections.SectionsRepository.Insert(sectDiet);


WriteLine("\n B.2.b La table section contient :");
List<Section> sections = unitOfWorkSections.SectionsRepository.GetAll().ToList();

foreach (Section section in sections)
{
    WriteLine(section.SectionId + " : " + section.Name);
}

WriteLine("\n B.2.c On ajoute 3 étudiants :");

IUnitOfWorkStudents unitOfWorkStudents = new UnitOfWorkStudentsSQLServer(context);

unitOfWorkStudents.StudentsRepository.Insert(new Student { Name = "DUPOND", Firstname = "Ted", YearResult = 100, Section = sectInfo });
unitOfWorkStudents.StudentsRepository.Insert(new Student { Name = "DUPOND", Firstname = "Fred", YearResult = 120, Section = sectDiet });
unitOfWorkStudents.StudentsRepository.Insert(new Student { Name = "WINDSOR", Firstname = "Elizabeth", YearResult = 110, Section = sectInfo });

WriteLine("\n B.2.b La table student contient :");
List<Student> students = unitOfWorkStudents.StudentsRepository.GetAll().ToList();

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


