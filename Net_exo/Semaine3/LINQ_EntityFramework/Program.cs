// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using LINQ_EntityFramework.Models;
using static System.Console; //

WriteLine("Séance 3 : Linq to Entites");

while (true) 
{
    WriteLine("\n\n-------- Entrez un numéro d'exercice à exécuter, null pour quitter --------");
    WriteLine("B1-7, C1, D1, E1-4");
    string? exerciceNumber = ReadLine();

    if (exerciceNumber != "")
    {
        switch (exerciceNumber)
        {
            case "B1":
                ExB1();
                break;
            case "B2":
                ExB2();
                break;
            case "B3":
                ExB3();
                break;
            case "B4":
                ExB4();
                break;
            case "B5":
                ExB5();
                break;
            case "B6":
                ExB6();
                break;
            case "B7":
                ExB7();
                break;
            case "C1":
                ExC1();
                break;
            case "D1":
                ExD1();
                break;
            case "E1":
                ExE1();
                break;
            case "E2":
                ExE2();
                break;
            case "E3":
                ExE3();
                break;
            case "E4":
                ExE4();
                break;
            default:
                WriteLine("numéro d'exerice inconnu");
                break;

        }
    }
    else
    {
        break;
    }
}

static void ExB1()
{
    WriteLine("\nQuestion B1 : ");
    WriteLine("Tapez le nom d'une ville : ");
    string? city = ReadLine();

    if (city != null)
    {
        using (NorthwindContext context = new NorthwindContext())
        {
            var query1 = from Customer c in context.Customers
                         where c.City == city
                         select new { c.ContactName, c.CustomerId };

            WriteLine("\nLes clients suivant habitent dans cette ville : ");
            foreach (var c in query1)
            {
                WriteLine("{0} : {1}", c.CustomerId, c.ContactName);
            }
        }
    }
}

static void ExB2()
{
    WriteLine("\nQuestion B2 : ");
    WriteLine("Afficher les produits de la catégorie Beverages et Condiments (lazy loading): ");

    using (NorthwindContext context = new NorthwindContext())
    {
        var beveragesProducts = from Product p in context.Products
                        where p.Category.CategoryName == "Beverages" 
                        select new { p.ProductName };

        WriteLine("\nCategory : Beverages");
        foreach (var product in beveragesProducts)
        {
            WriteLine("{0} :", product.ProductName);
        }

        var condimentsProducts = from Product p in context.Products
                                where p.Category.CategoryName == "Condiments"
                                select new { p.ProductName };

        WriteLine("\nCategory : Condiments");
        foreach (var product in condimentsProducts)
        {
            WriteLine("{0}", product.ProductName);
        }
    }
}

static void ExB3()
{
    WriteLine("\nQuestion B3 : ");
    WriteLine("Afficher les produits de la catégorie Beverages et Condiments (eager loading): ");

    using (NorthwindContext context = new NorthwindContext())
    {
        var beveragesProducts = from Product p in context.Products
                                .Include("Category")
                                where p.Category.CategoryName == "Beverages"
                                select new { p.ProductName };

        WriteLine("\nCategory : Beverages");
        foreach (var product in beveragesProducts)
        {
            WriteLine("{0} :", product.ProductName);
        }

        var condimentsProducts = from Product p in context.Products
                                 where p.Category.CategoryName == "Condiments"
                                 select new { p.ProductName };

        WriteLine("\nCategory : Condiments");
        foreach (var product in condimentsProducts)
        {
            WriteLine("{0}", product.ProductName);
        }
    }
}

static void ExB4()
{
    WriteLine("\nQuestion B4 : ");
    WriteLine("Donnez pour un client donné saisi au clavier la liste de ces commandes (de la plus récente à la plus ancienne) et qui ont été livrées ( il y a une date de livraison) : ");
    WriteLine("Entrez l'ID du client (ex : LILAS) : ");
    string? customerId = ReadLine();

    using (NorthwindContext context = new NorthwindContext())
    {
        var customerCommands = from Order o in context.Orders
                               where o.CustomerId == customerId && o.ShippedDate < DateTime.Now.Date
                               orderby o.OrderDate descending
                               select new { o.CustomerId, o.OrderDate, o.ShippedDate };

        WriteLine("\nCommandes :");
        foreach (var customerCommand in customerCommands)
        {
            WriteLine("CustomerID: {0}, OderDate : {1}, ShippedDate : {2}", customerCommand.CustomerId, customerCommand.OrderDate, customerCommand.ShippedDate);
        }
    }
}

