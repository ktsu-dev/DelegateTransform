# ktsu.DelegateTransform

> A utility library for transforming values using delegates in C#.

[![License](https://img.shields.io/github/license/ktsu-dev/DelegateTransform)](https://github.com/ktsu-dev/DelegateTransform/blob/main/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/v/ktsu.DelegateTransform.svg)](https://www.nuget.org/packages/ktsu.DelegateTransform/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ktsu.DelegateTransform.svg)](https://www.nuget.org/packages/ktsu.DelegateTransform/)
[![Build Status](https://github.com/ktsu-dev/DelegateTransform/workflows/build/badge.svg)](https://github.com/ktsu-dev/DelegateTransform/actions)
[![GitHub Stars](https://img.shields.io/github/stars/ktsu-dev/DelegateTransform?style=social)](https://github.com/ktsu-dev/DelegateTransform/stargazers)

## Introduction

DelegateTransform is a utility library that provides methods for transforming values using various delegate types in C#. It offers a clean and efficient way to apply transformations using `ActionRef`, `Func`, and `FuncRef` delegates, making your code more readable and functional.

## Features

- **ActionRef Transformations**: Modify values by reference using action delegates
- **Func Transformations**: Transform values using function delegates
- **FuncRef Transformations**: Transform values by reference using function delegates
- **Fluent API**: Chain multiple transformations together
- **Generic Implementation**: Works with any value or reference type

## Installation

### Package Manager Console

```powershell
Install-Package ktsu.DelegateTransform
```

### .NET CLI

```bash
dotnet add package ktsu.DelegateTransform
```

### Package Reference

```xml
<PackageReference Include="ktsu.DelegateTransform" Version="x.y.z" />
```

## Usage Examples

### With ActionRef

The `With` method can be used with an `ActionRef` delegate to modify the input value by reference:

```csharp
using ktsu.DelegateTransform;

int input = 5;
DelegateTransform.With(input, (ref int x) => x *= 2);
// input is now 10
```

### With Func

The `With` method can be used with a `Func` delegate to transform the input value:

```csharp
using ktsu.DelegateTransform;

int input = 5;
int result = DelegateTransform.With(input, (int x) => x * 2);
// result is 10
```

### With FuncRef

The `With` method can be used with a `FuncRef` delegate to transform the input value by reference:

```csharp
using ktsu.DelegateTransform;

int input = 5;
DelegateTransform.With(input, (ref int x) => x *= 2);
// input is now 10
```

### Chaining Transformations

You can chain multiple transformations for more complex operations:

```csharp
using ktsu.DelegateTransform;

// Starting value
string input = "example";

// Chain of transformations
var result = DelegateTransform
    .With(input, s => s.ToUpper())                      // EXAMPLE
    .With(s => s.Replace("EX", "**"))                   // **AMPLE
    .With(s => s + " transformed");                     // **AMPLE transformed

Console.WriteLine(result); // Outputs: **AMPLE transformed
```

### Complex Object Transformations

```csharp
using ktsu.DelegateTransform;

var person = new Person { Name = "John", Age = 30 };

// Transform using ActionRef
DelegateTransform.With(person, (ref Person p) => {
    p.Name = p.Name.ToUpper();
    p.Age += 1;
});

Console.WriteLine($"{person.Name}, {person.Age}"); // Outputs: JOHN, 31

// Transform using Func
var description = DelegateTransform.With(person, p => 
    $"{p.Name} is {p.Age} years old");

Console.WriteLine(description); // Outputs: JOHN is 31 years old
```

## API Reference

### `DelegateTransform` Static Class

The main class providing transformation methods.

#### Methods

| Name | Parameters | Return Type | Description |
|------|------------|-------------|-------------|
| `With<T>` | `T value, Action<ref T> transform` | `T` | Transforms a value by reference using an action delegate |
| `With<T, TResult>` | `T value, Func<T, TResult> transform` | `TResult` | Transforms a value using a function delegate |
| `With<T>` | `T value, Func<ref T, T> transform` | `T` | Transforms a value by reference using a function delegate |
| `With<T>` | `ref T value, Action<ref T> transform` | `void` | Transforms a reference to a value using an action delegate |

## Advanced Usage

### Performance Considerations

Using reference-based transformations (`ActionRef`, `FuncRef`) can be more performant for large structs as they avoid unnecessary copying:

```csharp
// For large structs, using ref can be more efficient
var largeStruct = new LargeStruct(1000000);

// This avoids copying the struct
DelegateTransform.With(ref largeStruct, (ref LargeStruct s) => {
    s.Process();
});
```

### Custom Delegate Types

You can easily extend DelegateTransform with your own delegate types:

```csharp
// Define a custom delegate type
public delegate void MyCustomDelegate<T>(ref T value, string parameter);

// Extension method for DelegateTransform
public static class DelegateTransformExtensions
{
    public static T WithCustom<T>(this T value, MyCustomDelegate<T> transform, string parameter)
    {
        T result = value;
        transform(ref result, parameter);
        return result;
    }
}

// Usage
var result = 5.WithCustom((ref int x, string p) => {
    x *= int.Parse(p);
}, "3");
// result is 15
```

## Contributing

Contributions are welcome! Here's how you can help:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

Please make sure to update tests as appropriate and adhere to the existing coding style.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
