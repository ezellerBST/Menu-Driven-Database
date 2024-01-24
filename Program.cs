//Error Function
int getValidChoice(int min, int max)
{
    int input;
    while (true)
    {
        if (int.TryParse(Console.ReadLine(), out input) && input >= min && input <= max)
        {
            return input;
        }
        Console.WriteLine("Please enter a number from the list\n");
    }
}

//Menu Functions
void displayOptions()
{
    Console.WriteLine("\n-----------------------");
    Console.WriteLine("1. Create New Employee" + "\n2. View All Employees" + "\n3. Search Employees" + "\n4. Delete Employee" + "\n5. Quit");
    Console.WriteLine("-----------------------");
    Console.Write("\nEnter your choice: ");
}

void createNewEmployee()
{
    Console.WriteLine("\n--------------------");
    Console.WriteLine("Create New Employee");
    Console.WriteLine("--------------------\n");
    Console.Write("What is the employee's name? ");
    string newEmployeeName = Console.ReadLine();
    Console.Write("What is the employee's title? ");
    string newEmployeeTitle = Console.ReadLine();
    Console.Write("What is the employee's start date? ");
    string newEmployeeStartDate = Console.ReadLine();
    Employee newEmployee = new Employee(newEmployeeName, newEmployeeTitle, newEmployeeStartDate);
    Employee.employeeList.Add(newEmployee);
    int employeeID = newEmployee.setEmployeeID();
    Console.WriteLine($"\nNew Employee Added - Employee ID: {employeeID}");
}

void viewEmployees()
{
    Console.WriteLine("\n-------------------");
    Console.WriteLine("View All Employees");
    Console.WriteLine("-------------------\n");
    if (Employee.employeeList.Count == 0)
    {
        Console.WriteLine("No employees to display");
    }
    else
    {
        for (int i = 0; i < Employee.employeeList.Count; i++)
        {
            Employee newEmployee = Employee.employeeList[i];
            newEmployee.printDetails();
        }
    }
}

void searchEmployees()
{
    Console.WriteLine("\n---------------------");
    Console.WriteLine("Search Employees By:");
    Console.WriteLine("1: Employee ID");
    Console.WriteLine("2: Name");
    Console.WriteLine("---------------------\n");
    Console.Write("Enter your choice: ");
    Employee.findEmployees();
}

void deleteEmployee()
{
    Console.WriteLine("\n------------------");
    Console.WriteLine("Delete an Employee");
    Console.WriteLine("------------------\n");
    Console.Write("Enter your Employee ID to delete: ");
    Employee.getDeleteEmployee();
}

void LoadEmployeesFromFile()
{
    string filePath = "employees.txt";
    if (File.Exists(filePath))
    {
        string[] input = File.ReadAllLines(filePath);
        foreach (string line in input)
        {
            string[] noComma = line.Split(',');
            int id = int.Parse(noComma[0]);
            string name = noComma[1];
            string title = noComma[2];
            DateOnly startDate = DateOnly.Parse(noComma[3]);
            Employee employee = new Employee(id, name, title, startDate);
            Employee.employeeList.Add(employee);
        }
    }
}

void SaveEmployeesToFile()
{
    string filePath = "employees.txt";
    using (StreamWriter savedFile = new StreamWriter(filePath))
    {
        foreach (Employee employee in Employee.employeeList)
        {
            string line = $"{employee.employeeID},{employee.name},{employee.title},{employee.startDate.ToString("MM/dd/yyyy")}";
            savedFile.WriteLine(line);
        }
    }
}

//Menu-driven program
bool continueProgram = true;
LoadEmployeesFromFile();

while (continueProgram)
{
    displayOptions();
    int taskChoice = getValidChoice(1, 5);
    switch (taskChoice)
    {
        case 1:
            createNewEmployee();
            break;
        case 2:
            viewEmployees();
            break;
        case 3:
            searchEmployees();
            break;
        case 4:
            deleteEmployee();
            break;
        default:
            continueProgram = false;
            SaveEmployeesToFile();
            Console.WriteLine("Employees Saved, Goodbye!");
            break;
    }
}