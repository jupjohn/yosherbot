using System;
using System.Linq;
using Jammehcow.YosherBot.EfCore.Models;
using Jammehcow.YosherBot.EfCore.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Jammehcow.YosherBot.EfCore.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void EnumEntity<TEnum, TEnumEntity>(this ModelBuilder modelBuilder)
            where TEnumEntity : class, IEnumEntity, new()
            where TEnum : struct, Enum
        {
            var entities = Enum.GetValues<TEnum>()
                .Select(enumValue => new TEnumEntity {Id = Convert.ToInt32(enumValue), Name = enumValue.ToString()})
                .ToList();

            modelBuilder.Entity<TEnumEntity>()
                .HasData(entities);
        }
    }
}
