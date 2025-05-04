// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.DelegateTransform;

/// <summary>
/// Represents a delegate that performs an action on a reference to an item of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the item.</typeparam>
/// <param name="item">The item to perform the action on.</param>
public delegate void ActionRef<T>(ref T item);

/// <summary>
/// Represents a delegate that performs a function on a reference to an item of type <typeparamref name="T"/> and returns a value of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the item.</typeparam>
/// <param name="item">The item to perform the function on.</param>
/// <returns>The result of the function.</returns>
public delegate T FuncRef<T>(ref T item);

/// <summary>
/// Provides methods for applying delegates to input items.
/// </summary>
public static class DelegateTransform
{
	/// <summary>
	/// Applies an <see cref="ActionRef{T}"/> delegate to the input item and returns the modified item.
	/// </summary>
	/// <typeparam name="T">The type of the input item.</typeparam>
	/// <param name="input">The input item.</param>
	/// <param name="delegate">The delegate to apply to the input item.</param>
	/// <returns>The modified item.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the delegate is null.</exception>
	public static T With<T>(T input, ActionRef<T> @delegate)
	{
		ArgumentNullException.ThrowIfNull(@delegate);

		var output = input;
		@delegate(ref output);
		return output;
	}

	/// <summary>
	/// Applies a <see cref="Func{T, TResult}"/> delegate to the input item and returns the result.
	/// </summary>
	/// <typeparam name="T">The type of the input item.</typeparam>
	/// <param name="input">The input item.</param>
	/// <param name="delegate">The delegate to apply to the input item.</param>
	/// <returns>The result of the delegate.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the delegate is null.</exception>
	public static T With<T>(T input, Func<T, T> @delegate)
	{
		ArgumentNullException.ThrowIfNull(@delegate);

		return @delegate(input);
	}

	/// <summary>
	/// Applies a <see cref="FuncRef{T}"/> delegate to the input item and returns the result.
	/// </summary>
	/// <typeparam name="T">The type of the input item.</typeparam>
	/// <param name="input">The input item.</param>
	/// <param name="delegate">The delegate to apply to the input item.</param>
	/// <returns>The result of the delegate.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the delegate is null.</exception>
	public static T With<T>(T input, FuncRef<T> @delegate)
	{
		ArgumentNullException.ThrowIfNull(@delegate);

		return @delegate(ref input);
	}
}
