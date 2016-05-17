using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDMiniHack
{
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
}
