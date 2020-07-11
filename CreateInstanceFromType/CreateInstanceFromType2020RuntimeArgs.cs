namespace CreateInstanceFromType
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class CreateInstanceFromType2020RuntimeArgs
    {
        private static readonly ConcurrentDictionary<TypeFactoryKey, Func<object[], object>> _factoriesByType =
            new ConcurrentDictionary<TypeFactoryKey, Func<object[], object>>();

        /// <summary>
        /// Returns an instance of this <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type an instance of which should be created.</param>
        /// <param name="arguments">The arguments to pass to the type constructor.</param>
        /// <returns>An instance of this given <paramref name="type"/>.</returns>
        public static object GetInstance(this Type type, params object[] arguments)
        {
            var factoryKey = new TypeFactoryKey(type, arguments);
            var typeFactory = _factoriesByType.GetOrAdd(factoryKey, CreateObjectFactory);

            return typeFactory.Invoke(arguments);
        }

        private static Func<object[], object> CreateObjectFactory(TypeFactoryKey key)
        {
            var argumentTypes = key.ArgumentTypes;

            // The constructor which matches the given argument types:
            var instanceTypeCtor = key.Type.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                CallingConventions.HasThis,
                argumentTypes,
                new ParameterModifier[0]);

            // An Expression representing the parameter to pass
            // to the Func:
            var lambdaParameters = Expression.Parameter(typeof(object[]), "params");

            // A set of Expressions representing the parameters to pass
            // to the constructor:
            var argumentCount = argumentTypes.Length;
            var ctorArguments = new Expression[argumentCount];

            for (var i = 0; i < argumentCount; ++i)
            {
                var argumentType = argumentTypes[i];

                // Access the approriate Lambda parameter by index:
                var lambdaParameter = Expression
                    .ArrayAccess(lambdaParameters, Expression.Constant(i));

                // Convert the lambda parameter to the constructor
                // parameter type if necessary:
                ctorArguments[i] = argumentType == typeof(object)
                    ? (Expression)lambdaParameter
                    : Expression.Convert(lambdaParameter, argumentType);
            }

            // An Expression representing the constructor call, 
            // passing in the constructor parameters:
            var instanceCreation = Expression.New(instanceTypeCtor, ctorArguments);

            // Compile the Expression into a Func which takes an 
            // object argument array and returns the constructed object:
            var instanceCreationLambda = Expression
                .Lambda<Func<object[], object>>(instanceCreation, lambdaParameters);

            return instanceCreationLambda.Compile();
        }

        #region Helper Classes

        private class TypeFactoryKey
        {
            private readonly int _hashCode;

            public TypeFactoryKey(Type type, object[] arguments)
            {
                Type = type;
                _hashCode = type.GetHashCode();

                var argumentCount = arguments.Length;

                unchecked
                {
                    switch (argumentCount)
                    {
                        case 0:
                            ArgumentTypes = Type.EmptyTypes;
                            return;

                        case 1:
                            {
                                var argument = arguments[0];
                                var argumentType = argument.GetType();
                                ArgumentTypes = new[] { argumentType };
                                _hashCode = GetHashCodeValue(argumentType);
                                return;
                            }

                        default:
                            ArgumentTypes = new Type[argumentCount];

                            for (var i = 0; i < argumentCount; ++i)
                            {
                                var argument = arguments[i];
                                var argumentType = argument.GetType();
                                ArgumentTypes[i] = argumentType;
                                _hashCode = GetHashCodeValue(argumentType);
                            }

                            return;
                    }
                }
            }

            private int GetHashCodeValue(Type argumentType)
                => (_hashCode * 397) ^ argumentType.GetHashCode();

            public Type Type { get; }

            public Type[] ArgumentTypes { get; }

            public override bool Equals(object obj)
                => ((TypeFactoryKey)obj)._hashCode == _hashCode;

            public override int GetHashCode() => _hashCode;
        }

        #endregion
    }
}
