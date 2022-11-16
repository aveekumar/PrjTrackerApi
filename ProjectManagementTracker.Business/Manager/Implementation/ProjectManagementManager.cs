using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using ProjectManagementTracker.DAL.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProjectManagementTracker.Business.Manager.Implementation
{
    public class ProjectManagementManager : IProjectManagementManager
    {
        private readonly IProjectManagementRepository _iProjectManagementRepository;
        public ProjectManagementManager(IProjectManagementRepository iProjectManagementRepository)
        {
            this._iProjectManagementRepository = iProjectManagementRepository;
        }

        /// <summary>
        /// Add a new team member
        /// </summary>
        /// <returns></returns>
        public Response<bool> AddNewTeamMember(TeamMember teamMember)
        {
            return _iProjectManagementRepository.AddNewTeamMember(teamMember);
        }

        /// <summary>
        /// Manager can fetch details of all the team members. For that he needs to pass his ManagerId.
        /// </summary>
        /// <returns></returns>
        public Response<List<TeamMember>> GetTeamMembers(string managerId)
        {
            return _iProjectManagementRepository.GetTeamMembers(managerId);
        }

        /// <summary>
        /// Assign a task to each member of the team. 
        /// Task is assigned against particular project. So need to select project during assigning task.
        /// </summary>
        /// <returns></returns>
        public Response<bool> AssignTask(TaskDetail taskDetail)
        {
            return _iProjectManagementRepository.AssignTask(taskDetail);
        }

        /// <summary>
        /// Update Team Member Allocation
        /// </summary>
        /// <returns></returns>
        public Response<bool> UpdateTeamMemberAllocation(string projectId, string memberId, string allocationPercentage)
        {
            if (IsValidAllocationPercentage(allocationPercentage))
            {
                return _iProjectManagementRepository.UpdateTeamMemberAllocation(projectId, memberId, allocationPercentage);
            }
            else
            {
                return General.GenerateResponse<bool>(false, Constants.MsgInValidPercentage, false);
            }
        }

        private bool IsValidAllocationPercentage(string allocationPercentage)
        {
            var regExPercentage = @"^(?:100|[1-9]?[0-9])%";
            if ((Regex.IsMatch(allocationPercentage, regExPercentage)))
            {
                return true;
            }
            return false;
        }
    }
}
