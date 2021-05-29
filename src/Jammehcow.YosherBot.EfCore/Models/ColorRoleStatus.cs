using Jammehcow.YosherBot.EfCore.Models.Base;

namespace Jammehcow.YosherBot.EfCore.Models
{
    /// <summary>
    /// The status of a color role
    /// </summary>
    public class ColorRoleStatus : IEnumEntity
    {
        public int Id { get; set; }
        public string Name { get; init; } = null!;
    }
}
