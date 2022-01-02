using HR_Manager_Project.Models;
using HR_Manager_Project.Services;
using System;

namespace HR_Manager_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            HRManager hrManager = new HRManager();

            do
            {
                Console.WriteLine("------------------Human Resource Management------------------");
                Console.WriteLine("Yerine Yetirmek Istediyniz Emeliyyatin Qarsisindaki Nomreni Daxil Edin ");
                Console.WriteLine("1 - Departamentlerin siyahisini goster");
                Console.WriteLine("2 - Yeni Departament Yarat");
                Console.WriteLine("3 - Departamentde Deyisiklik Etmek");
                Console.WriteLine("4 - Butun Iscilerin Siyahisi");
                Console.WriteLine("5 - Secilmis Departamentdeki Iscilerin Siyahisi");
                Console.WriteLine("6 - Yeni Isci Elave Etmek");
                Console.WriteLine("7 - Isci Uzerinde Deyisiklik Etmek");
                Console.WriteLine("8 - Departament Uzre Iscini Silinmesi");
                Console.Write("Daxil Edin: ");

                string select = Console.ReadLine();
                int selectNum;
                int.TryParse(select, out selectNum);

                switch (selectNum)
                {
                    case 1:
                        Console.Clear();
                        GetDepartments(ref hrManager);
                        break;
                    case 2:
                        Console.Clear();
                        AddDepartment(ref hrManager);
                        break;
                    case 3:
                        Console.Clear();
                        EditDepartment(ref hrManager);
                        break;
                    case 4:
                        Console.Clear();
                        GetEmployees(ref hrManager);
                        break;
                    case 5:
                        Console.Clear();
                        GetEmployeesByDepartmentName(ref hrManager);
                        break;
                    case 6:
                        Console.Clear();
                        AddEmployee(ref hrManager);
                        break;
                    case 7:
                        Console.Clear();
                        EditEmployee(ref hrManager);
                        break;
                    case 8:
                        Console.Clear();
                        RemoveEmployee(ref hrManager);
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Secim Yalnisdir!");
                        Console.ResetColor();
                        break;
                }
            } while (true);
        }

        static void AddDepartment(ref HRManager hrManager)
        {
        reEnterDepartmentName:
            Console.Write("Departamentin Adini Daxil Edin: ");
            string departmentname = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(departmentname) || departmentname.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Departament Adini Duzgun Daxil Edin!");
                Console.ResetColor();
                goto reEnterDepartmentName;
            }

            foreach (Department item in hrManager.Departments)
            {
                if (item.Name.ToLower() == departmentname.ToLower())
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Daxil Etdiyiniz Adda Departament Artiq Movcuddur!");
                    Console.ResetColor();
                    return;
                }
            }

        reEnterWorkerLimit:
            Console.Write("Departamentin Maximum Isci Sayini Daxil Edin: ");
            string workers = Console.ReadLine();
            int workersNum;
            while (!int.TryParse(workers, out workersNum) || workersNum < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Isci Sayini Duzgun Daxil Edin!");
                Console.ResetColor();
                goto reEnterWorkerLimit;
            }


        reEnterSalaryLimit:
            Console.Write("Departament Uzre Maximum Maas Limitini Daxil Edin: ");
            string salary = Console.ReadLine();
            double salaryNum;
            while (!double.TryParse(salary, out salaryNum) || salaryNum < 250)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Maas 250 Manatdan Asagi Ola Bilmez!");
                Console.ResetColor();
                goto reEnterSalaryLimit;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Yeni Departament Elave Olundu");
            Console.ResetColor();
            hrManager.AddDepartment(departmentname, workersNum, salaryNum);
        }

        static void AddEmployee(ref HRManager hrManager)
        {
            if (hrManager.Departments.Length <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Departament Yoxdur!");
                Console.ResetColor();
                return;
            }


        reEnterDepartmentName:
            Console.Write("Iscinin Daxil Olacag Departamentin Adini Daxil Edin: ");
            string departmentName = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(departmentName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Departamentin Adini Duzgun Daxil Edin!");
                Console.ResetColor();
                goto reEnterDepartmentName;
            }

            bool check = false;
            string fullname = string.Empty;
            string positionName = string.Empty;
            double checkSalary = 0;
            foreach (Department item in hrManager.Departments)
            {
                if (item.Name.ToLower() == departmentName.ToLower())
                {
                    check = true;
                    departmentName = item.Name;

                    if (item.WorkerLimit <= item.WorkerCounter())
                    {
                        Console.WriteLine($"{item.Name} Departamentin Isci Kadrosu Dolmusdur!");
                        return;
                    }

                    if (item.SalaryLimit - item.SalaryCounter() == 0 || item.SalaryLimit - item.SalaryCounter() < 250)
                    {
                        Console.WriteLine($"{item.Name} Departamentin Maas Limiti Dolmusdur ve Yaxud Elave Etmek Istediyiniz Iscinin Minimum Maasi Departamentin Maas Limitini Kecdiyine Gore Isci Elave Oluna Bilmez!");
                        return;
                    }

                reEnterFullname:
                    Console.Write("Elave Etmek Istediyiniz Iscinin Adini ve Soyadini Daxil Edin: ");
                    fullname = Console.ReadLine();
                    string[] full = fullname.Split(' ');
                    if (String.IsNullOrWhiteSpace(fullname) || full.Length < 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Iscinin Adini ve Soyadini Duzgun Daxil Edin!");
                        Console.ResetColor();
                        goto reEnterFullname;
                    }


                reEnterPositionName:
                    Console.Write("Iscinin Vezifesini Daxil Edin: ");
                    positionName = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(positionName) || positionName.Length < 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Vezife Adini Duzgun Daxil Edin!");
                        Console.ResetColor();
                        goto reEnterPositionName;
                    }


                reEnterSalary:
                    Console.Write("Elave Etmek Istediyiniz Iscinin Maasini Daxil Edin: ");
                    string salary = Console.ReadLine();
                    if (!double.TryParse(salary, out checkSalary) || checkSalary < 250)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Iscinin Aylig Maasi 250 Manatdan Asagi Ola Bilmez!");
                        Console.ResetColor();
                        goto reEnterSalary;
                    }

                    foreach (Department department in hrManager.Departments)
                    {
                        if (department.Name.ToLower() == departmentName.ToLower())
                        {
                            while (department.SalaryLimit < department.SalaryCounter() + checkSalary)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{department.Name} Departamentinin Maas Limitini Kece Bilmessiniz!");
                                Console.ResetColor();
                                Console.WriteLine($"{department.Name} Departamentinin Maximum Maas Limiti {department.SalaryLimit} Manatdir.");
                                Console.WriteLine($"{department.Name} Departamentindeki Iscilerin Toplam Maasi {department.SalaryCounter()} Manatdir.");
                                Console.WriteLine($"Elave Etmek Istediyiniz Isciye Maximum  {department.SalaryLimit - department.SalaryCounter()} Manat Maas Verile Biler!");
                                Console.Write("Maasi Yeniden Daxil Edin: ");
                                goto reEnterSalary;
                            }
                        }
                        break;
                    }
                    break;
                }
            }

            if (check)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Isci Elave Olundu");
                Console.ResetColor();
            }

            if (check == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{departmentName} Adli Departament Yoxdur!");
                Console.ResetColor();
                goto reEnterDepartmentName;
            }

            hrManager.AddEmployee(fullname, positionName, checkSalary, departmentName);
        }

        static void GetDepartments(ref HRManager hrManager)
        {
            if (hrManager.Departments.Length <= 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Departament Yoxdur!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("-----------------Departamentlerin Siyahisi----------------");
            foreach (Department item in hrManager.Departments)
            {
                Console.WriteLine(item);
                Console.WriteLine("------------------------------------------");
            }

            hrManager.GetDepartment();
        }

        static void EditDepartment(ref HRManager hrManager)
        {
            if (hrManager.Departments.Length <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Departament Yoxdur!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("-------------------Departamentlerin Siyahisi------------------");
            foreach (Department item in hrManager.Departments)
            {
                Console.WriteLine(item);
                Console.WriteLine("----------------------------------------");
            }


        reEnterNameNow:
            Console.Write("Deyisiklik Etmek Istediyiniz Departamentin Adini Daxil Edin: ");
            string nameNow = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(nameNow))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Departament Adi Yalnisdir!");
                Console.ResetColor();
                goto reEnterNameNow;
            }

            bool checker = true;
            string newName = string.Empty;
            foreach (Department item in hrManager.Departments)
            {
                if (item.Name.ToLower() == nameNow.ToLower())
                {
                    checker = false;

                reEnterNewName:
                    Console.Write("Departamentin Yeni Adini Daxil Edin: ");
                    newName = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(newName) || newName.Length < 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Departamentin Yeni Adi Yalnisdir!");
                        Console.ResetColor();
                        goto reEnterNewName;
                    }

                    if (nameNow.ToLower() == newName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Departamentin Yeni Adi Evvelki Adla Eyni Ola Bilmez!");
                        Console.ResetColor();
                        goto reEnterNewName;
                    }

                    foreach (Department item2 in hrManager.Departments)
                    {
                        if (item2.Name.ToLower() == newName.ToLower())
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{newName} Adli Departament Movcuddur. Eyni Adda Iki Departament Ola Bilmez!");
                            Console.ResetColor();
                            return;
                        }
                    }

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Departament Adi Deyisdirildi!");
                    Console.ResetColor();
                    break;
                }
            }

            if (checker)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Daxil Etdiyiniz Adda Departament Yoxdur!");
                Console.ResetColor();
                goto reEnterNameNow;
            }

            hrManager.EditDepartment(nameNow, newName);
        }

        static void GetEmployees(ref HRManager hrManager)
        {
            int countWorker = 0;
            foreach (Department department in hrManager.Departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee != null)
                    {
                        countWorker++;
                    }
                }
            }

            if (countWorker == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Isci Yoxdur!");
                Console.ResetColor();
                return;
            }

            foreach (Department department in hrManager.Departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee != null)
                    {
                        Console.WriteLine(employee);
                        Console.WriteLine("------------------------------------------");
                    }
                }
            }
        }

        static void GetEmployeesByDepartmentName(ref HRManager hrManager)
        {
            if (hrManager.Departments.Length <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Departament Yoxdur!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("---------------------Departamentlerin Siyahisi--------------------");
            foreach (Department item in hrManager.Departments)
            {
                Console.WriteLine(item);
                Console.WriteLine("------------------------------------------");
            }


        reEnterDpName:
            Console.Write("Departament Uzre Isci Siyahisina Baxmaq Ucun Departament Adini Daxil Edin: ");
            string dpname = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(dpname) || dpname.Length < 2)
            {
                Console.WriteLine("Departament Adi Yalnisdir!");
                goto reEnterDpName;
            }

            bool check = true;
            foreach (Department department in hrManager.Departments)
            {
                if (department.Name.ToLower() == dpname.ToLower())
                {
                    Console.Clear();
                    int cntworker = 0;
                    foreach (Employee employee in department.Employees)
                    {
                        if (employee != null)
                        {
                            cntworker++;
                        }
                    }
                    if (cntworker == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bu Departamentde Isci Yoxdur!");
                        Console.ResetColor();
                        return;
                    }

                    foreach (Employee employee in department.Employees)
                    {
                        if (employee != null)
                        {
                            Console.WriteLine(employee);
                            Console.WriteLine("------------------------------------------");
                        }
                    }
                    check = false;
                    break;
                }
            }

            if (check)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Daxil Etdiyiniz Adda Departament Yoxdur!");
                Console.ResetColor();
                return;
            }
        }

        static void EditEmployee(ref HRManager hrManager)
        {
            int countWorker = 0;
            foreach (Department department in hrManager.Departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee != null)
                    {
                        countWorker++;
                    }
                }
            }

            if (countWorker == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Isci Yoxdur!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("---------------------Iscilerin Siyahisi--------------------");
            foreach (Department department in hrManager.Departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee != null)
                    {
                        Console.WriteLine(employee);
                        Console.WriteLine("------------------------------------------");
                    }
                }
            }


        reEnterWorkerNo:
            Console.Write("Deyisiklik Etmek Istediyiniz Iscinin Nomresini Daxil Edin: ");
            string workerNo = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(workerNo))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Isci Nomresi Yalnisdir!");
                Console.ResetColor();
                goto reEnterWorkerNo;
            }

        reEnterFullname:
            Console.Write("Deyisiklik Etmek Istediyiniz Iscinin Ad Ve Soyadini Daxil Edin: ");
            string fullname = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(fullname))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Iscinin Adi Ve Soyadi Yalnisdir!");
                Console.ResetColor();
                goto reEnterFullname;
            }


            string newSalary = null;
            double newSalaryNum = 0;
            string newPosition = null;
            bool checker = true;
            foreach (Department department in hrManager.Departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee != null)
                    {
                        if ((employee.No.ToLower() == workerNo.ToLower()) && (employee.FullName.ToLower() == fullname.ToLower()))
                        {
                            Console.Clear();
                            Console.WriteLine(employee);

                            Console.WriteLine("---------------------Isci Emeliyyati--------------------");
                            Console.WriteLine("1 - Iscinin Maasinda Deyisiklik");
                            Console.WriteLine("2 - Iscinin Vezifesinde Deyisiklik");
                            Console.WriteLine("3 - Her Iki Emeliyyati Yerine Yetirmek");

                        reEnterEditWorker:
                            Console.WriteLine("Daxil Edin: ");
                            string select = Console.ReadLine();
                            int selectNum;
                            if (!int.TryParse(select, out selectNum))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Secim Yalnisdir!");
                                Console.ResetColor();
                                goto reEnterEditWorker;
                            }

                            switch (selectNum)
                            {
                                case 1:
                                reEnterNewSalary:
                                    Console.Write("Iscinin Yeni Maasini Daxil Edin: ");
                                    newSalary = Console.ReadLine();
                                    if (!double.TryParse(newSalary, out newSalaryNum) || newSalaryNum < 250)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Maas 250 Manatdan Asagi Ola Bilmez!");
                                        Console.ResetColor();
                                        goto reEnterNewSalary;
                                    }

                                    while (department.SalaryLimit < department.SalaryCounter() - employee.Salary + newSalaryNum)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"{department.Name} Departamentindeki Maas Limitini Kecmek Olmaz!");
                                        Console.ResetColor();
                                        Console.WriteLine($"{department.Name} Departamentindeki Maximum Maas Limiti {department.SalaryLimit} Manatdir!");
                                        Console.WriteLine($"Deyisiklik Etdiyiniz Isciye Maximum {department.SalaryLimit - (department.SalaryCounter() - employee.Salary)} Manat Maas Vermek Mumkundur!");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Meblegi Duzgun Daxil Edin: ");
                                        Console.ResetColor();
                                        goto reEnterNewSalary;
                                    }
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Maas Deyisiklik Olundu!");
                                    Console.ResetColor();
                                    break;
                                case 2:
                                reEnternewPosition:
                                    Console.Write("Iscinin Yeni Vezifesini Daxil Edin: ");
                                    newPosition = Console.ReadLine();
                                    if (String.IsNullOrWhiteSpace(newPosition) || newPosition.Length < 2)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Iscinin Vezifesi 2 Herfden Uzun Olmalidir!");
                                        Console.ResetColor();
                                        goto reEnternewPosition;
                                    }
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Vezifede Deyisiklik Olundu!");
                                    Console.ResetColor();
                                    break;
                                case 3:
                                reEnterNewSalary1:
                                    Console.Write("Iscinin Yeni Maasini Daxil Edin: ");
                                    newSalary = Console.ReadLine();
                                    if (!double.TryParse(newSalary, out newSalaryNum) || newSalaryNum < 250)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Maas 250 Manatdan Asagi Ola Bilmez!");
                                        Console.ResetColor();
                                        goto reEnterNewSalary1;
                                    }

                                    while (department.SalaryLimit < department.SalaryCounter() - employee.Salary + newSalaryNum)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"{department.Name} Departamentindeki Maas Limitini Kecmek Olmaz!");
                                        Console.ResetColor();
                                        Console.WriteLine($"{department.Name} Departamentindeki Maximum Maas Limiti {department.SalaryLimit} Manatdir!");
                                        Console.WriteLine($"Deyisiklik Etdiyiniz Isciye Maximum {department.SalaryLimit - (department.SalaryCounter() - employee.Salary)} Manat Maas Vermek Mumkundur!");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("Meblegi Duzgun Daxil Edin: ");
                                        Console.ResetColor();
                                        goto reEnterNewSalary1;
                                    }

                                reEnternewPosition1:
                                    Console.Write("Iscinin Yeni Vezifesini Daxil Edin: ");
                                    newPosition = Console.ReadLine();
                                    if (String.IsNullOrWhiteSpace(newPosition) || newPosition.Length < 2)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Iscinin Vezifesi 2 Herfden Uzun Olmalidir!");
                                        Console.ResetColor();
                                        goto reEnternewPosition1;
                                    }
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Iscinin Maasinda Ve Vezifesinde Deyisiklik Olundu!");
                                    Console.ResetColor();
                                    break;
                            }
                            checker = false;
                            break;
                        }
                    }
                }
            }

            if (checker)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Daxil Etdiyinin Isci Nomresi ve Ad, Soyad Yalnisdir!");
                Console.ResetColor();
                return;
            }

            hrManager.EditEmployee(workerNo, fullname, newPosition, newSalaryNum);
        }

        static void RemoveEmployee(ref HRManager hrManager)
        {
            int counter = 0;
            foreach (Department department in hrManager.Departments)
            {
                if (department.Employees.Length > 0)
                {
                    counter++;
                }
            }

            if (counter == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Departament Ve Isci Yoxdur!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("-----------------Departamentlerin siyahisi----------------------");
            foreach (Department item in hrManager.Departments)
            {
                Console.WriteLine(item);
                Console.WriteLine("------------------------------------------");
            }


        reEnterSelectDepartment:
            Console.Write("Silmek Istediyiniz Iscinin Departamentini Daxil Edin: ");
            string Department = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(Department) || Department.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Daxil Etdiyiniz Adda Departament Yoxdur!");
                Console.ResetColor();
                goto reEnterSelectDepartment;
            }

            string DeleteWorker = string.Empty;
            bool find = true;
            foreach (Department department in hrManager.Departments)
            {
                if (department.Name.ToLower() == Department.ToLower())
                {
                    find = false;
                    int cntworker = 0;
                    foreach (Employee employee in department.Employees)
                    {
                        if (employee != null)
                        {
                            cntworker++;
                        }
                    }
                    if (cntworker == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{department.Name} Departamentinde Isci Yoxdur!");
                        Console.ResetColor();
                        return;
                    }

                    Console.Clear();
                    Console.WriteLine($"------------------{department.Name} Departamentindeki Isciler------------------");
                    foreach (Employee employee in department.Employees)
                    {
                        if (employee != null)
                        {
                            Console.WriteLine(employee);
                            Console.WriteLine("------------------------------------------");
                        }
                    }

                reEnterDelWorker:
                    Console.Write("Silmek Istediyiniz Iscinin Nomresini Daxil Edin: ");
                    DeleteWorker = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(DeleteWorker) || DeleteWorker.Length < 6)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Daxil Etdiyiniz isci Nomresi Yalnisdir!");
                        Console.ResetColor();
                        goto reEnterDelWorker;
                    }

                    bool del = false;
                    for (int i = 0; i < department.Employees.Length; i++)
                    {
                        if (department.Employees[i] != null)
                        {
                            if (department.Employees[i].No.ToLower() == DeleteWorker.ToLower())
                            {
                                del = true;
                                break;
                            }
                        }
                    }

                    if (del)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Isci Silindi!");
                        Console.ResetColor();
                    }

                    if (del == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Daxil etdiyiniz nomrede isci movcud deyil..");
                        return;
                    }

                    break;
                }
            }

            if (find)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Daxil Etdiyiniz Adda Departament Yoxdur!");
                Console.ResetColor();
                return;
            }

            hrManager.RemoveEmployee(DeleteWorker, Department);

        }

    }
}
