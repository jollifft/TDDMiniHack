If you created a new TeamMember class in step 6, our test from step 5 needs some refactoring. 

Our test can be updated to reflect the new TeamMember class:
```c#
[Test]
public void CanAddTeamMembersToProject()
{
    //Arrange
    Project project = new Project();
    //Act
    project.AddTeamMember(new TeamMember("Robert"));
    project.AddTeamMember(new TeamMember("Austin"));
    project.AddTeamMember(new TeamMember("Beth"));
    //Assert
    Assert.AreEqual(3, project.NumberOfTeamMembers);

}
```

With our test updated, this puts us in the red phase for this test. After updating the test project, our implementation would look like this:
```c#
public class Project
{
    public IList<Segment> Segments = new List<Segment>();
    private Segment _activeSegment;
    private IList<TeamMember> _teamMembers = new List<TeamMember>();

    public bool IsActive { get; set; }
    public double Duration
    {
        get
        {
            double totalDuration = 0;
            foreach (var segment in Segments)
            {
                totalDuration += (segment.EndTime - segment.StartTime).TotalSeconds;
            }
            return totalDuration;
        }
    }

    public int NumberOfTeamMembers
    {
        get { return _teamMembers.Count; }
    }

    public void Start(DateTime startTime)
    {
        _activeSegment = new Segment();
        _activeSegment.Start(startTime);

        IsActive = true;
    }

    public void End(DateTime endTime)
    {
        _activeSegment.End(endTime);
        Segments.Add(_activeSegment);
    }

    public void AddTeamMember(TeamMember member)
    {
        _teamMembers.Add(member);

    }
}
```

Run the test from step 5 again, and we should now be in the green phase!
