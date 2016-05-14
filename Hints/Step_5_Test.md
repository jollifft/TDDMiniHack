Test example for step 5 

```c#
[Test]
public void CanAddTeamMembersToProject()
{
    //Arrange
    Project project = new Project();
    //Act
    project.AddTeamMember("Robert");
    project.AddTeamMember("Austin");
    project.AddTeamMember("Beth");
    //Assert
    Assert.AreEqual(3, project.NumberOfTeamMembers);

}
```
