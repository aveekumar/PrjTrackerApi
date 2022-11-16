using Microsoft.Extensions.Configuration;
using ProjectManagement.Models;
using ProjectManagementTracker.DAL.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjectManagementTracker.DAL.Repository.Implementation
{
    public class SkillSetRepository : ISkillSetRepository
    {
        private static IConfiguration _config;
        private readonly string connectionString = string.Empty;
        public SkillSetRepository(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:DefaultConnection"].ToString();
        }

        /// <summary>
        /// Add new Skill. This is used during adding new team member by Manager.
        /// Manager needs to select atleast 3 skill set.
        /// </summary>
        /// <param name="SkillName"></param>
        /// <returns></returns>
        public Response<bool> AddNewSkill(string skillName)
        {
            var result = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Constants.ProcSaveSkillList, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SkillName", skillName);

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
                            return General.GenerateResponse<bool>(true, Constants.MsgSkillAdded, true);
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

        /// <summary>
        /// Get list of Skills.
        /// </summary>
        /// <returns></returns>
        public Response<List<Skillset>> GetSkillsets()
        {
            var result = new List<Skillset>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Constants.ProcGetSkillList, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            result.Add(new Skillset
                            {
                                SkillId = Convert.ToInt32(dr["SkillId"]),
                                SkillName = Convert.ToString(dr["SkillName"])
                            });
                        }
                        dr.Close();
                        return General.GenerateResponse<List<Skillset>>(true, Constants.MsgSuccess, result);
                    }
                }
            }
            catch (Exception ex)
            {
                return General.GenerateResponse<List<Skillset>>(false, ex.Message, result);
            }
        }
    }
}
