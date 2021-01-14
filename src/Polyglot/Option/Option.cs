// GetHashCode and Equals are defined by Some and None, and are not implemented in the abstract base class.
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Perustaja.Polyglot.Result;

namespace Perustaja.Polyglot.Option
{
    public abstract class Option<T> : IEquatable<Option<T>>, IStructuralEquatable
    {
        public static Option<T> Some([NotNullAttribute] T val) => new Choices.Some(val);

        public static Option<T> None => new Choices.None();

        public abstract bool IsSome();

        public abstract bool IsNone();

        /// <summary>
        /// Unwraps the current Option, returning the current value or throwing an exception if None.
        /// </summary>
        /// <returns>The underlying value if Some.</returns>
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
        /// Finalizes the Option, invoking the function argument if Some. The fallback value must be eagerly evaluated, 
        /// use MapOrElse if fallback is the result of a lazily evaluated function.
        /// </summary>
        /// <returns>Either the default value passed if None, or the result of the function call on the underlying value if Some.</returns>
        public U MapOr<U>(U fallback, Func<T, U> func)
            => MapOrElse<U>(
                () => fallback,
                s => func(s)
            );

        /// <summary>
        /// Finalizes the Option, invoking the passed function argument if Some, else lazily invokes the fallback function if None.
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
        /// Filters the Option, turning it into None if the underlying value does not satisfy predicate.
        /// </summary>
        /// <returns>Some with same underlying value if predicate returns true, else None.</returns>
        public Option<T> Filter(Func<T, bool> predicate)
            => MapOrElse(
                () => Option<T>.None,
                s => predicate(s) ? Option<T>.Some(s) : Option<T>.None
            );

        /// <summary>
        /// Transforms the current Option into a Result, mapping the underlying value or using the error
        /// value provided.
        /// </summary>
        /// <returns>Ok with same underlying value if Some, or Err with passed value if None.</returns>
        public Result<T, E> OkOr<E>(E error)
            => MapOrElse<Result<T, E>>(
                () => Result<T, E>.Err(error),
                s => Result<T, E>.Ok(s)
            );

        /// <summary>
        /// Transforms the current Option into a Result, mapping the underlying value or lazily evaluating
        /// the error function passed.
        /// </summary>
        /// <returns>Ok with same underlying value if Some, or Err with the result of the passed func if None.</returns>
        public Result<T, E> OkOrElse<E>(Func<E> error)
            => MapOrElse<Result<T, E>>(
                () => Result<T, E>.Err(error.Invoke()),
                s => Result<T, E>.Ok(s)
            );

        /// <summary>
        /// Compares the current Option with the passed one. Returns this if Some, or the other if None.
        /// Note that this does not guarantee the other Option is Some.
        /// </summary>
        /// <returns>The current Option if Some, or the other Option if None.</returns>
        public Option<T> Or(Option<T> other)
            => MapOrElse<Option<T>>(
                () => other,
                s => this
            );

        /// <summary>
        /// Compares the current Option with the passed one. Returns this if Some, or the result of the 
        /// lazily evaluated function otherFunc if None. Note that this does not guarantee the other 
        /// Option is Some.
        /// </summary>
        /// <returns>The current Option if Some, or the Option result of the otherFunc if None.</returns>
        public Option<T> OrElse(Func<Option<T>> otherFunc)
            => MapOrElse<Option<T>>(
                () => otherFunc.Invoke(),
                s => this
            );

        /// <summary>
        /// Invokes someAction if type is Some, or noneAction if type is None. This function returns void and is not for a functional approach,
        /// but rather as a very simple substitute for a match block.
        /// </summary>
        public abstract void Match(Action<T> someAction, Action noneAction);

        #region OperatorsAndEquation
        public bool Equals(Option<T> other) => Equals(other as object);

        public abstract bool Equals(object other, IEqualityComparer comparer);

        public static bool operator ==(Option<T> x, Option<T> y) => x.Equals(y);

        public static bool operator !=(Option<T> x, Option<T> y) => !(x == y);

        public abstract int GetHashCode(IEqualityComparer comparer);
        #endregion

        private static class Choices
        {
            #region SomeRegion
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

                #region OperatorsAndEquation
                public override bool Equals(object obj)
                    => obj is Some s
                        ? Equals(_value, s._value)
                        : false;

                public override int GetHashCode()
                    => "Some".GetHashCode() ^ _value.GetHashCode();

                public override bool Equals(object other, IEqualityComparer comparer)
                    => other is Some s
                        ? comparer.Equals(_value, s._value)
                        : false;

                public override int GetHashCode(IEqualityComparer comparer)
                    => "Some".GetHashCode() ^ comparer.GetHashCode(_value);
                #endregion
            }
            #endregion

            #region NoneRegion
            public sealed class None : Option<T>
            {
                public override bool IsSome() => false;

                public override bool IsNone() => true;

                public override T Unwrap() => throw new Exception("Attempted to unwrap None.");

                public override T UnwrapOr(T fallback) => fallback;

                public override T UnwrapOrDefault() => default(T);

                public override T UnwrapOrElse(Func<T> fallbackFunc) => fallbackFunc();

                public override U MapOrElse<U>(Func<U> fallbackFunc, Func<T, U> someFunc)
                    => fallbackFunc();

                public override void Match(Action<T> someAction, Action noneAction)
                    => noneAction?.Invoke();

                #region OperatorsAndEquation
                public override bool Equals(object obj) => obj is None;

                public override int GetHashCode() => "None".GetHashCode();

                public override bool Equals(object other, IEqualityComparer comparer)
                    => Equals(other);

                public override int GetHashCode(IEqualityComparer comparer) => GetHashCode();
                #endregion
            }
            #endregion
        }
    }
}