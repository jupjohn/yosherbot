namespace Jammehcow.YosherBot.Command.ColorMe.Helpers
{
    public static class RoleNameHelper
    {
        public static string GetRoleNameFromUserId(ulong userId, string prefix)
        {
            var roleHexPart = userId.ToString("X").ToLowerInvariant();
            return string.Join('-', prefix, roleHexPart);
        }
    }
}