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
2) Now that we know our test project is working, we can actually get into the meat of our application. Lets start by creating a test to make sure new projects are active when they are first created. However, we wont start by creating a project model. We will start with a test to make sure a project is active when it is created.
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

