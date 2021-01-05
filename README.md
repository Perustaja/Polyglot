# Perustaja.Polyglot
Classes and Types that extend C#, designed to work similarly to constructs in other languages.
# Option
Inspired by Rust's ```Option``` monad which is very similar to ```Maybe``` in F# and Haskell. 
The ```Option``` monad represents a discriminated union type of either ```Some``` or ```None```.
#### Instantiation
```
var something = Option<string>.Some("Hello");
var nothing = Option<string>.None;
```
#### Checking the underlying value
Two functions are provided to see check if it is ```Some``` or ```None```. Using these can result in messy code but nonetheless is provided as it is in Rust.
```
var o = Option<string>.Some("Noniin");
Console.WriteLine(o.IsSome()); // Prints true
Console.WriteLine(o.IsNone()); // Prints false
```
#### UnWrap - Returning the underlying value
```Unwrap()``` returns the underlying value if the value is ```Some```, or throws an exception if it is ```None```. As in Rust, its use is discouraged 
but it can come in handy.
```
var o = Option<int>.Some(10);
o.Unwrap(); // Returns 10

var o = Option<int>.None;
o.Unwrap(); // Throws an exception!
```
Consider using ```UnwrapOr()``` which provides a default value 
```
var o Option<int>.None;
o.UnwrapOr(10); // Returns 10, the default value
```
#### Map - Transforming the underlying type of the Option
```Map<U>``` is a function for mapping an ```Option<T>``` to an ```Option<U>``` by invoking a passed function. Use this to get a possible option of a different type.
```
var o = Option<int>.Some(10);
o.Map<string>(o => o.ToString()); // Returns Some<string> with underlying value "10"

var o = Option<int>.None;
o.Map<string>(o => o.ToString()); // Returns None<string>
```

#### Match - Retrieving the underlying value
Two functions are provided, one which returns a value and one which does not.
```Match<U>``` invokes one of two functions that return U. Based on whether the current ```Option``` is
```Some``` or ```None```, the associated function will be invoked and its value returned. Use this to
safely finalize a value returned by the ```Option```.
```
int value = 10;
var o = Option<int>.Some(value);
string r = o.Match<string>(
    s => s.ToString(),
    () => String.Empty
);
// r : "10"

var o = Option<int>.None;
string r = o.Match<string>(
    s => s.ToString(),
    () => String.Empty
);
// r : ""
```

The second version simply invokes a differing Action based on whether the current is ```Some``` or ```None```. This can be used for I/O operations or when a side effect is desired. This is made to mimic
the ```match``` block in Rust, but obviously has its downsides as you cannot handle many different values
like in Rust. Avoid this unless you MUST have side-effects, if you want to return different values based on
the underlying value, use ```Match<U>``` above. 
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

#### AndThen - Combinator function
```AndThen()``` allows chaining. Each function in the chain returns an ```Option<T>``` where ```T``` is the same type as the original. This allows custom functions
which can make decisions on what kind of ```Option``` they want to return based on the current ```Option```. In other words, you can chain together potentially failing
logic and go about your business with the end result.
```
// Assume the following function exists
private Option<int> SquareIfEven(int n)
    => n % 2 == 0 
    ? Option<int>.Some(n * n)
    : Option<int>.None;

public void SomeOtherScope()
{
    var o = Option<int>.Some(2);
    var r = o.AndThen(SquareIfEven).AndThen(SquareIfEven);
    r.Unwrap(); // Returns 16

    o = Option<int>.Some(3);
    var r = o.AndThen(SquareIfEven).AndThen(SquareIfEven);
    r.Unwrap() // Throws an exception, Option is still None
}
If any combinator in the chain fails, the end result ends as None.
```