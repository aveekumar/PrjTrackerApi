using Moq;
using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProjectManagementTracker.Test
{
    public class ProjectMemberControllerTest
    {
        private readonly Mock<IProjectMemberManager> _iProjectMember;
        public ProjectMemberControllerTest()
        {
            _iProjectMember = new Mock<IProjectMemberManager>();
        }

        [Fact]
        public void ViewAssignedTask_Test()
        {
            // Arrange
            _iProjectMember.Setup(srvc => srvc.GetAssignedTask(221, 2, 5, "ProjectName", "ASC")).Returns(new Response<List<MemberAssignedTaskDetail>>()
            {
                Success = true,
                Message = Constants.MsgSuccess,
                Data = GetAssignedTaskList()
            });

            //Act
            var result = _iProjectMember.Object.GetAssignedTask(221, 2, 5, "ProjectName", "ASC").Data;
            //Assert
            Assert.True(result.Count == 4);
        }

        /// <summary>
        /// Get List of assigned task
        /// </summary>
        /// <returns></returns>
        private List<MemberAssignedTaskDetail> GetAssignedTaskList()
        {
            var result = new List<MemberAssignedTaskDetail>();
            result.Add(new MemberAssignedTaskDetail
            {
                MemberName = "Rahul",
                ProjectName = "PMP Aggregator",
                TaskName = "Menu functionality",
                Deliverables = "Complete Menu functionality",
                TaskStartDate = DateTime.Now,
                TaskEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(30),
                AllocationPercentage = "90%"
            });

            result.Add(new MemberAssignedTaskDetail
            {
                MemberName = "Raman",
                ProjectName = "A2Z staffing",
                TaskName = "Access Right",
                Deliverables = "Access Right Screen with functionality",
                TaskStartDate = DateTime.Now,
                TaskEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(30),
                AllocationPercentage = "85%"
            });

            result.Add(new MemberAssignedTaskDetail
            {
                MemberName = "Avee",
                ProjectName = "Data Checker",
                TaskName = "Access Right with data",
                Deliverables = "Access Right Screen with functionality with data",
                TaskStartDate = DateTime.Now,
                TaskEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(30),
                AllocationPercentage = "100%"
            });

            result.Add(new MemberAssignedTaskDetail
            {
                MemberName = "Rajan",
                ProjectName = "Parking System",
                TaskName = "Create Design for Dashboard",
                Deliverables = "Create Design for Dashboard with functionality",
                TaskStartDate = DateTime.Now,
                TaskEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(30),
                AllocationPercentage = "90%"
            });

           
            return result;
        }
    }
}
