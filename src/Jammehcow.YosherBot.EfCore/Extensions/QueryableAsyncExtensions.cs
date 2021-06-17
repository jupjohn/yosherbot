using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MayBee;
using Microsoft.EntityFrameworkCore;

namespace Jammehcow.YosherBot.EfCore.Extensions
{
    public static class EnumerableAsyncExtensions
    {
        public static async Task<Maybe<T>> SingleAsMaybeAsync<T>(this IQueryable<T> enumerable,
            Expression<Func<T, bool>> predicate) where T : class
        {
            return Maybe.FromNullable(await enumerable.SingleOrDefaultAsync(predicate));
        }
    }
}
