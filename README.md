# Perustaja.Polyglot
Classes and Types that extend C#, designed to work similarly to constructs in other languages.
Inspired by johnazariah https://gist.github.com/johnazariah/d95c03e2c56579c11272a647bab4bc38 and his implementation of F#'s Maybe<T>.
# Option
Inspired by Rust's Option.
#### Instantiation and basic functionality
<b>Instantiation</b>
```
    var version = Option<string>.Some("My error");
    var version = Option<string>.None();
```

<b>Match</b>
Similar to Rust's match block (although nowhere near as terse or flexibile).
The first argument is an Action<T> which is executed if the Option is Some.
The second argument is an Action which is executed if the Option is None.
```
    version.Match(
        e => Console.WriteLine(e),
        () => {}
    );
```