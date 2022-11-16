using Microsoft.Extensions.Configuration;
using ProjectManagement.Models;
using ProjectManagementTracker.DAL.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjectManagementTracker.DAL.Repository.Implementation
{
    public class ProjectMemberRepository : IProjectMemberRepository
    {
        private static IConfiguration _config;
        private readonly string connectionString = string.Empty;
        public ProjectMemberRepository(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:DefaultConnection"].ToString();
        }

        /// <summary>
        ///Member can view assigned task. For that he need to pass his MemberId.
        /// </summary>
        /// <returns></returns>
        public Response<List<MemberAssignedTaskDetail>> GetAssignedTask(int memberId, int PageIndex, int PageSize, string SortByColumn, string SortOrder)
        {
            var result = new List<MemberAssignedTaskDetail>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Constants.ProcGetAssignedTaskByMemberId, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MemberId", memberId);
                        cmd.Parameters.AddWithValue("@PageIndex", PageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", PageSize);
                        cmd.Parameters.AddWithValue("@SortByColumn", SortByColumn);
                        cmd.Parameters.AddWithValue("@SortOrder", SortOrder);

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            result.Add(new MemberAssignedTaskDetail
                            {
                                MemberName = Convert.ToString(dr["MemberName"]),
                                ProjectName = Convert.ToString(dr["ProjectName"]),
                                TaskName = Convert.ToString(dr["TaskName"]),
                                Deliverables = Convert.ToString(dr["Deliverables"]),
                                TaskStartDate = Convert.ToDateTime(dr["TaskStartDate"]),
                                TaskEndDate = Convert.ToDateTime(dr["TaskEndDate"]),
                                ProjectStartDate = Convert.ToDateTime(dr["ProjectStartDate"]),
                                ProjectEndDate = Convert.ToDateTime(dr["ProjectEndDate"]),
                                AllocationPercentage = Convert.ToString(dr["AllocationPercentage"]),
                            });
                        }
                        dr.Close();
                        return General.GenerateResponse<List<MemberAssignedTaskDetail>>(true, Constants.MsgSuccess, result);
                    }
                }
            }
            catch (Exception ex)
            {
                return General.GenerateResponse<List<MemberAssignedTaskDetail>>(false, ex.Message, result);
            }
        }
    }
}
