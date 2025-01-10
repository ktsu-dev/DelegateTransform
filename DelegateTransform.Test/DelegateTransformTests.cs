namespace ktsu.DelegateTransform.Test;

[TestClass]
public class DelegateTransformTests
{
	[TestMethod]
	public void WithActionRefModifiesInput()
	{
		int input = 5;
		int expected = 10;

		int result = DelegateTransform.With(input, (ref int x) => x *= 2);

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void WithActionRefThrowsArgumentNullException()
	{
		int input = 5;

		Assert.ThrowsException<ArgumentNullException>(() => input = DelegateTransform.With(input, (ActionRef<int>)null!));
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

		Assert.ThrowsException<ArgumentNullException>(() => input = DelegateTransform.With(input, (Func<int, int>)null!));
	}

	[TestMethod]
	public void WithFuncRefModifiesInput()
	{
		int input = 5;
		int expected = 25;

		int result = DelegateTransform.With(input, (ref int x) => x * x);

		Assert.AreEqual(expected, result);
	}

	[TestMethod]
	public void WithFuncRefThrowsArgumentNullException()
	{
		int input = 5;

		Assert.ThrowsException<ArgumentNullException>(() => DelegateTransform.With(input, (FuncRef<int>)null!));
	}
}
