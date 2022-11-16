using Moq;
using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjectManagementTracker.Test
{
    public class ProjectManagementControllerTest
    {
        private readonly Mock<IProjectManagementManager> _iProjectMgmttMgr;
        public ProjectManagementControllerTest()
        {
            _iProjectMgmttMgr = new Mock<IProjectManagementManager>();
        }

        /// <summary>
        /// Add new member to team- Test case
        /// </summary>
        [Fact]
        public void AddNewTeamMember_Test()
        {
            // Arrange
            var teamMember = new TeamMember()
            {
                ProjectId = 1,
                ManagerId = 110,
                MemberId = 220,
                MemberName = "Rahul",
                NoOfYearExperience = 9,
                skillSets = new List<Skillset>() { new Skillset() { SkillId = 2 }, new Skillset() { SkillId = 3 }, new Skillset() { SkillId = 4 } },
                AllocationPercentage = "100%",
                ProfileDescription = ".net Developer",
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddMonths(2)
            };

            _iProjectMgmttMgr.Setup(srvc => srvc.AddNewTeamMember(teamMember)).Returns(new Response<bool>()
            {
                Success = true,
                Message = Constants.MsgSuccess,
                Data = true
            });
            //Act
            var result = _iProjectMgmttMgr.Object.AddNewTeamMember(teamMember).Data;
            //Assert 
            Assert.True(result == true);
        }

        /// <summary>
        /// Get team member by managerId - Test case
        /// </summary>
        [Fact]
        public void GetTeamMembers_Test()
        {
            // Arrange
            var managerId = "110";
            _iProjectMgmttMgr.Setup(srvc => srvc.GetTeamMembers(managerId)).Returns(new Response<List<TeamMember>>()
            {
                Success = true,
                Message = Constants.MsgSuccess,
                Data = GetTeamMembersDetail()
            });

            //Act
            var result = _iProjectMgmttMgr.Object.GetTeamMembers(managerId).Data;
            //Assert
            Assert.True(result.Where(x => x.ManagerId == Convert.ToInt32(managerId)).Count() == 2);
        }

        /// <summary>
        /// Assign Task to team member - test case
        /// </summary>
        [Fact]
        public void AssignTask_Test()
        {
            // Arrange
            var taskDetail = new TaskDetail()
            {
                ProjectId = 1,
                MemberId = 220,
                TaskName = "Menu creation",
                Deliverables = "Complete Menu functionality",
                TaskStartDate = DateTime.Now.AddDays(10),
                TaskEndDate = DateTime.Now.AddDays(100)
            };

            _iProjectMgmttMgr.Setup(srvc => srvc.AssignTask(taskDetail)).Returns(new Response<bool>()
            {
                Success = true,
                Message = Constants.MsgSuccess,
                Data = true
            });
            //Act
            var result = _iProjectMgmttMgr.Object.AssignTask(taskDetail).Data;
            //Assert 
            Assert.True(result == true);
        }

        [Fact]
        public void UpdateTeamMemberAllocation()
        {
            // Arrange
            string projectId = "1";
            string memberId = "220";
            string allocationPercentage = "100%";

            _iProjectMgmttMgr.Setup(srvc => srvc.UpdateTeamMemberAllocation(projectId, memberId, allocationPercentage)).Returns(new Response<bool>()
            {
                Success = true,
                Message = Constants.MsgSuccess,
                Data = true
            });
            //Act
            var result = _iProjectMgmttMgr.Object.UpdateTeamMemberAllocation(projectId, memberId, allocationPercentage).Data;
            //Assert 
            Assert.True(result == true);
        }

        /// <summary>
        /// Get list of Team member by managerId - Test case
        /// </summary>
        /// <returns></returns>
        private List<TeamMember> GetTeamMembersDetail()
        {
            var result = new List<TeamMember>();
            result.Add(new TeamMember() { ManagerId = 110, MemberId = 222, MemberName = "Raman" });
            result.Add(new TeamMember() { ManagerId = 110, MemberId = 223, MemberName = "Sahil" });
            result.Add(new TeamMember() { ManagerId = 111, MemberId = 224, MemberName = "Sachin" });
            return result;
        }
    }
}
