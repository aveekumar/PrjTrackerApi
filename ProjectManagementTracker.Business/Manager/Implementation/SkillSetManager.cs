using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using ProjectManagementTracker.DAL.Repository.Contract;
using System.Collections.Generic;

namespace ProjectManagementTracker.Business.Manager.Implementation
{
    public class SkillSetManager : ISkillSetManager
    {
        private readonly ISkillSetRepository _iSkillSetRepository;
        public SkillSetManager(ISkillSetRepository iSkillSetRepository)
        {
            this._iSkillSetRepository = iSkillSetRepository;
        }

        /// <summary>
        /// Add new Skill. This is used during adding new team member by Manager.
        /// Manager needs to select atleast 3 skill set.
        /// </summary>
        /// <param name="SkillName"></param>
        /// <returns></returns>
        public Response<bool> AddNewSkill(string skillName)
        {
            return _iSkillSetRepository.AddNewSkill(skillName);
        }

        /// <summary>
        /// Get list of Skills.
        /// </summary>
        /// <returns></returns>
        public Response<List<Skillset>> GetSkillsets()
        {
            return _iSkillSetRepository.GetSkillsets();
        }
    }
}
