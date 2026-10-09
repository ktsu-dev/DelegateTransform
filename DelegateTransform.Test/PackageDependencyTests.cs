// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.DelegateTransform.Test;

using System.IO;
using System.Text.Json;

[TestClass]
public class PackageDependencyTests
{
	// A PackageReference the library never uses still flows into its nuspec, so every consumer
	// restores it. The test's own deps.json records the library's resolved dependencies, which is
	// the same closure a consumer of the package gets.
	[TestMethod]
	public void LibraryDependsOnlyOnPackagesItUses()
	{
		string depsPath = Path.Join(AppContext.BaseDirectory, "ktsu.DelegateTransform.Test.deps.json");
		using JsonDocument deps = JsonDocument.Parse(File.ReadAllText(depsPath));

		foreach (JsonProperty library in deps.RootElement.GetProperty("libraries").EnumerateObject())
		{
			Assert.IsFalse(
				library.Name.StartsWith("ktsu.ScopedAction/", StringComparison.Ordinal),
				$"{library.Name} is restored although nothing in ktsu.DelegateTransform uses it.");
		}
	}
}
