# DelegateTransform

DelegateTransform is a utility library for transforming values using delegates in C#. It provides methods to apply transformations using `ActionRef`, `Func`, and `FuncRef` delegates.

## Installation

To install DelegateTransform, add the following package to your project:

```
dotnet add package DelegateTransform
```

## Usage

### With ActionRef

The `With` method can be used with an `ActionRef` delegate to modify the input value by reference.

```
int input = 5; DelegateTransform.With(input, (ref int x) => x *= 2); // input is now 10
```

### With Func

The `With` method can be used with a `Func` delegate to transform the input value.
```
int input = 5; int result = DelegateTransform.With(input, (int x) => x * 2); // result is 10
```

### With FuncRef

The `With` method can be used with a `FuncRef` delegate to transform the input value by reference.
```
int input = 5; DelegateTransform.With(input, (ref int x) => x *= 2); // input is now 10
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License

DelegateTransform is licensed under the MIT license. See the [LICENSE](LICENSE) file for more information.
