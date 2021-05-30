using System.ComponentModel.DataAnnotations;

namespace Jammehcow.YosherBot.EfCore.Models.Base
{
    /// <summary>
    /// Base entity for all models
    /// </summary>
    public interface IEntity
    {
        [Key]
        public int Id { get; init; }
    }
}
