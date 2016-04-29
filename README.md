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
    //Act
    project.Start();
    //Assert
    Assert.IsTrue(project.IsActive);
}
```
With our new test created, go ahead and run all of the tests. Of course our newest one will fail. THIS IS GOOD! We want to start in the "Red" phase.  
Remember, we want our test to lead to the design of our application. Based on our test, we know we need to:
*Create a project object
*Create a start method
*Create and set a IsActive property
Lets go ahead and create our project class now.
```c#
namespace TDDMiniHack
{
    public class Project
    {
        public bool IsActive { get; set; }

        public void Start()
        {
            IsActive = true;
        }
    }
}
```
*Don't forget to add a reference from our main project to the test project so our tests know about our new project class*

Now that we have our project class created, lets go ahead and run all of our tests again. If you copy/pasted everything correctly, all the test should be green. Good job! We are now in the "Green" phase.

3) Our next phase is the "Refactor" phase. We want to be able to check the duration of our projects. We will need to refactor our project class to do this. However, we won't start in the project class. We start in our test project again.
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
Now that we have our test, lets fix our build errors by implementing our missing logic. Keep in mind, we are only **refactoring** here, so we only want to add enough code to get rid of our build errors. 

Our project class should now look like this:
```c#
public class Project
{
    public bool IsActive { get; set; }
    public double Duration { get; }

    public void Start()
    {
        IsActive = true;
    }

    public void Start(DateTime startTime)
    {
        IsActive = true;
    }

    public void End(DateTime endTime)
    {
        
    }
}
```

4) Now that we have refactored, it's time for the "Red" phase again. Go ahead and run all of our tests again. The test we wrote in the last step should now fail. Again, this is good. Based off our our test method and how we have our project class set up, it should be easy to see what logic we are missing and where to add it. Lets update our project class now:
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

    public void Start()
    {
        IsActive = true;
    }

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
