using System;

namespace Perustaja.Polyglot.Result
{
    public abstract class Result<T, E>
    {
        public static Result<T, E> Ok(T val) => new Choices.Ok(val);

        public static Result<T, E> Err(E val) => new Choices.Err(val);

        public abstract bool IsOk();

        public abstract bool IsErr();

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

            }

            public sealed class Err : Result<T, E>
            {
                private E _value { get; }

                public Err(E val) => _value = val;

                public override bool IsOk() => false;

                public override bool IsErr() => true;

                public override void Match(Action<T> okAction, Action<E> errAction)
                    => errAction?.Invoke(_value);
            }
        }
    }
}