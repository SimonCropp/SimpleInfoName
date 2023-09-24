#if NET48

// ReSharper disable UnusedTypeParameter

#region Target

namespace MyNamespace
{
    public class Parent<T>;

    public class Target<K> : Parent<int>
    {
        // ReSharper disable once EmptyConstructor
        public Target()
        {
        }

        public string Property { get; set; } = null!;
        public string field = null!;

        public void Method<Y, D>(List<D> parameter)
        {
        }
    }
}

#endregion

[UsesVerify]
public class Snippets
{
    [Fact]
    public void WriteMarkdown()
    {
        var md = Path.Combine(AttributeReader.GetSolutionDirectory(), "sample.include.md");
        File.Delete(md);
        using var writer = File.CreateText(md);
        var type = typeof(Target<int>);

        writer.WriteLine("|   |   |");
        writer.WriteLine("| - | - |");
        writer.WriteLine($"| Type | `{type.SimpleName()}` |");
        writer.WriteLine($"| | Compared to `Type.FullName` of<br> `{type.FullName!.Replace("`", "'")}` |");

        var method = type.GetMethod("Method")!.MakeGenericMethod(typeof(string), typeof(bool));

        var constructorInfos = type.GetConstructors();
        writer.WriteLine($"| Constructor | `{constructorInfos.Single().SimpleName()}` |");
        writer.WriteLine($"| Method | `{method.SimpleName()}` |");
        writer.WriteLine($"| Parameter | `{method.GetParameters().First().SimpleName()}` |");
        writer.WriteLine($"| Field | `{type.GetField("field").SimpleName()}` |");
        writer.WriteLine($"| Property | `{type.GetProperty("Property")!.SimpleName()}` |");
    }
}

#endif