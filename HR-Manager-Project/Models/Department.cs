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
                    return;
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
                    return;
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
                    return;
                }
                _salarylimit = value;
            }
        }
        private double _salarylimit;
        public Employee[] Employees = { };

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

        public int WorkerCounter()
        {
            int total = 0;

            foreach (Employee item in Employees)
            {
                if (item != null)
                {
                    total++;
                }
            }

            return total;
        }

        public double SalaryCounter()
        {
            double salaryNow = 0;

            foreach (Employee item in Employees)
            {
                if (item != null)
                {
                    salaryNow += item.Salary;
                }
            }

            return salaryNow;
        }

        public Department(string name, int workerlimit, double salarylimit)
        {
            Employees =new Employee[0];
            Name = name;
            WorkerLimit = workerlimit;
            SalaryLimit = salarylimit;
        }
        public override string ToString()
        {

            Console.WriteLine("--------------------------");    
            return $"\nDepartament Adi:{Name}\nIsci Limiti:{WorkerLimit}\nMaksimum Maas Limiti:{SalaryLimit}\n";

        }
    }
}
