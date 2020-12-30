using System;

namespace Perustaja.Polyglot.Option
{
    public abstract class Option<T>
    {
        public static Option<T> Some(T val) => new Choices.Some(val);

        public static Option<T> None() => new Choices.None();

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
        /// Invokes someAction if type is Some, or noneAction if type is None.
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

                public override void Match(Action<T> someAction, Action noneAction)
                    => someAction?.Invoke(_value);

            }

            public sealed class None : Option<T>
            {

                public override bool IsSome() => false;

                public override bool IsNone() => true;

                public override T Unwrap() => throw new Exception("Attempted to unwrap None.");

                public override T UnwrapOr(T fallback) => fallback;

                public override void Match(Action<T> someAction, Action noneAction)
                    => noneAction?.Invoke();
            }
        }
    }
}