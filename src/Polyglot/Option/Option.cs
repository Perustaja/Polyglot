using System;

namespace Perustaja.Polyglot.Option
{
    public abstract class Option<T>
    {
        public static Option<T> Some(T val) => new Choices.Some(val);

        public static Option<T> None => new Choices.None();

        /// <summary>
        /// Maps the given Option to one of a new type by invoking the function argument.
        /// </summary>
        public Option<U> Map<U>(Func<T, U> func)
            => Match(
                s => Option<U>.Some(func(s)),
                () => Option<U>.None
            );
        
        /// <summary>
        /// Returns the underlying value after applying the function if Some. Returns None if None.
        /// </summary>
        public Option<T> AndThen(Func<T, Option<T>> func)
            => Match(
                s => func(s),
                () => Option<T>.None
            );

        public abstract bool IsSome();

        public abstract bool IsNone();

        /// <summary>
        /// Use is discouraged, will return the value if Some or throw an exception if None.
        /// Prefer to use UnwrapOr().
        /// </summary>
        public abstract T Unwrap();

        /// <summary>
        /// Use is discouraged, will return the value if Some or throw an exception if None.
        /// Prefer to use UnwrapOr().
        /// </summary>
        public abstract T UnwrapOr(T fallback);

        /// <summary>
        /// Returns U based upon whether the function is Some or None.
        /// </summary>
        public abstract U Match<U>(Func<T, U> someFunc, Func<U> noneFunc);

        /// <summary>
        /// Invokes someAction if type is Some, or noneAction if type is None. This function returns void and is not for a functional approach,
        /// but rather as a very simple substitute for a match block.
        /// </summary>
        public abstract void Match(Action<T> someAction, Action noneAction);

        private static class Choices
        {
            public sealed class Some : Option<T>
            {
                private T _value { get; }

                public Some(T val) => _value = val;

                public override bool IsSome() => true;

                public override bool IsNone() => false;

                public override T Unwrap() => _value;

                public override T UnwrapOr(T fallback) => _value;

                public override U Match<U>(Func<T, U> someFunc, Func<U> noneFunc) => someFunc(_value);

                public override void Match(Action<T> someAction, Action noneAction)
                    => someAction?.Invoke(_value);

            }

            public sealed class None : Option<T>
            {

                public override bool IsSome() => false;

                public override bool IsNone() => true;

                public override T Unwrap() => throw new Exception("Attempted to unwrap None.");

                public override T UnwrapOr(T fallback) => fallback;
                public override U Match<U>(Func<T, U> someFunc, Func<U> noneFunc) => noneFunc();

                public override void Match(Action<T> someAction, Action noneAction)
                    => noneAction?.Invoke();
            }
        }
    }
}