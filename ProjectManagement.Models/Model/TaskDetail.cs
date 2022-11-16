using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class TaskDetail
    {
        [Required(ErrorMessage = "Project Id is required.")]
        [Range(minimum: 1, maximum: 100000, ErrorMessage = "Project Id should be between 1 to 999999.")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Member Id is required.")]
        [Range(minimum: 100, maximum: 999999, ErrorMessage = "Manager ID should be between 100 to 999999.")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Task Name is required.")]
        [MinLength(3, ErrorMessage = "Task Name must be minimum 3 characters.")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Deliverables are required.")]
        [MinLength(3, ErrorMessage = "Deliverables must be minimum 3 characters.")]
        public string Deliverables { get; set; }

        [Required(ErrorMessage = "Task Start Date is required.")]
        public DateTime TaskStartDate { get; set; }

        [Required(ErrorMessage = "Task End Date is required.")]
        [CompareTaskDateValidator("TaskStartDate")]
        public DateTime TaskEndDate { get; set; }
    }
}
