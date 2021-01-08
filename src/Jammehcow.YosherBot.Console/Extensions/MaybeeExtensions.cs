using System;
using MayBee;

namespace Jammehcow.YosherBot.Console.Extensions
{
    // ReSharper disable once IdentifierTypo
    /// <summary>
    /// Extensions to the Maybee library
    /// </summary>
    public static class MaybeeExtensions
    {
        /// <summary>
        /// Invokes the given Action with the value of the Maybe object (type T) if it exists
        /// </summary>
        /// <param name="maybe">The Maybe to operate on</param>
        /// <param name="action">The action to execute if a value is present</param>
        /// <typeparam name="T">The inner type of the Maybe instance</typeparam>
        public static Maybe<T> IfExists<T>(this Maybe<T> maybe, Action<T> action)
        {
            if (maybe.IsEmpty)
                return maybe;

            action.Invoke(maybe.It);
            return maybe;
        }

        /// <summary>
        /// Invokes the given Action if the Maybe object has a value
        /// </summary>
        /// <param name="maybe">The Maybe to operate on</param>
        /// <param name="action">The action to execute if a value is present</param>
        /// <typeparam name="T">The inner type of the Maybe instance</typeparam>
        public static Maybe<T> IfExists<T>(this Maybe<T> maybe, Action action)
        {
            if (maybe.IsEmpty)
                return maybe;

            action.Invoke();
            return maybe;
        }

        /// <summary>
        /// Invokes the given Action if the Maybe object does not have a value
        /// </summary>
        /// <param name="maybe">The Maybe to operate on</param>
        /// <param name="action">The action to execute if a value is not present</param>
        /// <typeparam name="T">The inner type of the Maybe instance</typeparam>
        public static Maybe<T> IfMissing<T>(this Maybe<T> maybe, Action action)
        {
            if (maybe.Exists)
                return maybe;

            action.Invoke();
            return maybe;
        }
    }
}