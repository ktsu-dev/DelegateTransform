# ktsu.DelegateTransform

> A utility library for transforming values using delegates in C#.

[![License](https://img.shields.io/github/license/ktsu-dev/DelegateTransform.svg?label=License&logo=nuget)](LICENSE.md)
[![NuGet Version](https://img.shields.io/nuget/v/ktsu.DelegateTransform?label=Stable&logo=nuget)](https://nuget.org/packages/ktsu.DelegateTransform)
[![NuGet Version](https://img.shields.io/nuget/vpre/ktsu.DelegateTransform?label=Latest&logo=nuget)](https://nuget.org/packages/ktsu.DelegateTransform)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ktsu.DelegateTransform?label=Downloads&logo=nuget)](https://nuget.org/packages/ktsu.DelegateTransform)
[![GitHub commit activity](https://img.shields.io/github/commit-activity/m/ktsu-dev/DelegateTransform?label=Commits&logo=github)](https://github.com/ktsu-dev/DelegateTransform/commits/main)
[![GitHub contributors](https://img.shields.io/github/contributors/ktsu-dev/DelegateTransform?label=Contributors&logo=github)](https://github.com/ktsu-dev/DelegateTransform/graphs/contributors)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/ktsu-dev/DelegateTransform/dotnet.yml?branch=main&label=Build&logo=github)](https://github.com/ktsu-dev/DelegateTransform/actions)

## Introduction

DelegateTransform is a utility library that provides methods for transforming values using various delegate types in C#. It offers a clean and efficient way to apply transformations using `ActionRef`, `Func`, and `FuncRef` delegates, making your code more readable and functional.

**Important**: All `With` methods return a new transformed value - they never mutate the original input. This provides functional programming semantics even when using reference-based delegates.

## Features

- **ActionRef Transformations**: Modify copies of values by reference using action delegates
- **Func Transformations**: Transform values using function delegates
- **FuncRef Transformations**: Transform values by reference using function delegates that return values
- **Copy-on-Transform**: Original values are never mutated
- **Generic Implementation**: Works with any value or reference type

## Supported Frameworks

- .NET 10.0, 9.0, 8.0, 7.0, 6.0
- .NET Standard 2.1

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

The `With` method can be used with an `ActionRef<T>` delegate to apply a transformation by reference and return the result:

```csharp
using ktsu.DelegateTransform;

int input = 5;
int result = input.With((ref int x) => x *= 2);
// result is 10, input is still 5 (original unchanged)
```

### With Func

The `With` method can be used with a `Func<T, TResult>` delegate to transform the input value. The result can be the same type as the input or a different one:

```csharp
using ktsu.DelegateTransform;

int input = 5;
int result = input.With(x => x * 2);
// result is 10
```

### With FuncRef

The `With` method can be used with a `FuncRef<T>` delegate to transform the input value by reference:

```csharp
using ktsu.DelegateTransform;

int input = 5;
int result = input.With((ref int x) => x * x);
// result is 25, input is still 5 (original unchanged)
```

### Chaining Transformations

Every `With` method is an extension method that returns the transformed value, so calls chain fluently:

```csharp
using ktsu.DelegateTransform;

string result = "example"
    .With(s => s.ToUpper())            // "EXAMPLE"
    .With(s => s.Replace("EX", "**"))  // "**AMPLE"
    .With(s => s + " transformed");    // "**AMPLE transformed"

Console.WriteLine(result); // Outputs: **AMPLE transformed
```

### Complex Object Transformations

```csharp
using ktsu.DelegateTransform;

var person = new Person { Name = "John", Age = 30 };

// Transform using ActionRef (for reference types, the object is modified)
Person updatedPerson = person.With((ref Person p) => {
    p.Name = p.Name.ToUpper();
    p.Age += 1;
});

Console.WriteLine($"{updatedPerson.Name}, {updatedPerson.Age}"); // Outputs: JOHN, 31

// Transform using Func to create a string description
string description = person.With(p => $"{p.Name} is {p.Age} years old");

Console.WriteLine(description); // Outputs: JOHN is 31 years old
```

## API Reference

### Delegate Types

```csharp
// Custom delegate for actions on references
public delegate void ActionRef<T>(ref T item);

// Custom delegate for functions on references
public delegate T FuncRef<T>(ref T item);
```

### `DelegateTransform` Static Class

The static class holding the `With` extension methods. Call them on the value, as in `input.With(...)`.
Naming the class, as in `DelegateTransform.With(input, ...)`, does not compile from other `ktsu.*`
namespaces, where `DelegateTransform` resolves to the namespace of the same name.

#### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `With<T>` | `this T input, ActionRef<T> delegate` | `T` | Creates a copy, applies the action by reference, returns the modified copy |
| `With<T, TResult>` | `this T input, Func<T, TResult> delegate` | `TResult` | Applies a function to the input and returns the result, which may be a different type |
| `With<T>` | `this T input, FuncRef<T> delegate` | `T` | Passes input by reference to the function and returns the result |

All methods throw `ArgumentNullException` if the delegate is null.

## Design Principles

### Copy-on-Transform

The library follows a functional programming pattern where the original input is never mutated:

```csharp
public static T With<T>(this T input, ActionRef<T> @delegate)
{
    Ensure.NotNull(@delegate);

    T output = input;  // Creates a copy
    @delegate(ref output);
    return output;
}
```

This ensures predictable behavior and prevents side effects on the original values.

### Performance Considerations

For value types (structs), a copy is made before transformation. For large structs where you want to avoid copying, consider using the `FuncRef<T>` overload which passes by reference and returns the modified value.

For reference types (classes), the "copy" is actually a copy of the reference, so the underlying object can be modified. If you need true immutability for reference types, create a new instance within your delegate.

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
