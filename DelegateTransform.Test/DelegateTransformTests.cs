// Copyright (c) 2023-2026 ktsu-dev contributors

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace ktsu.DelegateTransform.Test;

[TestClass]
public class DelegateTransformTests
{
	private static void DoubleValue(ref int x) => x *= 2;

	[TestMethod]
	public void WithActionRefModifiesInput()
	{
		int input = 5;
		int expected = 10;

		int result = DelegateTransform.With(input, DoubleValue);

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void WithActionRefThrowsArgumentNullException()
	{
		int input = 5;
		ActionRef<int> nullDelegate = null!;

		ArgumentNullException ex = Assert.ThrowsExactly<ArgumentNullException>(() => input = DelegateTransform.With(input, nullDelegate));
		Assert.AreEqual("delegate", ex.ParamName);
	}

	[TestMethod]
	public void WithFuncModifiesInput()
	{
		int input = 5;
		int expected = 15;

		int result = DelegateTransform.With(input, x => x + 10);

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void WithFuncThrowsArgumentNullException()
	{
		int input = 5;

		ArgumentNullException ex = Assert.ThrowsExactly<ArgumentNullException>(() => input = DelegateTransform.With(input, (Func<int, int>)null!));
		Assert.AreEqual("delegate", ex.ParamName);
	}

	[TestMethod]
	public void WithActionRefKeepsExpressionBodiedLambdaChange()
	{
		int input = 5;

		// An expression body has type int, so this lambda used to bind to an overload that returned x++'s
		// value (the old 5) and threw the change away. It must reach the ActionRef overload (#79).
		int result = input.With((ref x) => x++);

		Assert.AreEqual(6, result);
		Assert.AreEqual(5, input);
	}

	[TestMethod]
	public void WithActionRefKeepsExpressionBodiedCallChange()
	{
		int input = 5;

		int result = input.With((ref x) => Interlocked.Exchange(ref x, 7));

		Assert.AreEqual(7, result);
		Assert.AreEqual(5, input);
	}
}
