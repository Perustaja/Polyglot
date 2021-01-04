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
```Map<U>``` is a function for mapping an ```Option<T>``` to an ```Option<U>``` by invoking a passed function.
```
var o = Option<int>.Some(10);
o.Map<string>(o => o.ToString()).Unwrap(); // returns "10"

var o = Option<int>.None;
o.Map<string>(o => o.ToString()).Unwrap(); // returns None<string>
```

#### Match
Two functions are provided, one, which returns a value and one which does not.
```Match<R>``` invokes one of two functions that return R, each depending on whether the current is ```Some``` or ```None```.
```
int value = 10;
var o = Option<int>.Some(value);
var r = o.Match<string>(
    s => s.ToString(),
    () => String.Empty
);
// r : "10"

var o = Option<int>.None;
var r = o.Match<string>(
    s => s.ToString(),
    () => String.Empty
);
// r : ""
```

The second version simply invokes a differing action based on whether the current is ```Some``` or ```None```. This can be used for
I/O operations or whether a side effect is desired. Ideally, this is for returning different values and is meant to serve as a match block in Rust does,
albeit without handling different value cases.
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