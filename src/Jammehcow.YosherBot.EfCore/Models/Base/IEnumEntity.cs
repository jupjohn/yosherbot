using System.ComponentModel.DataAnnotations;

namespace Jammehcow.YosherBot.EfCore.Models.Base
{
    public interface IEnumEntity : IEntity
    {
        [Required]
        public string Name { get; init; }
    }
}
