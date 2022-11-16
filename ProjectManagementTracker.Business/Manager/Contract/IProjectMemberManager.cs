using ProjectManagement.Models;
using System.Collections.Generic;

namespace ProjectManagementTracker.Business.Manager.Contract
{
    public interface IProjectMemberManager
    {
        /// <summary>
        ///Member can view assigned task. For that he need to pass his MemberId.
        /// </summary>
        /// <returns></returns>
        Response<List<MemberAssignedTaskDetail>> GetAssignedTask(int memberId, int PageIndex, int PageSize, string SortByColumn, string SortOrder);
    }
}
