using Moq;
using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using System.Collections.Generic;
using Xunit;

namespace ProjectManagementTracker.Test
{
    public class ProjectControllerTest
    {
        private readonly Mock<IProjectManager> _iProjectMgr;
        public ProjectControllerTest()
        {
            _iProjectMgr = new Mock<IProjectManager>();
        }

        /// <summary>
        /// Add new Project - Test case
        /// </summary>
        [Fact]
        public void AddNewProject_Test()
        {
            // Arrange
            var newProjectName = "Staffing Tracker";
            _iProjectMgr.Setup(srvc => srvc.AddNewProject(newProjectName)).Returns(new Response<bool>()
            {
                Success = true,
                Message = Constants.MsgSuccess,
                Data = true
            });
            //Act
            var result = _iProjectMgr.Object.AddNewProject(newProjectName).Data;
            //Assert 
            Assert.True(result == true);
        }

        /// <summary>
        /// Get list of Project Test case
        /// </summary>
        [Fact]
        public void GetProject_Test()
        {
            // Arrange
            _iProjectMgr.Setup(srvc => srvc.GetProject()).Returns(new Response<List<Project>>()
            {
                Success = true,
                Message = Constants.MsgSuccess,
                Data = GetProjectsList()
            });

            //Act
            var result = _iProjectMgr.Object.GetProject().Data;
            //Assert
            Assert.True(result.Count == 3);
        }

        /// <summary>
        /// Get list of Project for mocking
        /// </summary>
        /// <returns></returns>
        private List<Project> GetProjectsList()
        {
            var result = new List<Project>();
            result.Add(new Project { ProjectId = 1, ProjectName = "PMP Aggregator" });
            result.Add(new Project { ProjectId = 2, ProjectName = "A2Z staffing" });
            result.Add(new Project { ProjectId = 3, ProjectName = "Data Checker" });
            return result;
        }
    }
}
