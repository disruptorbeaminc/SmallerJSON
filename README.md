# SmallerJSON
A memory-optimized mostly-drop-in replacement for MiniJSON

Like MiniJSON, it is a simple recursive descent JSON parser that deserializes
to `IDictionary<string, object>`, but it has some optimizations to reduce its
heap allocations. In tests, parsing with it allocated ~1/5 the memory and took
~1/4 the time as with MiniJSON.

Parsing optimization include:
* Parse directly from a UTF-8 byte array rather than a string to avoid
  intermediate allocations
* Reuse repeated string keys using a cache of the last 20 keys used
* Alternate IDictionary implementation that has reduce memory overhead, but
  potentially slower lookup (contollable with #define)
* Unescape strings in place
* Fast-path allocations for ASCII-only strings
* Reduce un-needed allocations:
  * Don't allocate string when parsing integers, booleans, or `null`
  * Reuse string builder when parsing string values
  * Don't read input through StringReader
  * ...


## Installation
You can copy the files, or add this repo as a dependency in the Unity package
manager. There are no external dependencies (not even Unity), so it is pretty
simple.

## Caveats
* Writing JSON to a string is relatively unoptimized. This was a later addition
  mostly to allow the removal of another copy of MiniJSON just for
  serialization, and is pretty close to the original MiniJSON. (although it does
  avoid `char[]`` allocations when writing strings)
* The alternative `IDictionary` implementation may not be compatible with code
  that expects a concrete Dictionary. Most code can be adjusted easily, or this
  option can be turned off.
* SmallerJSON used to only parse from byte arrays, and not write to json.
  Parsing from strings and writing to strings were added later, so it is less
  small than it used to be. It is still smaller than most C# JSON parsers.
* While SmallerJSON is optimized relative to MiniJSON, it may not the fastest
  parser out there, since serializing to a dictionary means it is always going
  to be allocating a fair amount. There are still a lot of slower parsers out
  there.
* Parsing from a byte array may mutate the input buffer

## License
MIT