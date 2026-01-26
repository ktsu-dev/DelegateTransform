# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**DelegateTransform** is a utility library providing methods for transforming values using various delegate types in C#. It's part of the ktsu.dev ecosystem and offers three distinct transformation patterns:

- **ActionRef<T>** - Modify values by reference using action delegates
- **Func<T, T>** - Transform values using standard function delegates
- **FuncRef<T>** - Transform values by reference using function delegates that return values

## Build and Test Commands

### Building the Project

```bash
# Build the solution
dotnet build

# Build with specific configuration
dotnet build --configuration Release

# Clean build (recommended after SDK changes)
dotnet build --no-incremental
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run a specific test by name
dotnet test --filter "FullyQualifiedName~DelegateTransformTests.WithActionRefModifiesInput"

# Run tests matching a pattern
dotnet test --filter "FullyQualifiedName~WithActionRef"
```

### Creating NuGet Package

```bash
# Pack for release
dotnet pack --configuration Release --output ./staging
```

## Architecture and Key Concepts

### Delegate Types

The library defines two custom delegate types that complement .NET's standard delegates:

```csharp
// Custom delegate for actions on references
public delegate void ActionRef<T>(ref T item);

// Custom delegate for functions on references
public delegate T FuncRef<T>(ref T item);
```

### Transformation Methods

All transformations use the static `DelegateTransform.With<T>()` method with three overloads:

1. **ActionRef overload** - `With<T>(T input, ActionRef<T> delegate)`
   - Creates a copy of the input value
   - Applies the action to the copy by reference
   - Returns the modified copy
   - Original input remains unchanged

2. **Func overload** - `With<T>(T input, Func<T, T> delegate)`
   - Standard functional transformation
   - Takes input, applies function, returns new value
   - Most straightforward pattern

3. **FuncRef overload** - `With<T>(T input, FuncRef<T> delegate)`
   - Passes input by reference to the function
   - Function can modify and return the reference
   - Useful for performance with large structs

### Design Pattern: Copy-on-Transform

Important architectural decision: The ActionRef and FuncRef overloads that take value parameters (not `ref` parameters) intentionally create a copy before transformation:

```csharp
public static T With<T>(T input, ActionRef<T> @delegate)
{
    var output = input;  // Creates a copy
    @delegate(ref output);
    return output;
}
```

This ensures the original value is never mutated, providing functional programming semantics even with reference-based delegates.

### Fluent API Chaining

All `With` methods return the transformed value, enabling method chaining:

```csharp
var result = value
    .With(x => x.Transform1())
    .With(x => x.Transform2())
    .With(x => x.Transform3());
```

However, standard C# extension method syntax doesn't directly support this - users typically chain through repeated calls to `DelegateTransform.With()`.

## Project Structure

```
DelegateTransform/
├── DelegateTransform/              # Main library
│   ├── DelegateTransform.cs        # Single file with all delegate types and methods
│   └── DelegateTransform.csproj    # Uses ktsu.Sdk.Lib
├── DelegateTransform.Test/         # Test project
│   ├── DelegateTransformTests.cs   # MSTest tests for all overloads
│   └── DelegateTransform.Test.csproj  # Uses ktsu.Sdk.Test
├── Directory.Packages.props        # Central Package Management
└── DelegateTransform.sln
```

### Dependencies

The library has minimal dependencies:
- **ktsu.ScopedAction** - Only external dependency
- Part of the ktsu.dev ecosystem using ktsu.Sdk for standardized builds

## Testing Conventions

Tests use **MSTest** framework and follow these patterns:

- Test class: `DelegateTransformTests` with `[TestClass]` attribute
- Test methods: Use `[TestMethod]` attribute
- Naming: `With{DelegateType}{Behavior}` (e.g., `WithActionRefModifiesInput`)
- Validation: Tests both successful transformations and null delegate exceptions

Each overload has two tests:
1. Successful transformation test
2. ArgumentNullException test for null delegate

## Common Development Scenarios

### Adding a New Delegate Type

1. Define the delegate type in [DelegateTransform.cs](DelegateTransform/DelegateTransform.cs)
2. Add corresponding `With<T>()` overload to the `DelegateTransform` class
3. Include null check: `ArgumentNullException.ThrowIfNull(@delegate);`
4. Add tests in [DelegateTransformTests.cs](DelegateTransform.Test/DelegateTransformTests.cs)

### Modifying Transformation Behavior

When modifying the `With` methods, be careful to preserve the copy-on-transform semantics for value parameters. Reference parameters (with `ref` keyword) can modify in-place, but value parameters should never mutate the original.

### Multi-Targeting

This library targets multiple .NET versions through the ktsu.Sdk:
- net9.0, net8.0, net7.0, net6.0, netstandard2.1

Test projects only target net9.0 for faster builds.
