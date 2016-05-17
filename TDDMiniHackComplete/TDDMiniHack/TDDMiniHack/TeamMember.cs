using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDMiniHack
{
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
}
