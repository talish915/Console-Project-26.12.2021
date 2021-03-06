using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Manager_Project.Models
{
    class Employee
    {
        private static int _count = 1000;
        public string No { get; set; }
        public string FullName 
        {
            get
            {
                return _fullname;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                _fullname = value;
            }
        }
        private string _fullname;
        public string Position 
        {
            get
            {
                return _position;
            }
            set
            {
                if (value.Length < 2)
                {
                    return;
                }
                _position = value;
            }
        }
        private string _position;
        public double Salary 
        {
            get
            {
                return _salary;
            }
            set
            {
                if (value < 250)
                {
                    return;
                }
                _salary = value;
            }
        }
        private double _salary;
        public string DepartmentName { get; set; }

        public Employee(string fullname, string position, double salary, string departmentname)
        {
            FullName = fullname;
            Position = position;
            Salary = salary;
            DepartmentName = departmentname;
            _count++;
            No += DepartmentName.ToUpper().Substring(0, 2) + _count;
        }
        public override string ToString()
        {
            return $" Ad,Soyad: {FullName}\n Nomresi: {No}\n Departamenti: {DepartmentName}\n Vezifesi: {Position}\n Maasi: {Salary}\n";
        }
    }

}
