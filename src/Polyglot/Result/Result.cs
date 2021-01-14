// GetHashCode and Equals are defined by Ok and Err, and are not implemented in the abstract base class.
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()using System;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Perustaja.Polyglot.Result
{
    public abstract class Result<T, E> : IEquatable<Result<T, E>>, IStructuralEquatable
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
        /// Maps the Result to one of a new type, applying the function argument to T if Ok.
        /// </summary>
        /// <returns>An Ok Result containing the return value of the applied function, or an untouched Err.</returns>
        public abstract Result<U, E> Map<U>(Func<T, U> okFunc);

        /// <summary>
        /// Finalizes the Result, returning the fallback if Err or a function result if Ok.
        /// </summary>
        /// <returns>Either the default value passed if Err, or the result of the function call on the underlying value if Ok.</returns>
        public U MapOr<U>(U fallback, Func<T, U> okFunc)
            => MapOrElse<U>(
                () => fallback,
                ok => okFunc(ok)
            );

        /// <summary>
        /// Finalizes the Result, lazily evaluating the fallback function if Err or a function result if Ok.
        /// </summary>
        /// <returns>Result of the lazily evaluated fallback func if Err, or the result of the function call on the underlying value if Ok.</returns>
        public abstract U MapOrElse<U>(Func<U> fallbackFunc, Func<T, U> okFunc);

        /// <summary>
        /// Maps the Result to one of a new type, applying the function argument to E if Err.
        /// </summary>
        /// <returns>An Err Result containing the return value of the applied function, or an untouched Ok.</returns>
        public abstract Result<T, F> MapErr<F>(Func<E, F> errFunc);

        /// <summary>
        /// Compares the current Result with the passed one. Returns this if Ok, or the other if Err.
        /// Note that this does not guarantee the other Result is Ok.
        /// </summary>
        /// <returns>The current Result if Ok, or the other Result if Err.</returns>
        public abstract Result<T, E> Or(Result<T, E> other);

        /// <summary>
        /// Invokes the passed function on the underlying value if Ok, or leaves the result an untouched Err.
        /// </summary>
        /// <returns>Ok with its underlying value as the result of the previous value after the function is called on it, or Err if previously Err.</returns>
        public abstract Result<U, E> AndThen<U>(Func<Result<T, E>, Result<U, E>> okFunc);

        /// <summary>
        /// Invokes okAction if type is Ok, or errAction if type is Err.
        /// </summary>
        public abstract void Match(Action<T> okAction, Action<E> errAction);

        #region OperatorsAndEquationBase
        public bool Equals(Result<T, E> other) => Equals(other as object);

        public abstract bool Equals(object other, IEqualityComparer comparer);

        public static bool operator ==(Result<T, E> x, Result<T, E> y) => x.Equals(y);

        public static bool operator !=(Result<T, E> x, Result<T, E> y) => !(x == y);

        public abstract int GetHashCode(IEqualityComparer comparer);
        #endregion

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

                public override Result<U, E> Map<U>(Func<T, U> okFunc)
                    => Result<U, E>.Ok(okFunc(_value));

                public override U MapOrElse<U>(Func<U> fallbackFunc, Func<T, U> okFunc)
                    => okFunc(_value);

                public override Result<T, F> MapErr<F>(Func<E, F> errFunc)
                    => Result<T, F>.Ok(_value);

                public override Result<U, E> AndThen<U>(Func<Result<T, E>, Result<U, E>> okFunc)
                    => okFunc(this);

                public override Result<T, E> Or(Result<T, E> other)
                    => this;

                #region OperatorsAndEquationOk
                public override bool Equals(object obj)
                    => obj is Ok ok
                        ? Equals(_value, ok._value)
                        : false;
                public override int GetHashCode()
                    => "Ok".GetHashCode() ^ _value.GetHashCode();

                public override bool Equals(object other, IEqualityComparer comparer)
                    => other is Ok ok
                        ? comparer.Equals(_value, ok._value)
                        : false;

                public override int GetHashCode(IEqualityComparer comparer)
                    => "Ok".GetHashCode() ^ comparer.GetHashCode(_value);
                #endregion
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

                public override Result<U, E> Map<U>(Func<T, U> okFunc)
                    => Result<U, E>.Err(_value);

                public override U MapOrElse<U>(Func<U> fallbackFunc, Func<T, U> okFunc)
                    => fallbackFunc.Invoke();

                public override Result<T, F> MapErr<F>(Func<E, F> errFunc)
                    => Result<T, F>.Err(errFunc(_value));

                public override Result<U, E> AndThen<U>(Func<Result<T, E>, Result<U, E>> okFunc)
                    => Result<U, E>.Err(_value);

                public override Result<T, E> Or(Result<T, E> other)
                    => other;

                #region OperatorsAndEquationErr
                public override bool Equals(object obj)
                    => obj is Err err
                        ? Equals(_value, err._value)
                        : false;

                public override int GetHashCode()
                    => "Err".GetHashCode() ^ _value.GetHashCode();

                public override bool Equals(object other, IEqualityComparer comparer)
                    => other is Err err
                        ? comparer.Equals(_value, err._value)
                        : false;

                public override int GetHashCode(IEqualityComparer comparer)
                    => "Err".GetHashCode() ^ comparer.GetHashCode(_value);
                #endregion
            }
        }
    }
}