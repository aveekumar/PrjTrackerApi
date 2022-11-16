using Moq;
using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using System.Collections.Generic;
using Xunit;

namespace ProjectManagementTracker.Test
{
    public class SkillSetControllerTest
    {
        private readonly Mock<ISkillSetManager> _iSkillSetManager;
        public SkillSetControllerTest()
        {
            _iSkillSetManager = new Mock<ISkillSetManager>();
        }

        /// <summary>
        /// Add new Skill - Test case
        /// </summary>
        [Fact]
        public void AddNewSkill_Test()
        {
            // Arrange
            var newSkill = "React";
            _iSkillSetManager.Setup(srvc => srvc.AddNewSkill(newSkill)).Returns(new Response<bool>()
            {
                Success = true,
                Message = Constants.MsgSuccess,
                Data = true
            });
            //Act
            var result = _iSkillSetManager.Object.AddNewSkill(newSkill).Data;
            //Assert 
            Assert.True(result == true);
        }

        /// <summary>
        /// Get list of Project Test case
        /// </summary>
        [Fact]
        public void GetSkillSets_Test()
        {
            // Arrange
            _iSkillSetManager.Setup(srvc => srvc.GetSkillsets()).Returns(new Response<List<Skillset>>()
            {
                Success = true,
                Message = Constants.MsgSuccess,
                Data = GetSkillSets()
            });

            //Act
            var result = _iSkillSetManager.Object.GetSkillsets().Data;
            //Assert
            Assert.True(result.Count == 3);
        }

        /// <summary>
        /// Get list of Project for mocking
        /// </summary>
        /// <returns></returns>
        private List<Skillset> GetSkillSets()
        {
            var result = new List<Skillset>();
            result.Add(new Skillset { SkillId = 1, SkillName = "MVC" });
            result.Add(new Skillset { SkillId = 2, SkillName = "React" });
            result.Add(new Skillset { SkillId = 3, SkillName = "Java Script" });
            return result;
        }
    }
}
