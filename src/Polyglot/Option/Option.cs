using System;

namespace Perustaja.Polyglot.Option
{
    public abstract class Option<T>
    {
        public static Option<T> Some(T val) => new Choices.Some(val);

        public static Option<T> None => new Choices.None();

        public abstract bool IsSome();

        public abstract bool IsNone();

        /// <summary>
        /// Returns the underlying value if Some, or throws an exception if None. Prefer to use UnwrapOr for safely finalizing.
        /// </summary>
        /// <exception cref="System.Exception">If the current Option is None.</exception>
        public abstract T Unwrap();

        /// <summary>
        /// Safely finalizes the Option. The fallback is eagerly evaluated, if you want lazy evaluation use UnwrapOrElse.
        /// </summary>
        /// <returns>The underlying value if Some, or the default value passed if None.</returns>
        public abstract T UnwrapOr(T fallback);

        /// <returns>The underlying value if Some, or the C# default of the type.</returns>
        public abstract T UnwrapOrDefault();

        /// <summary>
        /// Safely finalizes the Option. Lazily evaluates the result of the function argument.
        /// </summary>
        /// <returns>The underlying value if Some, or the result of the invoked function if None.</returns>
        public abstract T UnwrapOrElse(Func<T> fallbackFunc);

        /// <summary>
        /// Maps the Option to one of a new type, applying the function argument if Some.
        /// </summary>
        /// <returns>An Option containing the result of the function call if Some, or None if None.</returns>
        public Option<U> Map<U>(Func<T, U> func)
            => MapOrElse(
                () => Option<U>.None,
                s => Option<U>.Some(func(s))
            );

        /// <summary>
        /// Finalizes the Option, invoking the function argument if Some. Use MapOrElse if you want the default value to be the result of a function call.
        /// </summary>
        /// <returns>Either the default value passed if None, or the result of the function call on the underlying value if Some.</returns>
        public U MapOr<U>(U fallback, Func<T, U> func)
            => MapOrElse<U>(
                () => fallback,
                s => func(s)
            );

        /// <summary>
        /// Finalizes the Option, invoking the passed function argument if Some, else invokes the fallback function if None.
        /// </summary>
        /// <returns>The result of the fallback function if None, or the result of the normal function call on the underlying value if Some.</returns>
        public abstract U MapOrElse<U>(Func<U> fallbackFunc, Func<T, U> someFunc);

        /// <summary>
        /// Invokes the passed function on the underlying value if Some.
        /// </summary>
        /// <returns>Some with its underlying value as the result of the previous value after the function is called on it, or None if previously None.</returns>
        public Option<U> AndThen<U>(Func<T, Option<U>> func)
            => MapOrElse<Option<U>>(
                () => Option<U>.None,
                s => func(s)
            );

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

                public override T UnwrapOrDefault() => _value;

                public override T UnwrapOrElse(Func<T> fallbackFunc) => _value;

                public override U MapOrElse<U>(Func<U> fallbackFunc, Func<T, U> someFunc) => someFunc(_value);

                public override void Match(Action<T> someAction, Action noneAction) => someAction?.Invoke(_value);

            }

            public sealed class None : Option<T>
            {
                public override bool IsSome() => false;

                public override bool IsNone() => true;

                public override T Unwrap() => throw new Exception("Attempted to unwrap None.");

                public override T UnwrapOr(T fallback) => fallback;

                public override T UnwrapOrDefault() => default(T);

                public override T UnwrapOrElse(Func<T> fallbackFunc) => fallbackFunc();

                public override U MapOrElse<U>(Func<U> fallbackFunc, Func<T, U> someFunc) => fallbackFunc();

                public override void Match(Action<T> someAction, Action noneAction) => noneAction?.Invoke();
            }
        }
    }
}