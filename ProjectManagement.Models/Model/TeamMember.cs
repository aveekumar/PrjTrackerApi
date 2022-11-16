using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class TeamMember
    {
        public TeamMember()
        {
            this.skillSets = new List<Skillset>();
        }

        [Required(ErrorMessage = "Project Id is required.")]
        [Range(minimum: 1, maximum: 999999, ErrorMessage = "Project Id should be between 1 to 999999.")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Manager ID is required.")]
        [Range(minimum: 100, maximum: 999999, ErrorMessage = "Manager ID should be between 100 to 999999.")]
        public int ManagerId { get; set; }

        [Required(ErrorMessage = "Member ID is required.")]
        [Range(minimum: 1, maximum: 99999, ErrorMessage = "Member ID should be between 1 to 99999.")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Member Name is required.")]
        [MinLength(4, ErrorMessage = "Member Name should be minimum 4 characters.")]
        public string MemberName { get; set; }

        [Required]
        [Range(minimum: 5, maximum: 80, ErrorMessage = "Member should not be added in  project as his experience is not greater than 4 years.")]
        public int NoOfYearExperience { get; set; }

        [Required]
        [ValidateSkillSet()]
        public List<Skillset> skillSets { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Profile Description must be minimum 3 characters.")]
        public string ProfileDescription { get; set; }

        [Required]
        public DateTime ProjectStartDate { get; set; }

        [Required]
        [CompareDateValidator("ProjectStartDate")]
        public DateTime ProjectEndDate { get; set; }

        [Required]
        [ValidateAllocation]
        public string AllocationPercentage { get; set; }
    }
}
