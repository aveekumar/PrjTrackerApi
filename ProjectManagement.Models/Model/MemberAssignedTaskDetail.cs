using System;

namespace ProjectManagement.Models
{
    public class MemberAssignedTaskDetail
    {
        public string MemberName { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string Deliverables { get; set; }
        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public string AllocationPercentage { get; set; }
    }
}