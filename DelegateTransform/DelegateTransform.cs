// Copyright (c) 2023-2026 ktsu-dev contributors

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
/// Provides extension methods for applying delegates to input items.
/// </summary>
/// <remarks>
/// Call these as extension methods, for example <c>input.With(x =&gt; x * 2)</c>. From code in another
/// <c>ktsu.*</c> namespace the simple name <c>DelegateTransform</c> resolves to the namespace rather than
/// this class, so the extension form is the one that works everywhere.
/// </remarks>
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
	public static T With<T>(this T input, ActionRef<T> @delegate)
	{
		Ensure.NotNull(@delegate);

		T output = input;
		@delegate(ref output);
		return output;
	}

	/// <summary>
	/// Applies a <see cref="FuncRef{T}"/> delegate to the input item and returns the result.
	/// </summary>
	/// <typeparam name="T">The type of the input item.</typeparam>
	/// <param name="input">The input item.</param>
	/// <param name="delegate">The delegate to apply to the input item.</param>
	/// <returns>The result of the delegate.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the delegate is null.</exception>
	public static T With<T>(this T input, FuncRef<T> @delegate)
	{
		Ensure.NotNull(@delegate);

		return @delegate(ref input);
	}

	/// <summary>
	/// Applies a <see cref="Func{T, TResult}"/> delegate to the input item and returns the result.
	/// </summary>
	/// <typeparam name="T">The type of the input item.</typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <param name="input">The input item.</param>
	/// <param name="delegate">The delegate to apply to the input item.</param>
	/// <returns>The result of the delegate, which may be of a different type from the input.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the delegate is null.</exception>
	public static TResult With<T, TResult>(this T input, Func<T, TResult> @delegate)
	{
		Ensure.NotNull(@delegate);

		return @delegate(input);
	}
}
