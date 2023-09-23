// See https://aka.ms/new-console-template for more information

using LINQDataContext;
using System.Linq;

// Exercice 1
Console.WriteLine("Exercice 1 :\n");

DataContext dc = new DataContext();

Student? jdepp = (from student in dc.Students
                  where student.Login == "jdepp"
                  select student).SingleOrDefault();

if (jdepp != null)
{
    Console.WriteLine(jdepp.Last_Name + jdepp.First_Name);
}

// Exercice 2.2
Console.WriteLine("\nExercice 2.2 :\n");

IEnumerable<Student> students22 = from student in dc.Students select student;

foreach (Student student in students22) {
    Console.WriteLine(student.Last_Name + ", " + student.First_Name + ", " + student.Student_ID + ", " + student.BirthDate);    
}

// Exercice 3.1 :
Console.WriteLine("\nExercice 3.1 :\n");

IEnumerable<Student> students31 = dc.Students.Where(c => c.BirthDate < new DateTime(1955, 1, 1));

foreach (Student student in students31)
{
    string status = "";
    if (student.Year_Result >= 12) status = "Ok"; else status = "KO";
    Console.WriteLine(student.Last_Name + ", " + student.Year_Result + ", " + status );
}

// Exercice 4.1 :
Console.WriteLine("\nExercice 4.1 :\n");

Console.WriteLine("Résultat annuel moyen pour l'ensembe des étudiant : " + dc.Students.Average(i => i.Year_Result));

// Exercice 4.5 :
Console.WriteLine("\nExercice 4.5 :\n");

Console.WriteLine("Nombre de students : " + dc.Students.Count());

// Exercice 5.1 :
Console.WriteLine("\nExercice 5.1 :\n");

var students51 = dc.Students
                .GroupBy(c => c.Section_ID);

foreach (IGrouping<int, Student> section in students51) {
    Console.WriteLine("Section : " + section.Key + " : " + section.Max(m => m.Year_Result));
}
// si on utilise les opérateurs de requêtes en pls temps séparé c'est bien aussi.

// Exercice 5.3 :
Console.WriteLine("\nExercice 5.3 :\n");

var students53 = dc.Students
                .Where(c => c.BirthDate < new DateTime(1985, 1, 1) && c.BirthDate >= new DateTime(1970, 1, 1))
                .GroupBy(c => c.BirthDate.Month);

foreach (IGrouping<int, Student> group in students51)
{
    Console.WriteLine("Section : " + group.Key + " : " + group.Average(g => g.Year_Result));
}

// Exercice 5.5 :
Console.WriteLine("\nExercice 5.5 :\n");

var query55 = from Cours in dc.Courses
           join Prof in dc.Professors on Cours.Professor_ID equals Prof.Professor_ID
           join Sect in dc.Sections on Prof.Section_ID equals Sect.Section_ID
           select new { Cours.Course_Name, Prof.Professor_Name, Sect.Section_Name };

foreach (var cours in query55)
{
    Console.WriteLine("Cours : " + cours.Course_Name + ", Prof : " + cours.Professor_Name + ", Section : " + cours.Section_Name);
}

// Exercice 5.5 :
Console.WriteLine("\nExercice 5.5 :\n");

var query57 = from Section in dc.Sections
              join Prof in dc.Professors on Section.Section_ID equals Prof.Section_ID into SectProfs
              select new { Section.Section_Name, SectProfs };
// on renvoi une jointure : IEnumerable<string, Ienumerable<Prof>> 

foreach (var section in query57)
{
    Console.WriteLine("Section : " + section.Section_Name);
    foreach (Professor prof in section.SectProfs) {
        Console.WriteLine(" Prof : " + prof.Professor_Name);
    }
}



