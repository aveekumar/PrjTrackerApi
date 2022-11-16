using Microsoft.Extensions.Configuration;
using ProjectManagement.Models;
using ProjectManagementTracker.DAL.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjectManagementTracker.DAL.Repository.Implementation
{
    public class ProjectRepository : IProjectRepository
    {
        private static IConfiguration _config;
        private readonly string connectionString = string.Empty;
        public ProjectRepository(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:DefaultConnection"].ToString();
        }

        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public Response<bool> AddNewProject(string ProjectName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Constants.ProcSaveProject, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectName", ProjectName?.Trim());

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlDataReader dr = cmd.ExecuteReader();
                        var status = string.Empty;
                        if (dr.Read())
                        {
                            status = Convert.ToString(dr["Result"]);
                        }
                        dr.Close();
                        if (status.Equals("SUCCESS"))
                        {
                            return General.GenerateResponse<bool>(true, Constants.ProjectAdded, true);
                        }
                        else
                        {
                            return General.GenerateResponse<bool>(false, status, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return General.GenerateResponse<bool>(false, ex.Message, false);
            }
        }

        /// <summary>
        /// Get list of projects
        /// </summary>
        /// <returns></returns>
        public Response<List<Project>> GetProject()
        {
            var result = new List<Project>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Constants.ProcGetProjectList, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            result.Add(new Project
                            {
                                ProjectId = Convert.ToInt32(dr["ProjectId"]),
                                ProjectName = Convert.ToString(dr["ProjectName"])
                            });
                        }
                        dr.Close();
                        return General.GenerateResponse<List<Project>>(true, Constants.MsgSuccess, result);
                    }
                }
            }
            catch (Exception ex)
            {
                return General.GenerateResponse<List<Project>>(false, ex.Message, result);
            }
        }
    }
}
