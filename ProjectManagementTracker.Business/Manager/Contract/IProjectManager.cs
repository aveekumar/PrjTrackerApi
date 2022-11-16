using ProjectManagement.Models;
using System.Collections.Generic;

namespace ProjectManagementTracker.Business.Manager.Contract
{
    public interface IProjectManager
    {
        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        Response<bool> AddNewProject(string ProjectName);

        /// <summary>
        /// Get list of projects
        /// </summary>
        /// <returns></returns>
        Response<List<Project>> GetProject();
    }
}
