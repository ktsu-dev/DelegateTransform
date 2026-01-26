// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace ktsu.DelegateTransform.Test;

[TestClass]
public class DelegateTransformTests
{
	private static void DoubleValue(ref int x) => x *= 2;

	private static int SquareValue(ref int x) => x * x;

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

		_ = Assert.ThrowsExactly<ArgumentNullException>(() => input = DelegateTransform.With(input, (ActionRef<int>)null!));
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

		_ = Assert.ThrowsExactly<ArgumentNullException>(() => input = DelegateTransform.With(input, (Func<int, int>)null!));
	}

	[TestMethod]
	public void WithFuncRefModifiesInput()
	{
		int input = 5;
		int expected = 25;

		int result = DelegateTransform.With(input, SquareValue);

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void WithFuncRefThrowsArgumentNullException()
	{
		int input = 5;

		_ = Assert.ThrowsExactly<ArgumentNullException>(() => DelegateTransform.With(input, (FuncRef<int>)null!));
	}
}
