using ProjectManagement.Models;
using System.Collections.Generic;

namespace ProjectManagementTracker.DAL.Repository.Contract
{
    public interface ISkillSetRepository
    {
        /// <summary>
        /// Add new Skill. This is used during adding new team member by Manager.
        /// Manager needs to select atleast 3 skill set.
        /// </summary>
        /// <param name="SkillName"></param>
        /// <returns></returns>
        Response<bool> AddNewSkill(string skillName);

        /// <summary>
        /// Get list of Skills.
        /// </summary>
        /// <returns></returns>
        Response<List<Skillset>> GetSkillsets();
    }
}