static void ExB5()
{
    WriteLine("\nQuestion B5 : ");
    WriteLine("Afficher le total des ventes par produit (ID  produit -> Total) trié par ordre de numéro  : ");

    using (NorthwindContext context = new NorthwindContext())
    {
        var orderDetails = from OrderDetail o in context.OrderDetails
                       orderby o.ProductId
                       group o by o.ProductId;

        WriteLine("\nProducts :");
        foreach (var product in orderDetails)
        {
            WriteLine("{0} --> {1}", product.Key, product.Sum(o => o.UnitPrice * o.Quantity));
        }
    }
}

static void ExB6()
{
    WriteLine("\nQuestion B6 : ");
    WriteLine("Afficher tous les employés (leur nom) qui ont sous leur responsabilité la région « Western » : ");

    using (NorthwindContext context = new NorthwindContext())
    {
        var employees = from Employee e in context.Employees
                        where e.Territories.Any(t => t.Region.RegionDescription == "western") 
                        select new { e.FirstName, e.LastName };

          WriteLine("\nEmployees :");
        foreach (var employee in employees)
        {
            WriteLine("{0} {1}", employee.FirstName, employee.LastName);
        }
    }
}

static void ExB7()
{
    WriteLine("\nQuestion B7 : ");
    WriteLine("Quels sont les territoires gérés par le supérieur de « Suyama Michael » : ");

    using (NorthwindContext context = new NorthwindContext())
    {
        var territories = (from Employee e in context.Employees
                           where e.FirstName.Equals("Michael") && e.LastName.Equals("Suyama")
                           select e.ReportsToNavigation.Territories).SingleOrDefault();

        WriteLine("\nTerritoires :");
        foreach (var teritory in territories)
        {
            WriteLine("{0}", teritory.TerritoryDescription);
        }
    }
}

static void ExC1()
{
    WriteLine("\nQuestion C1 : ");
    WriteLine("Mettez en majuscule le nom de tous les clients & affiichez les clients: ");

    using (NorthwindContext context = new NorthwindContext())
    {
        var customers = from Customer e in context.Customers
                        select e;

        foreach (var customer in customers) 
        {
            if (customer.ContactName != null)
                customer.ContactName = customer.ContactName.ToUpper();
        }

        try
        {
            context.SaveChanges();
        }
        catch (Exception e)
        {
            WriteLine("Erreur {0}", e.Message);
        }

        customers = from Customer e in context.Customers
                    select e;

        WriteLine("\nClients :");
        foreach (var customer in customers)
        {
            WriteLine("{0}", customer.ContactName);
        }
    }
}

static void ExD1()
{
    WriteLine("\nQuestion D1 : ");
    WriteLine("Ajoutez une catégorie à partir d’un nom saisi au clavier & Vérifiez que l’ajout a bien été effectué en DB");
    
    WriteLine("\nNom de la nouvelle categorie");
    string? category = ReadLine();
    if (category != null) 
    {
        using (NorthwindContext context = new NorthwindContext())
        {

            // add

            Category? checkCategories = (from Category c in context.Categories
                             where c.CategoryName.Equals(category)
                             select c).SingleOrDefault();

            if (checkCategories == null)
            {
                try
                {
                    context.Categories.Add(new Category { CategoryName = category });
                    context.SaveChanges();
                    WriteLine("\nCatégorie ajoutée");
                }
                catch (Exception e)
                {
                    WriteLine("Erreur {0}", e.Message);
                } 

            }
            else 
            {
                WriteLine("La catégorie existe déjà");
                return;
            }

            // check

            Category? cat = (from Category c in context.Categories
                           where c.CategoryName.Equals(category)
                           select c).SingleOrDefault();

            WriteLine("\nRecherche pour une categorie " + category + " : ");
            if (cat == null) WriteLine(" Non trouvée :(");
            else WriteLine(" Trouvée --> {0}", cat.CategoryName); 

        }
    }
}

