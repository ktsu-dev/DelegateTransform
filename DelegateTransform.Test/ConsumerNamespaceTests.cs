// Copyright (c) 2023-2026 ktsu-dev contributors

// This file lives outside ktsu.DelegateTransform on purpose. From any other ktsu.* namespace the
// simple name DelegateTransform resolves to the namespace, not the class, so these tests only
// compile while the README's calling forms avoid naming the type.
namespace ktsu.Consumer;

using ktsu.DelegateTransform;

[TestClass]
public class ConsumerNamespaceTests
{
	private sealed class Person
	{
		public string Name { get; set; } = string.Empty;
		public int Age { get; set; }
	}

	private static void DoubleValue(ref int x) => x *= 2;

	private static int SquareValue(ref int x) => x * x;

	private static void HaveBirthday(ref Person p)
	{
		p.Name = p.Name.ToUpperInvariant();
		p.Age += 1;
	}

	[TestMethod]
	public void ActionRefExtensionReturnsModifiedCopy()
	{
		int input = 5;

		int result = input.With(DoubleValue);

		Assert.AreEqual(10, result);
		Assert.AreEqual(5, input);
	}

	[TestMethod]
	public void FuncExtensionTransformsInput()
	{
		int result = 5.With(x => x * 2);

		Assert.AreEqual(10, result);
	}

	[TestMethod]
	public void FuncRefExtensionTransformsInput()
	{
		int input = 5;

		int result = input.With(SquareValue);

		Assert.AreEqual(25, result);
		Assert.AreEqual(5, input);
	}

	[TestMethod]
	public void ExtensionsChainFluently()
	{
		string result = "example"
			.With(s => s.ToUpperInvariant())
			.With(s => s.Replace("EX", "**", StringComparison.Ordinal))
			.With(s => s + " transformed");

		Assert.AreEqual("**AMPLE transformed", result);
	}

	[TestMethod]
	public void FuncExtensionMapsToAnotherType()
	{
		Person person = new() { Name = "John", Age = 30 };

		Person updated = person.With(HaveBirthday);
		string description = person.With(p => $"{p.Name} is {p.Age} years old");

		Assert.AreEqual("JOHN", updated.Name);
		Assert.AreEqual(31, updated.Age);
		Assert.AreEqual("JOHN is 31 years old", description);
	}

	[TestMethod]
	public void MappingFuncThrowsArgumentNullException() =>
		_ = Assert.ThrowsExactly<ArgumentNullException>(() => 5.With((Func<int, string>)null!));
}
