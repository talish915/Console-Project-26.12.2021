using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Manager_Project.Models
{
    class Department
    {
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length < 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Departament Adi Minimum 2 Herfden Ibaret Olunmaldir!");
                    Console.ResetColor();
                }
                _name = value;
            }
        }
        private string _name;
        public int WorkerLimit 
        {
            get
            {
                return _workerlimit;
            }
            set
            {
                if (value < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Departamentin Isci Limiti Minimum 1 Olmalidir!");
                    Console.ResetColor();
                }
                _workerlimit = value;
            }
        }
        private int _workerlimit;
        public double SalaryLimit 
        {
            get
            {
                return _salarylimit;
            }
            set
            {
                if (value < 250)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Departamentin Maas Limiti 250 Manatdan Asagi Ola Bilmez!");
                    Console.ResetColor();
                }
                _salarylimit = value;
            }
        }
        private double _salarylimit;
        public Employee[] Employees = { };

        public Department(string name, int workerlimit, double salarylimit)
        {
            Employees =new Employee[0];
            Name = name;
            WorkerLimit = workerlimit;
            SalaryLimit = salarylimit;
        }
        public double CalcSalaryAverage(Department department)
        {
            double totalsalary = 0;
            int counter = 0;
            foreach (Employee item in department.Employees)
            {
                totalsalary += item.Salary;
                counter++;
            }
            return totalsalary / counter;
        }
        public override string ToString()
        {

            Console.WriteLine("--------------------------");    
            return $"\nDepartament di:{Name}\nIsci Limiti:{WorkerLimit}\nMaksimum Maas Limiti:{SalaryLimit}\n";

        }
    }
}