static void ExE1()
{
    WriteLine("\nQuestion E1 : ");
    WriteLine("Supprimez la catégorie ajoutée à l’exercice précédent & Vérifiez que la suppression a bien été faite en DB :");

    WriteLine("\nNom de la categorie à supprimer :");
    string? categoryName = ReadLine();
    if (categoryName != null)
    {
        using (NorthwindContext context = new NorthwindContext())
        {
            Category? checkedCategory = (from Category c in context.Categories
                                         where c.CategoryName.Equals(categoryName)
                                         select c).SingleOrDefault();

            if (checkedCategory != null)
            {
                try
                {
                    context.Categories.Remove(checkedCategory);
                    context.SaveChanges();
                    WriteLine("\nCatégorie supprimée");
                }
                catch (Exception e)
                {
                    WriteLine("Erreur {0}", e.Message);
                }

            }
            else
            {
                WriteLine("La catégorie n'existe pas");
                return;
            }

            // check

            Category? cat = (from Category c in context.Categories
                             where c.CategoryName.Equals(categoryName)
                             select c).SingleOrDefault();

            WriteLine("\nRecherche pour une categorie " + categoryName + " : ");
            if (cat == null) WriteLine(" Non trouvée");
            else WriteLine(" Trouvée --> {0}", cat.CategoryName);

        }
    }
}

static void ExE2()
{
    WriteLine("\nQuestion E2 : ");
    WriteLine("Vérifiez que la suppression a bien été faite en DB :");

    WriteLine("\nNom de la categorie à checkée :");
    string? categoryName = ReadLine();
    if (categoryName != null)
    {
        using (NorthwindContext context = new NorthwindContext())
        {
            Category? checkedCategory = (from Category c in context.Categories
                                         where c.CategoryName.Equals(categoryName)
                                         select c).SingleOrDefault();

            if (checkedCategory == null) WriteLine(" Non trouvée");
            else WriteLine(" Trouvée --> {0}", checkedCategory.CategoryName);
        }
    }
}

static void ExE3()
{
    WriteLine("\nQuestion E3 : ");
    WriteLine("Supprimez un employé et réassignez toutes ses commandes (« orders ») à un autre employé. Vous demanderez les «id » des employés au clavier");

    WriteLine("\nId de l'employé à supprimer :");
    string? id1 = ReadLine();

    WriteLine("\nId de l'employé à qui va recevoir les commandes :");
    string? id2 = ReadLine();


    if (id1 != null && id2 != null)
    {
        if (!int.TryParse(id1, out int e1))
        {
            WriteLine("--> Employé 1 inconnu");
            return;
        }
        if (!int.TryParse(id2, out int e2))
        {
            WriteLine("--> Employé 2 inconnu");
            return;
        }

        using (NorthwindContext context = new NorthwindContext())
        {
            Employee emp1 = (from Employee e in context.Employees
                             where e.EmployeeId.Equals(e1)
                             select e).Single<Employee>();

            Employee emp2 = (from Employee e in context.Employees
                             where e.EmployeeId.Equals(e2)
                             select e).Single<Employee>();

            var orders = from Order o in context.Orders
                         where o.EmployeeId.Equals(e1)
                         select o;

            foreach (Order o in orders)
            {
                emp2.Orders.Add(o);
                emp1.Orders.Remove(o);

            }

            emp1.Territories.Clear();
            emp1.InverseReportsToNavigation.Clear();



            context.Employees.Remove(emp1);

            try
            {
                int affected = context.SaveChanges();
                WriteLine("--> Nombre de lignes affectées " + affected);
            }
            catch (Exception e)
            {
                WriteLine("Erreur {0}", e.Message);
            }

        }
    }
}

static void ExE4()
{
    WriteLine("\nQuestion E4 : ");
    WriteLine("Ckeck employé");

    WriteLine("\nId de l'employé à checker :");
    string? id1 = ReadLine();



    if (id1 != null && int.TryParse(id1, out int e1))
    {
        using (NorthwindContext context = new NorthwindContext())
        {
            Employee? emp1 = (from Employee e in context.Employees
                             where e.EmployeeId.Equals(e1)
                             select e).SingleOrDefault<Employee>();
            if (emp1 != null)
            {

                var orders = from Order o in context.Orders
                             where o.EmployeeId.Equals(e1)
                             select o;

                WriteLine("\n--> {0} {1} existe et s'occupe de {2} commandes", emp1.FirstName, emp1.LastName, orders.Count());
            }
            else
            {
                WriteLine("\n--> Employé inexistant");
            }
        }
    }
}
