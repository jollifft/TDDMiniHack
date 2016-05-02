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

4) 
