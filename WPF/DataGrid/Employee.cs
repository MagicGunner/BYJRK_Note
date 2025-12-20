using Bogus;

namespace DataGrid;

public class Employee {
    public        bool                  IsSelected          { get; set; }
    public        int                   Id                  { get; set; }
    public        string?               FirstName           { get; set; }
    public        string?               LastName            { get; set; }
    public        DateOnly              BirthDay            { get; set; }
    public        int                   Salary              { get; set; }
    public static Employee              FakeOne()           => EmployeeFaker.Generate();
    public static IEnumerable<Employee> FakeMany(int count) => EmployeeFaker.Generate(count);

    private static readonly Faker<Employee> EmployeeFaker = new Faker<Employee>().RuleFor(e => e.Id, f => f.IndexFaker + 1)
                                                                                 .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                                                                                 .RuleFor(e => e.LastName, f => f.Name.LastName())
                                                                                 .RuleFor(e => e.BirthDay, f => DateOnly.FromDateTime(f.Date.Past(40, DateTime.Now.AddYears(-18))))
                                                                                 .RuleFor(e => e.Salary, f => f.Random.Int(30_000, 150_000));
}