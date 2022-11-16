namespace ProjectManagement.Models
{
    public static class Constants
    {
        

        public readonly static string ProcSaveProject = "Proc_SaveProject";
        public readonly static string ProjectAdded = "Project added successfully.";
        public readonly static string ProcGetProjectList = "Proc_GetProjectList";
        public readonly static string ProcGetAssignedTaskByMemberId = "Proc_GetAssignedTaskByMemberId";
        public readonly static string ProcSaveSkillList = "Proc_SaveSkillList";
        public readonly static string ProcGetSkillList = "Proc_GetSkillList";
        public readonly static string ProcGetMemberDetailByManagerId = "Proc_GetMemberDetailByManagerId";
        public readonly static string ProcAssignTaskToMember = "Proc_AssignTaskToMember";
        public readonly static string ProcUpdateAllocation = "Proc_UpdateAllocation";
        public readonly static string ProcSaveMemberDetail = "Proc_SaveMemberDetail";
        public readonly static string MsgInValidPercentage = "Invalid Allocation Percentage.Valid allocation percentage value is: 30% or 70%.";
        public readonly static string MsgSkillAdded = "Skill added successfully.";     
        public readonly static string MsgMemberAdded = "Team member added successfully.";
        public readonly static string MsgTaskAssigned = "Task is assigned successfully.";
        public readonly static string MsgAllocationUpdated = "Allocation updated successfully.";
        public readonly static string MsgEmptyProjectName = "Project Name should not be blank.";
        public readonly static string MsgEmptyManagerId = "Manager Id should not be blank.";
        public readonly static string MsgEmptySkillName = "Skill Name should not be blank.";
        public readonly static string MsgEmptyMemberId = "Member Id should not be blank.";       
        public readonly static string MsgPayload = "Request Payload";
        public readonly static string MsgResponse = "Response";
        public readonly static string MsgSuccess = "Success";
    }
}
