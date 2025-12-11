using Amusement_Park_System.Persistence;

namespace Amusement_Park_System.Models;

public class Manager : Employee
{
    
    private static List<Manager> _extent = new();
    public static IReadOnlyList<Manager> Extent => _extent.AsReadOnly();
    public static readonly string FilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Data/managers.json"));
    public static void Save() => ExtentManager.Save(_extent, FilePath);
    public static void Load() => ExtentManager.Load(ref _extent, FilePath);
    public static void ClearExtent() => _extent.Clear();
    
    public static int MinYearsOfExperience = 3;

    public Manager(
        string name,
        string surname,
        string contactInfo,
        DateTime birthDate,
        int yearsOfExperience)
        : base(name, surname, contactInfo, birthDate, yearsOfExperience)
    {
        if (YearsOfExperience < MinYearsOfExperience)
            throw new ArgumentException($"Managers must have at least {MinYearsOfExperience} years of experience.");
        _extent.Add(this);
    }
    
    // Employee association
    
    private readonly HashSet<Employee> _employeesManaged = new();
    public IReadOnlyCollection<Employee> EmployeesManaged => _employeesManaged.ToList().AsReadOnly();

    
    public void AddManagedEmployee(Employee employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));
        //manager cannot manage another manager

        if (employee is Manager)
            throw new InvalidOperationException("A manager cannot manage another manager.");

        // employee must be either RideOperator or MaintenanceStaff 
        if (employee is not RideOperator && employee is not MaintenanceStaff)
            throw new InvalidOperationException("Manager can only manage ride operators or maintenance staff.");

        //  XOR
        if (_employeesManaged.Count > 0)
        {
            var existingType = _employeesManaged.First().GetType();
            if (employee.GetType() != existingType)
            {
                throw new InvalidOperationException(
                    "This manager already manages a different type of employee. " +
                    "A manager can manage either ride operators or maintenance staff, but not both.");
            }
        }

        //employee already has another manager
        if (employee.Manager != null && employee.Manager != this)
            throw new InvalidOperationException("Employee is already managed by another manager.");

        if (_employeesManaged.Contains(employee))
            return;

        _employeesManaged.Add(employee);
        employee.SetManagerInternal(this);
    }
    
    public void RemoveManagedEmployee(Employee employee)
    {
        if (employee == null) throw new ArgumentNullException(nameof(employee));
        if (!_employeesManaged.Contains(employee)) return;

        _employeesManaged.Remove(employee);

        if (employee.Manager == this)
            employee.RemoveManagerInternal();
    }

    public bool ManagesEmployee(Employee employee) =>
        _employeesManaged.Contains(employee);


    //shift association
    
    private readonly HashSet<Shift> _shiftsAssigned = new();
    public IReadOnlyCollection<Shift> ShiftsAssigned => _shiftsAssigned.ToList().AsReadOnly();

    internal void AddShiftInternal(Shift shift)
    {
        _shiftsAssigned.Add(shift);
    }

    internal void RemoveShiftInternal(Shift shift)
    {
        _shiftsAssigned.Remove(shift);
    }
}