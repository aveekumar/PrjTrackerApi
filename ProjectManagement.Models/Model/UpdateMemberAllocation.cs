using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class UpdateMemberAllocation
    {
        [Required(ErrorMessage = "Project ID is required.")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Member ID is required.")]
        public int MemberId { get; set; }

        [Required]
        [ValidateAllocation]
        public string AllocationPercentage { get; set; }
    }
}
