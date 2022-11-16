using Microsoft.Extensions.Configuration;
using ProjectManagement.Models;
using ProjectManagementTracker.DAL.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace ProjectManagementTracker.DAL.Repository.Implementation
{
    public class ProjectManagementRepository : IProjectManagementRepository
    {
        private static IConfiguration _config;
        private readonly string connectionString = string.Empty;
        public ProjectManagementRepository(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:DefaultConnection"].ToString();
        }

        /// <summary>
        /// Add new team member
        /// </summary>
        /// <param name="teamMember"></param>
        /// <returns></returns>
        public Response<bool> AddNewTeamMember(TeamMember teamMember)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Constants.ProcSaveMemberDetail, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", teamMember.ProjectId);
                        cmd.Parameters.AddWithValue("@ManagerId", teamMember.ManagerId);
                        cmd.Parameters.AddWithValue("@MemberId", teamMember.MemberId);
                        cmd.Parameters.AddWithValue("@MemberName", teamMember.MemberName);
                        cmd.Parameters.AddWithValue("@NoOfYearExperience", teamMember.NoOfYearExperience);
                        cmd.Parameters.AddWithValue("@SkillSet", string.Join(',', teamMember.skillSets.ToList().Select(x => x.SkillId)));
                        cmd.Parameters.AddWithValue("@ProfileDescription", teamMember.ProfileDescription);
                        cmd.Parameters.AddWithValue("@ProjectStartDate", teamMember.ProjectStartDate);
                        cmd.Parameters.AddWithValue("@ProjectEndDate", teamMember.ProjectEndDate);
                        cmd.Parameters.AddWithValue("@AllocationPercentage", teamMember.AllocationPercentage);

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
                            return General.GenerateResponse<bool>(true, Constants.MsgMemberAdded, true);
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
        /// Manager can fetch details of all the team members. For that he needs to pass his ManagerId.
        /// </summary>
        /// <returns></returns>
        public Response<List<TeamMember>> GetTeamMembers(string managerId)
        {
            var result = new List<TeamMember>();
            var skillSet = new List<Skillset>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Constants.ProcGetMemberDetailByManagerId, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ManagerId", managerId);

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            skillSet.Add(new Skillset
                            {
                                SkillId = Convert.ToInt32(dr["SkillId"]),
                                SkillName = Convert.ToString(dr["SkillName"])
                            });
                        }

                        dr.NextResult();
                        while (dr.Read())
                        {
                            var skills = Convert.ToString(dr["SkillSet"]);
                            result.Add(new TeamMember
                            {
                                ProjectId = Convert.ToInt32(dr["ProjectId"]),
                                ManagerId = Convert.ToInt32(dr["ManagerId"]),
                                MemberId = Convert.ToInt32(dr["MemberId"]),
                                MemberName = Convert.ToString(dr["MemberName"]),
                                NoOfYearExperience = Convert.ToInt32(dr["NoOfYearExperience"]),
                                ProfileDescription = Convert.ToString(dr["ProfileDescription"]),
                                ProjectStartDate = Convert.ToDateTime(dr["ProjectStartDate"]),
                                ProjectEndDate = Convert.ToDateTime(dr["ProjectEndDate"]),
                                AllocationPercentage = Convert.ToString(dr["AllocationPercentage"]),
                                skillSets = GetSkillSets(skills, skillSet)
                            });
                        }
                        dr.Close();
                        return General.GenerateResponse<List<TeamMember>>(true, Constants.MsgSuccess, result);
                    }
                }
            }
            catch (Exception ex)
            {
                return General.GenerateResponse<List<TeamMember>>(false, ex.Message, result);
            }
        }

        /// <summary>
        /// Assign a task to each member of the team. 
        /// Task is assigned against particular project. So need to select project during assigning task.
        /// </summary>
        /// <returns></returns>
        public Response<bool> AssignTask(TaskDetail taskDetail)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Constants.ProcAssignTaskToMember, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProjectId", taskDetail.ProjectId);
                        cmd.Parameters.AddWithValue("@MemberId", taskDetail.MemberId);
                        cmd.Parameters.AddWithValue("@TaskName", taskDetail.TaskName);
                        cmd.Parameters.AddWithValue("@Deliverables", taskDetail.Deliverables);
                        cmd.Parameters.AddWithValue("@TaskStartDate", taskDetail.TaskStartDate);
                        cmd.Parameters.AddWithValue("@TaskEndDate", taskDetail.TaskEndDate);
                        cmd.CommandType = CommandType.StoredProcedure;

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
                            return General.GenerateResponse<bool>(true, Constants.MsgTaskAssigned, true);
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
        /// Update Team Member Allocation
        /// </summary>
        /// <returns></returns>
        public Response<bool> UpdateTeamMemberAllocation(string projectId, string memberId, string allocationPercentage)
        {
            try
            {
                var result = string.Empty;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Constants.ProcUpdateAllocation, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Parameters.AddWithValue("@MemberId", memberId);
                        cmd.Parameters.AddWithValue("@AllocationPercentage", allocationPercentage);

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            result = Convert.ToString(dr["Result"]);
                        }
                        dr.Close();
                        if (result.Equals("SUCCESS"))
                        {
                            return General.GenerateResponse<bool>(true, Constants.MsgAllocationUpdated, true);
                        }
                        else
                        {
                            return General.GenerateResponse<bool>(false, result, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return General.GenerateResponse<bool>(false, ex.Message, false);
            }
        }

        private List<Skillset> GetSkillSets(string skills, List<Skillset> skillSet)
        {
            var skillSetDetail = new List<Skillset>();
            var memberSkills = skills.Split(',').ToList();
            skillSetDetail = (from t1 in skillSet where memberSkills.Any(x => Convert.ToInt32(x) == t1.SkillId) select t1).ToList();
            return skillSetDetail;
        }
    }
}
