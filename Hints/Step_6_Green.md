```c#
public class TeamMember
{
    public double HoursWorked { get; set; }
    public string Name { get; set; }

    public TeamMember(string name)
    {
        Name = name;
    }

    public void AddHoursWorked(double hours)
    {
        HoursWorked += hours;
    }
}
```
