using HR_Manager_Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Manager_Project.Interface
{
    interface IHRManager
    {
        public Department[] Departments { get; }

        void AddDepartment(string name, int workerlimit, double salarylimit);
        Department[] GetDepartment(Department[] departments);
        void EditDepartment(string name, string newname);
        void AddEmployee(string fullname, string position, double salary, string departmentname);
        void RemoveEmployee(string no, string departmentname);
        void EditEmployee(string no, string fullname, string salary, string position);
    }
}
