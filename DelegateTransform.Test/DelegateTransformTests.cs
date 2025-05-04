// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.DelegateTransform.Test;

[TestClass]
public class DelegateTransformTests
{
	[TestMethod]
	public void WithActionRefModifiesInput()
	{
		var input = 5;
		var expected = 10;

		var result = DelegateTransform.With(input, (ref int x) => x *= 2);

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void WithActionRefThrowsArgumentNullException()
	{
		var input = 5;

		Assert.ThrowsException<ArgumentNullException>(() => input = DelegateTransform.With(input, (ActionRef<int>)null!));
	}

	[TestMethod]
	public void WithFuncModifiesInput()
	{
		var input = 5;
		var expected = 15;

		var result = DelegateTransform.With(input, x => x + 10);

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void WithFuncThrowsArgumentNullException()
	{
		var input = 5;

		Assert.ThrowsException<ArgumentNullException>(() => input = DelegateTransform.With(input, (Func<int, int>)null!));
	}

	[TestMethod]
	public void WithFuncRefModifiesInput()
	{
		var input = 5;
		var expected = 25;

		var result = DelegateTransform.With(input, (ref int x) => x * x);

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void WithFuncRefThrowsArgumentNullException()
	{
		var input = 5;

		Assert.ThrowsException<ArgumentNullException>(() => DelegateTransform.With(input, (FuncRef<int>)null!));
	}
}
