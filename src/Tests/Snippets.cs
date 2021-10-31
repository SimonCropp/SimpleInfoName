﻿#if NET48

using MyNamespace;
using VerifyXunit;
using Xunit;
using SimpleInfoName;
using VerifyTests;

#region Target
namespace MyNamespace
{
    public class Parent<T> { }

    public class Target<K> : Parent<int>
    {
        public Dictionary<string, K> Property { get; set; } = null!;
        public Dictionary<string, K> field = null!;

        public Dictionary<Y, D> Method<Y, D>(List<D> parameter)
        {
            return new Dictionary<Y, D>();
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

        writer.WriteLine($@" * Type `{type.SimpleName()}`. Compared to Type.FullName of `{type.FullName}`");

        var method = type.GetMethod("Method")!.MakeGenericMethod(typeof(string),typeof(bool));

        writer.WriteLine($@" * Method `{method.SimpleName()}`");
        writer.WriteLine($@" * Parameter `{method.GetParameters().First().SimpleName()}`");
        writer.WriteLine($@" * Field `{type.GetField("field").SimpleName()}`");
        writer.WriteLine($@" * Property `{type.GetProperty("Property")!.SimpleName()}`");
    }

}
#endif