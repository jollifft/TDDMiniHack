Example code for step 5 for passing test (green phase)

```c#
public class Project
{
    public IList<Segment> Segments = new List<Segment>();
    private Segment _activeSegment;
    private IList<string> _teamMembers = new List<string>();
    
    //Extra code removed for readability...

    public int NumberOfTeamMembers
    {
        get { return _teamMembers.Count; }
    }

    public void AddTeamMember(string member)
    {
        _teamMembers.Add(member);

    }
}
```
