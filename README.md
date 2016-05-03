# TDDMiniHack
A quick and dirty intro into the wild and wonderful world of test driven development.

##Intro
Talk about red green refactor  
In our application, we know we want to track projects, and how long it has taken the project to go from start to finish. 

##Steps
1) Our first step will be to set up a "Must Pass" test. This is mainly just a sanity check to make sure we are starting out on the right foot with testing. In the test project, lets add our first test...
```c#
[Test]
public void MustPass()
{
    //Arrange
    int a = 1;
    int b = 1;
    //Act

    //Assert
    Assert.AreEqual(a, b);
}
```

2) Now that we know our test project is working, we can actually get into the meat of our application. Lets start by creating a test to make sure new projects are active when they are first created. However, we won't start by creating a project class. We will start with a test to make sure a project is active when it is created.
```c#
[Test]
public void WhenProjectStartedIsActive()
{
    //Arrange
    Project project = new Project();
    DateTime startTime = DateTime.Now;
    //Act
    project.Start(startTime);
    //Assert
    Assert.IsTrue(project.IsActive);
}
```
With our new test created, we now need to create just enough of the project class to get everything to build. We are not trying to get a passing test here, just enough to build successfully. 
Remember, we want our test to lead to the design of our application. Based on our test, we know we need to: 

* Create a project object
* Create a start method  
* Create an IsActive property  

Lets go ahead and create our project class now.
```c#
namespace TDDMiniHack
{
    public class Project
    {
        public bool IsActive { get; set; }

        public void Start(DateTime startTime)
        {
            
        }
    }
}
```
*Don't forget to add a reference from our main project to the test project so our tests know about our new project class*

Now that our application will build, lets go ahead and run all of our tests. Our new test should fail; THIS IS GOOD! This is the "Red" phase.  
Our next step is to get our test to pass, or the "Green" phase. From the test we have written, we know we are checking to see if our project is active. Knowing this, lets update our project class so our test will pass:

```c#
namespace TDDMiniHack
{
    public class Project
    {
        public bool IsActive { get; set; }

        public void Start(DateTime startTime)
        {
            IsActive = true;
        }
    }
}
```

Now lets go back and run all of our tests again. If you have set everything up correctly, all of our tests should now be passing! Great job, we are now in the "Green" phase.

3) Lets go ahead and create a new test. We want to be able to check the duration of our projects. Again, we won't start in the project class. We want start in our test project like last time.
```c#
[Test]
public void ProjectHasDuration()
{
    //Arrange
    Project project = new Project();
    DateTime startTime = DateTime.Now;

    //Act
    project.Start(startTime);
    project.End(startTime.AddSeconds(60));

    //Assert
    Assert.AreEqual(60, project.Duration);
}
```
Now that we have our test, we can start the "Refactor" phase. Lets fix our build errors by implementing our missing logic. Keep in mind, we are only **refactoring** here, so we only want to add enough code to get rid of our build errors. 

Our project class should now look like this:
```c#
public class Project
{
    public bool IsActive { get; set; }
    public double Duration { get; }

    public void Start(DateTime startTime)
    {
        IsActive = true;
    }

    public void End(DateTime endTime)
    {
        
    }
}
```

Now that we have refactored, it's time for the "Red" phase again. Go ahead and run all of our tests again. The test we wrote should now fail. Again, this is good. Based off our our test method and how we have our project class set up, it should be easy to see what logic we are missing and where to add it. Lets update our project class now:
```c#
 public class Project
{
    public bool IsActive { get; set; }
    public double Duration
    {
        get { return (_endTime - _startTime).TotalSeconds; }
    }
    private DateTime _startTime;
    private DateTime _endTime;

    public void Start(DateTime startTime)
    {
        IsActive = true;
        _startTime = startTime;
    }

    public void End(DateTime endTime)
    {
        _endTime = endTime;
    }
}
```

Now that we have updated our project class, lets go ahead and run all of our test. If done correctly, all of our tests should now be passing!

4) Let's finish up with one final test. In this test, we want to make sure we can start and stop a project multiple times and that the duration works correctly.  
As before, we will start by creating our test:
```c#
[Test]
public void ProjectCanStartAndStopAndHasDuration()
{
    //Arrange
    Project project = new Project();
    DateTime startTime = DateTime.Now;

    //Act

    //one minute
    project.Start(startTime);
    project.End(startTime.AddSeconds(60));
    //two minutes
    project.Start(startTime.AddSeconds(120));
    project.End(startTime.AddSeconds(240));
    //three minutes
    project.Start(startTime.AddSeconds(300));
    project.End(startTime.AddSeconds(660));

    //Assert
    Assert.AreEqual(60+120+360, project.Duration);
}
```
Our next step would be the "Rafactor" phase, however, our current implementation of our project class should allow the project to build. So lets go ahead and run our new test. As you can see, our test will fail, putting us in the "Red" phase. Our test is failing because our Duration property is being reset each time. Lets update our code to take care of this.   

We'll start by creating a new class named "Segment". Our project class will keep a list of different project segments. Our Segment class will look like this:
```c#
public class Segment
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public void Start(DateTime startTime)
    {
        StartTime = startTime;
    }

    public void End(DateTime endTime)
    {
        EndTime = endTime;
    }
}
```
Now we have a way to keep track of the start and end times of different segments for each project. With that done, lets update our "Project" class to use our new segments. 
```c#
public class Project
{
    public IList<Segment> Segments = new List<Segment>();
    private Segment _activeSegment;

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
}
```
Our "Project" class now lets the "Segment" class handle keeping track of the start and end times. Our "Project" class now keeps track of the active segment, starting a new one each time the project is started, and ending and adding the segment to a list each time the project is ended. Also, notice that we have updated our getter for the duration property to correctly add up the total duration of each segment.  

Go ahead and run all of our tests again. If done correctly, all of our tests should now be passing, putting us back at the "Green" phase!
