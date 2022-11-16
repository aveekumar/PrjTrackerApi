using ProjectManagement.Models;
using System.Collections.Generic;

namespace ProjectManagementTracker.DAL.Repository.Contract
{
    public interface IProjectMemberRepository
    {
        /// <summary>
        ///Member can view assigned task. For that he need to pass his MemberId.
        /// </summary>
        /// <returns></returns>
        Response<List<MemberAssignedTaskDetail>> GetAssignedTask(int memberId, int PageIndex, int PageSize, string SortByColumn, string SortOrder);
    }
}
