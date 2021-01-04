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
#### Map
```Map<U>``` is a function for mapping an ```Option<T>``` to an ```Option<U>``` by invoking a passed function. Use this to get a possible option of a different type.
```
var o = Option<int>.Some(10);
o.Map<string>(o => o.ToString()); // returns Some<string> with underlying value "10"

var o = Option<int>.None;
o.Map<string>(o => o.ToString()); // returns None<string>
```

#### Match
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

The second version simply invokes a differing action based on whether the current is ```Some``` or ```None```. This can be used for I/O operations or when a side effect is desired. This is made to mimic
the ```match``` block in Rust, but obviously has its downsides as you cannot handle many different values
like in Rust. Avoid this unless you MUST have side-effects, if you want to return different values based on
the underlying value, use ```Match<U>``` above.
```
var bystander = 10;
var o = Option<int>.Some(10);
o.Match(
    s => bystander += s,
    () => { }
);
// bystander : 20

var bystander = 10;
var o = Option<int>.None;
o.Match(
    s => bystander += s,
    () => { }
);
// bystander : 10
```