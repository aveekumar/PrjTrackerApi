using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using ProjectManagementTracker.DAL.Repository.Contract;
using System.Collections.Generic;

namespace ProjectManagementTracker.Business.Manager.Implementation
{
    public class ProjectManager : IProjectManager
    {
        private readonly IProjectRepository _iProjectRepository;
        public ProjectManager(IProjectRepository iProjectRepository)
        {
            this._iProjectRepository = iProjectRepository;
        }

        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public Response<bool> AddNewProject(string ProjectName)
        {
            return _iProjectRepository.AddNewProject(ProjectName);
        }

        /// <summary>
        /// Get list of projects
        /// </summary>
        /// <returns></returns>
        public Response<List<Project>> GetProject()
        {
            return _iProjectRepository.GetProject();
        }
    }
}
