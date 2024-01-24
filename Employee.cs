public class Employee
{
    // Properties
    public string name { get; set; }
    public int employeeID { get; set; }
    public string title { get; set; }
    public DateOnly startDate { get; set; }

    public static List<Employee> employeeList = new List<Employee>();

    // Parameterized constructor
    public Employee(string Name, string Title, string StartDate)
    {
        name = Name;
        title = Title;
        bool validDate = false;

        while (!validDate)
        {
            if (DateOnly.TryParse(StartDate, out DateOnly parsedStartDate))
            {
                startDate = parsedStartDate;
                validDate = true;
            }
            else
            {
                Console.WriteLine("Invalid date format");
                Console.Write("What is the employee's start date? ");
                StartDate = Console.ReadLine();
            }
        }
    }

    //Text file constructor
    public Employee(int idT, string nameT, string titleT, DateOnly startDateT)
    {
        employeeID = idT;
        name = nameT;
        title = titleT;
        startDate = startDateT;
    }

    public int setEmployeeID()
    {
        Random randomID = new Random();
        employeeID = randomID.Next(1000, 10000);
        return employeeID;
    }

    // Printer Method
    public void printDetails()
    {
        Console.WriteLine($"{name}, {title}\nEmployee ID: {employeeID}\nStart Date: {startDate}\n");
    }

    public static Employee FindById(int id)
    {
        return employeeList.FirstOrDefault(e => e.employeeID == id);
    }

    public static List<Employee> FindByName(string Name)
    {
        return employeeList.Where(e => e.name.Contains(Name)).ToList();
    }

    //Find Employees
    public static void findEmployees()
    {
        string choice = Console.ReadLine();
        while (choice != "1" && choice != "2")
        {
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();
        }

        if (choice == "1")
        {
            Console.Write("Enter employee ID: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("\nNo employees found");
                Console.Write("Enter employee ID: ");
            }

            Employee employeeById = FindById(id);
            while (employeeById == null)
            {
                Console.WriteLine("No employees found");
                Console.Write("\nEnter your choice: ");
                choice = Console.ReadLine();
                while (choice != "1" && choice != "2")
                {
                    Console.Write("Enter your choice: ");
                    choice = Console.ReadLine();
                }

                if (choice == "1")
                {
                    Console.Write("Enter employee ID: ");
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.Write("Enter employee ID: ");
                    }
                    employeeById = FindById(id);
                }
                else
                {
                    Console.Write("Enter employee name: ");
                    string name = Console.ReadLine();
                    List<Employee> employeesByName = FindByName(name);
                    if (employeesByName.Count > 0)
                    {
                        Console.WriteLine("\n----------");
                        Console.WriteLine("Results:");
                        Console.WriteLine("----------\n"); 
                        foreach (Employee employee in employeesByName)
                        {
                            employee.printDetails();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo employees found");
                    }
                    return;
                }
            }
            Console.WriteLine("\n----------");
            Console.WriteLine("Results:");
            Console.WriteLine("----------\n"); 
            employeeById.printDetails();
        }
        else
        {
            Console.Write("Enter employee name: ");
            string name = Console.ReadLine();
            List<Employee> employeesByName = FindByName(name);
            if (employeesByName.Count > 0)
            {
                Console.WriteLine("\n----------");
                Console.WriteLine("Results:");
                Console.WriteLine("----------\n");
                foreach (Employee employee in employeesByName)
                {
                    employee.printDetails();
                }
            }
            else
            {
                Console.WriteLine("\nNo employees found");
            }
        }
    }

   //Delete Employees
    public static void getDeleteEmployee()
    {
        int id;
        if (int.TryParse(Console.ReadLine(), out id))
        {
            Employee employeeToDelete = FindById(id);
            if (employeeToDelete != null)
            {
                employeeList.Remove(employeeToDelete);
                Console.WriteLine($"\nEmployee ID {id} has been deleted");
            }
            else 
            {
            Console.WriteLine($"Employee ID {id} not found\n");
            }
        }
    }
}

