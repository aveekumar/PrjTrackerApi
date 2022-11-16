using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using ProjectManagementTracker.DAL.Repository.Contract;
using System.Collections.Generic;

namespace ProjectManagementTracker.Business.Manager.Implementation
{
    public class ProjectMemberManager : IProjectMemberManager
    {
        private readonly IProjectMemberRepository _iProjectMemberRepository;
        public ProjectMemberManager(IProjectMemberRepository iProjectMemberRepository)
        {
            this._iProjectMemberRepository = iProjectMemberRepository;
        }

        /// <summary>
        ///Member can view assigned task. For that he need to pass his MemberId.
        /// </summary>
        /// <returns></returns>
        public Response<List<MemberAssignedTaskDetail>> GetAssignedTask(int memberId, int PageIndex, int PageSize, string SortByColumn, string SortOrder)
        {
            // Sort By Column Name
            switch (SortByColumn)
            {
                case "MemberName":
                    SortByColumn = "MemberName";
                    break;
                case "ProjectName":
                    SortByColumn = "ProjectName";
                    break;
                case "TaskStartDate":
                    SortByColumn = "TaskStartDate";
                    break;
                case "TaskEndDate":
                    SortByColumn = "TaskEndDate";
                    break;
                case "ProjectStartDate":
                    SortByColumn = "ProjectStartDate";
                    break;
                case "ProjectEndDate":
                    SortByColumn = "ProjectEndDate";
                    break;
                default:
                    SortByColumn = "ProjectStartDate";
                    break;
            }

            // Sorting Ording Logic
            switch (SortOrder.ToUpper())
            {
                case "DESC":
                    SortOrder = "desc";
                    break;
                case "ASC":
                    SortOrder = "asc";
                    break;
                default:
                    SortOrder = "desc";
                    break;
            }
            return _iProjectMemberRepository.GetAssignedTask(memberId, PageIndex, PageSize, SortByColumn, SortOrder);
        }
    }
}
