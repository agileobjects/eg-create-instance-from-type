using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace CreateInstanceFromType
{
    public static class CreateInstanceFromType2020DesignTimeArgs
    {
        /// <summary>
        /// Returns an instance of this <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type an instance of which to create.</param>
        /// <returns>An instance of this <paramref name="type"/>.</returns>
        public static object GetInstance(this Type type)
            => InstanceFactoryCache.GetFactoryFor(type).Invoke();

        /// <summary>
        /// Returns an instance of this <paramref name="type"/> using its single-parameter constructor
        /// and the given <paramref name="argument"/>.
        /// </summary>
        /// <typeparam name="TArg"> 
        /// The type of the single argument to pass to the constructor. 
        /// </typeparam>
        /// <param name="type">The type an instance of which to create.</param>
        /// <param name="argument">The single argument to pass to the constructor.</param>
        /// <returns>An instance of this <paramref name="type"/>.</returns>
        public static object GetInstance<TArg>(
            this Type type,
            TArg argument)
        {
            return InstanceFactoryCache<TArg>.GetFactoryFor(type).Invoke(argument);
        }

        /// <summary>
        /// Returns an instance of this <paramref name="type"/> using its two-parameter constructor
        /// and the given <paramref name="argument1"/> and <paramref name="argument2"/>.
        /// </summary>
        /// <typeparam name="TArg1"> 
        /// The type of the first argument to pass to the constructor. 
        /// </typeparam>
        /// <typeparam name="TArg2"> 
        /// The type of the second argument to pass to the constructor. 
        /// </typeparam>
        /// <param name="type">The type an instance of which to create.</param>
        /// <param name="argument1">The first argument to pass to the constructor.</param>
        /// <param name="argument2">The second argument to pass to the constructor.</param>
        /// <returns>An instance of this <paramref name="type"/>.</returns>
        public static object GetInstance<TArg1, TArg2>(
            this Type type,
            TArg1 argument1,
            TArg2 argument2)
        {
            return InstanceFactoryCache<TArg1, TArg2>.GetFactoryFor(type)
                .Invoke(argument1, argument2);
        }

        /// <summary>
        /// Returns an instance of this <paramref name="type"/> using its two-parameter constructor
        /// and the given <paramref name="argument1"/>, <paramref name="argument2"/> and
        /// <paramref name="argument3"/>.
        /// </summary>
        /// <typeparam name="TArg1"> 
        /// The type of the first argument to pass to the constructor. 
        /// </typeparam>
        /// <typeparam name="TArg2"> 
        /// The type of the second argument to pass to the constructor. 
        /// </typeparam>
        /// <typeparam name="TArg3"> 
        /// The type of the third argument to pass to the constructor. 
        /// </typeparam>
        /// <param name="type">The type an instance of which to create.</param>
        /// <param name="argument1">The first argument to pass to the constructor.</param>
        /// <param name="argument2">The second argument to pass to the constructor.</param>
        /// <param name="argument3">The third argument to pass to the constructor.</param>
        /// <returns>An instance of this <paramref name="type"/>.</returns>
        public static object GetInstance<TArg1, TArg2, TArg3>(
            this Type type,
            TArg1 argument1,
            TArg2 argument2,
            TArg3 argument3)
        {
            return InstanceFactoryCache<TArg1, TArg2, TArg3>.GetFactoryFor(type)
                .Invoke(argument1, argument2, argument3);
        }

        private static class InstanceFactoryCache
        {
            private static readonly ConcurrentDictionary<Type, Func<object>> _factoriesByType =
                new ConcurrentDictionary<Type, Func<object>>();

            public static Func<object> GetFactoryFor(Type type)
            {
                return _factoriesByType.GetOrAdd(type, t =>
                {
                    var ctor = t.GetConstructor(Type.EmptyTypes);

                    var instanceCreation = Expression.New(ctor);

                    var instanceCreationLambda = Expression
                        .Lambda<Func<object>>(instanceCreation);

                    return instanceCreationLambda.Compile();
                });
            }
        }

        private static class InstanceFactoryCache<TArg>
        {
            private static readonly ConcurrentDictionary<Type, Func<TArg, object>> _factoriesByType =
                new ConcurrentDictionary<Type, Func<TArg, object>>();

            public static Func<TArg, object> GetFactoryFor(Type type)
            {
                return _factoriesByType.GetOrAdd(type, t =>
                {
                    var argType = typeof(TArg);

                    var ctor = t.GetConstructor(new[] { argType });
                    var argument = Expression.Parameter(argType, "param");

                    var instanceCreation = Expression.New(ctor, argument);

                    var instanceCreationLambda = Expression
                        .Lambda<Func<TArg, object>>(instanceCreation, argument);

                    return instanceCreationLambda.Compile();
                });
            }
        }

        private static class InstanceFactoryCache<TArg1, TArg2>
        {
            private static readonly ConcurrentDictionary<Type, Func<TArg1, TArg2, object>> _factoriesByType =
                new ConcurrentDictionary<Type, Func<TArg1, TArg2, object>>();

            public static Func<TArg1, TArg2, object> GetFactoryFor(Type type)
            {
                return _factoriesByType.GetOrAdd(type, t =>
                {
                    var arg1Type = typeof(TArg1);
                    var arg2Type = typeof(TArg2);

                    var ctor = t.GetConstructor(new[] { arg1Type, arg2Type });
                    var argument1 = Expression.Parameter(arg1Type, "param1");
                    var argument2 = Expression.Parameter(arg2Type, "param2");

                    var instanceCreation = Expression
                        .New(ctor, argument1, argument2);

                    var instanceCreationLambda = Expression
                        .Lambda<Func<TArg1, TArg2, object>>(instanceCreation, argument1, argument2);

                    return instanceCreationLambda.Compile();
                });
            }
        }

        private static class InstanceFactoryCache<TArg1, TArg2, TArg3>
        {
            private static readonly ConcurrentDictionary<Type, Func<TArg1, TArg2, TArg3, object>> _factoriesByType =
                new ConcurrentDictionary<Type, Func<TArg1, TArg2, TArg3, object>>();

            public static Func<TArg1, TArg2, TArg3, object> GetFactoryFor(Type type)
            {
                return _factoriesByType.GetOrAdd(type, t =>
                {
                    var arg1Type = typeof(TArg1);
                    var arg2Type = typeof(TArg2);
                    var arg3Type = typeof(TArg3);

                    var ctor = t.GetConstructor(new[] { arg1Type, arg2Type, arg3Type });
                    var argument1 = Expression.Parameter(arg1Type, "param1");
                    var argument2 = Expression.Parameter(arg2Type, "param2");
                    var argument3 = Expression.Parameter(arg3Type, "param3");

                    var instanceCreation = Expression
                        .New(ctor, argument1, argument2, argument3);

                    var instanceCreationLambda = Expression
                        .Lambda<Func<TArg1, TArg2, TArg3, object>>(
                            instanceCreation, argument1, argument2, argument3);

                    return instanceCreationLambda.Compile();
                });
            }
        }
    }
}