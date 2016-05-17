using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TDDMiniHack.Tests
{
    [TestFixture]
    public class UnitTests
    {
        //Arrange
        //Act
        //Assert
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
    }
}
