using HR_Manager_Project.Interface;
using HR_Manager_Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Manager_Project.Services
{
    class HRManager : IHRManager
    {
        public Department[] Departments => _departments;
        private Department[] _departments;

        public HRManager()
        {
            _departments = new Department[0];
        }

        public void AddDepartment(string name, int workerlimit, double salarylimit)
        {
            Department department = new Department(name, workerlimit, salarylimit);
            Array.Resize(ref _departments, _departments.Length + 1);
            _departments[_departments.Length - 1] = department;
            

        }

        public void AddEmployee(string fullname, string position, double salary, string departmentname)
        {

            foreach (Department item in _departments)
            {
                if (item.Name.ToLower()== departmentname.ToLower())
                {
                    Employee employee = new Employee(fullname, position, salary, departmentname);
                    Array.Resize(ref item.Employees, item.Employees.Length + 1);
                    item.Employees[item.Employees.Length - 1] = employee;
                }

            }
        }

        public void EditDepartment(string name, string newname)
        {
            Department department = null;

            foreach (Department item in _departments)
            {
                if (item.Name.ToLower() == name.ToLower())
                {
                    department = item;
                    break;
                }
            }
            department.Name = newname;
        }

        public void EditEmployee(string employeeNo, string fullname, string position, double salary)
        {
            foreach (Department department in _departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee != null)
                    {
                        if ((employee.No.ToLower() == employeeNo.ToLower()) && (employee.FullName.ToLower() == fullname.ToLower()))
                        {
                            if (position != null)
                            {
                                employee.Position = position;
                            }

                            if (salary != 0 && salary >= 250)
                            {
                                employee.Salary = salary;
                            }
                            break;
                        }
                    }
                }
            }
        }
        public Department[] GetDepartment()
        {
            if (_departments.Length <= 0)
            {
                return null;
            }
            return _departments;
        }

        public void RemoveEmployee(string no, string departmentname)
        {
            foreach (Department item in _departments)
            {
                for (int i = 0; i < item.Employees.Length; i++)
                {
                    if (item.Employees[i].No == no)
                    {
                        item.Employees = null;
                        return;
                    }
                }

            }
        }
    }
}
