using System;

namespace Jammehcow.YosherBot.Command.ColorMe.Helpers
{
    public static class RoleNameHelper
    {
        /// <summary>
        /// Creates a unique role name using the User's ID
        /// </summary>
        /// <param name="userId">The ulong ID of the Discord user</param>
        /// <param name="prefix">The prefix of the role. If null or empty no hyphen will be prefixed</param>
        /// <returns></returns>
        public static string GetRoleNameFromUserId(ulong userId, string prefix)
        {
            var roleHexPart = Convert.ToBase64String(BitConverter.GetBytes(userId));

            return string.IsNullOrWhiteSpace(prefix)
                ? roleHexPart
                : string.Join('-', prefix, roleHexPart);
        }
    }
}
