Test example for step 6

```c#
[Test]
public void CanAssignTimeToTeamMember()
{
    //Arrange
    TeamMember member1 = new TeamMember("Bob");
    //Act
    member1.AddHoursWorked(10);
    member1.AddHoursWorked(20.5);
    //Assert
    Assert.AreEqual(30.5, member1.HoursWorked);

}
```
