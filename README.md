# Perustaja.Polyglot
Classes and Types that extend C#, designed to work similarly to constructs in other languages.

Current implementations:
```Option<T>``` - Based upon Rust.
```Result<T, E>``` - based upon Rust.

References:
```Option<T>``` Is based upon code written by JohnAzariah's Maybe monad [here](https://gist.github.com/johnazariah/d95c03e2c56579c11272a647bab4bc38).
# Option
Inspired by Rust's ```Option``` monad which is very similar to ```Maybe``` in F# and Haskell. 
The ```Option``` monad represents a discriminated union type of either ```Some``` or ```None```.

#### Instantiation
```
var something = Option<string>.Some("Hello");
var nothing = Option<string>.None;
```

#### Checking the underlying value
Two functions are provided to see check if it is ```Some``` or ```None```.
```
var o = Option<string>.Some("Noniin");
Console.WriteLine(o.IsSome()); // Prints true
Console.WriteLine(o.IsNone()); // Prints false
```

## Unwrapping - Returning the underlying value

#### Unwrap : ```Option<T> -> T```
```Unwrap()``` returns the underlying value if the value is ```Some```, or throws an exception if it is ```None```.
```
int some = Option<int>.Some(10).Unwrap(); // Returns 10
int none = Option<int>.None.Unwrap(); // Throws an exception!
```

#### UnwrapOr : ```Option<T> -> T```
Provides a default value, if the default value is the result of a function call, it must be eagerly evaluted (the function must be invoked within the call to```UnwrapOr()```).
```
int some = Option<int>.Some(5).UnwrapOr(10); // Returns 5, the underlying value
int none = Option<int>.None.UnwrapOr(10); // Returns 10, the default value
```

#### UnwrapOrElse : ```Option<T> -> T```
Provides a fallback function to lazily evaluate in a closure if the current is ```None```.
```
int num = 10;
int some = Option<int>.Some(5).UnwrapOrElse(() => num * 5); // Returns 5, the underlying value
int none = Option<int>.None.UnwrapOrElse(() => num * 5); // Returns 50, the result of the default function
```

## Mapping - Performing transformations

#### Map : ```Option<T> -> Option<U>```
```Map()``` is a function for mapping an ```Option<T>``` to an ```Option<U>``` by invoking a passed function. If the current is ```Some```, the function is invoked
with the underlying value, returning a new ```Some``` of a different type. If it is ```None```, the result is still ```None```.
```
var someOpt = Option<int>.Some(10).Map(o => o.ToString()); // Returns Some with underlying value "10"
var none = Option<int>.None.Map(o => o.ToString()); // Returns None
```

#### MapOr : ```Option<T> -> U```
Provides a fallback value to return in case the current is ```None```.
```
string some = Option<int>.Some(10).MapOr("Default", o => o.ToString()); // Returns "10"
string none = Option<int>.None.MapOr("Default", o => o.ToString()); // Returns "Default"
```

#### MapOrElse : ```Option<T> -> U```
Provides a fallback function to lazily evaluate in a closure if the current is ```None```.
```
string greeting = "hello";
string r = Option<int>.None.MapOrElse(
    () => greeting.ToUpper, 
    s => s.ToString()
    );
// r : "HELLO"
```

## Combinators

#### AndThen : ```Option<T> -> Option<U>```
```AndThen()``` allows chaining. Each function in the chain returns an ```Option```, calling the passed function on its underlying value if ```Some```, or returning ```None``` if there isn't one. If any of the calls in the chain return ```None```, the end result is ```None```.
```
// Assume the following function exists
private Option<int> SquareIfEven(int n)
    => n % 2 == 0 
    ? Option<int>.Some(n * n)
    : Option<int>.None;

public void SomeOtherScope()
{
    var o = Option<int>.Some(2);
    var r = o.AndThen(SquareIfEven).AndThen(SquareIfEven).Unwrap(); // Returns 16

    o = Option<int>.Some(3);
    var r = o.AndThen(SquareIfEven).AndThen(SquareIfEven).Unwrap() // Throws an exception
}
```

## Matching - Side-effects or I/O from an Option

#### Match - Actions instead of Funcs
This can be used for I/O operations or when a side effect is desired. This is made to mimic
the ```match``` block in Rust, but obviously has its downsides as you cannot handle many different values
like in Rust. 
```
var o = Option<string>.Some("10");
o.Match(
    s => Console.WriteLine(s),
    () => someFile.WriteLine("Empty!")
);
// Prints "10" to the console

var o = Option<string>.None;
o.Match(
    s => Console.WriteLine(s),
    () => someFile.WriteLine("Empty!")
);
// Prints "Empty!" to some file
```

# Result
Represents either an ```Ok``` with underlying value ```T``` or ```Err``` with underlying value ```E```.
## Instantiation
```
var ok = Result<int, string>.Ok(5);
var err = Result<int, string>.Err("Error!");
```

## Checking the underlying value
```
```

## Unwrapping - Returning the underlying value

## Mapping - Performing transformations