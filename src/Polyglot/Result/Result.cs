using System;
using System.Diagnostics.CodeAnalysis;

namespace Perustaja.Polyglot.Result
{
    public abstract class Result<T, E>
    {
        public static Result<T, E> Ok([NotNullAttribute] T val) => new Choices.Ok(val);

        public static Result<T, E> Err([NotNullAttribute] E val) => new Choices.Err(val);

        public abstract bool IsOk();

        public abstract bool IsErr();

        /// <summary>
        /// Unwraps the current value if Ok, throws exception if Err.
        /// </summary>
        /// <returns>The underlying value if Ok.</returns>
        /// <exception cref="System.Exception">If the current Option is None.</exception>
        public abstract T Unwrap();

        /// <summary>
        /// Unwraps the current value if Err, throws exception if Ok.
        /// </summary>
        /// <returns>The underlying value if Err.</returns>
        /// <exception cref="System.Exception">If the current Option is Ok.</exception>
        public abstract E UnwrapErr();

        /// <summary>
        /// Invokes okAction if type is Ok, or errAction if type is Err.
        /// </summary>
        public abstract void Match(Action<T> okAction, Action<E> errAction);

        public static class Choices
        {
            public sealed class Ok : Result<T, E>
            {
                private T _value { get; }
                public Ok(T val) => _value = val;

                public override bool IsOk() => true;

                public override bool IsErr() => false;

                public override void Match(Action<T> okAction, Action<E> errAction)
                    => okAction?.Invoke(_value);

                public override T Unwrap() => _value;

                public override E UnwrapErr() 
                    => throw new Exception("Attempted to UnwrapErr an Ok Result.");
            }

            public sealed class Err : Result<T, E>
            {
                private E _value { get; }

                public Err(E val) => _value = val;

                public override bool IsOk() => false;

                public override bool IsErr() => true;

                public override void Match(Action<T> okAction, Action<E> errAction)
                    => errAction?.Invoke(_value);

                public override T Unwrap() 
                    => throw new Exception("Attempted to Unwrap an Err Result.");

                public override E UnwrapErr() => _value;
            }
        }
    }
}