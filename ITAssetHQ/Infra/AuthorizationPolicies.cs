namespace IT_Hardware.Infra
{
    public static class AuthorizationPolicies
    {
        /// <summary>
        /// this policy stipulates that users in both GroupMember and GroupAdmin can access resources
        /// </summary>
        public const string AssignmentToGroupMemberGroupRequired = "AssignmentToGroupMemberGroupRequired";

        /// <summary>
        /// this policy stipulates that users in GroupAdmin can access resources
        /// </summary>
        public const string AssignmentToGroupAdminGroupRequired = "AssignmentToGroupAdminGroupRequired";

        public const string AllAccess = "AllAccess";

        public const string ITStaffs = "ITStaffs";

        public const string ITManagers = "ITManagers";

        public const string ITSupportEngineers = "ITSupportEngineers";

      
    }
}
