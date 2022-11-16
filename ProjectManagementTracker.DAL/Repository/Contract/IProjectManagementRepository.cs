using ProjectManagement.Models;
using System.Collections.Generic;

namespace ProjectManagementTracker.DAL.Repository.Contract
{
    public interface IProjectManagementRepository
    {
        /// <summary>
        /// Add a new team member
        /// </summary>
        /// <returns></returns>
        Response<bool> AddNewTeamMember(TeamMember teamMember);

        /// <summary>
        /// Manager can fetch details of all the team members. For that he needs to pass his ManagerId.
        /// </summary>
        /// <returns></returns>
        Response<List<TeamMember>> GetTeamMembers(string memberId);

        /// <summary>
        /// Assign a task to each member of the team. 
        /// Task is assigned against particular project. So need to select project during assigning task.
        /// </summary>
        /// <returns></returns>
        Response<bool> AssignTask(TaskDetail taskDetail);

        /// <summary>
        /// Update Team Member Allocation
        /// </summary>
        /// <returns></returns>
        Response<bool> UpdateTeamMemberAllocation(string projectId, string memberId, string allocationPercentage);
    }
}
