using MyNamespace;
using VerifyXunit;
using Xunit;
using SimpleInfoName;

[UsesVerify]
public class Tests
{
    [Fact]
    public Task WithOneGeneric()
    {
        return Verifier.Verify(typeof(Dictionary<string, ConcurrentDictionary<string, string>>).SimpleName());
    }

    [Fact]
    public Task Simple()
    {
        return Verifier.Verify(typeof(string).SimpleName());
    }

    [Fact]
    public Task GenericTypeDefinition()
    {
        return Verifier.Verify(typeof(IEnumerable<>).SimpleName());
    }

    [Fact]
    public Task GenericArguments()
    {
        var type = typeof(IEnumerable<>)
            .GetGenericArguments()
            .First();
        return Verifier.Verify(type.SimpleName());
    }

    [Fact]
    public Task Nested()
    {
        return Verifier.Verify(typeof(TargetWithNested).SimpleName());
    }

    [Fact]
    public Task Nullable()
    {
        return Verifier.Verify(typeof(int?).SimpleName());
    }

    [Fact]
    public Task Array()
    {
        return Verifier.Verify(typeof(int[]).SimpleName());
    }

    [Fact]
    public Task ArrayMulti()
    {
        return Verifier.Verify(typeof(int[,]).SimpleName());
    }

    [Fact]
    public Task ArrayNullable()
    {
        return Verifier.Verify(typeof(int?[]).SimpleName());
    }

    [Fact]
    public Task List()
    {
        return Verifier.Verify(typeof(List<TargetWithNamespace>).SimpleName());
    }

    [Fact]
    public Task Enumerable()
    {
        return Verifier.Verify(typeof(IEnumerable<TargetWithNamespace>).SimpleName());
    }

    [Fact]
    public Task Dictionary()
    {
        return Verifier.Verify(typeof(Dictionary<string, int>).SimpleName());
    }

    [Fact]
    public Task Dictionary2()
    {
        return Verifier.Verify(typeof(Dictionary<IEnumerable<TargetWithNamespace>, IEnumerable<TargetWithNamespace>>).SimpleName());
    }

    [Fact]
    public Task Dynamic()
    {
        return Verifier.Verify(new {Name = "foo"}.GetType().SimpleName());
    }

    [Fact]
    public Task RuntimeEnumerable()
    {
        return Verifier.Verify(MethodWithYield().GetType().SimpleName());
    }

    [Fact]
    public Task RuntimeEnumerableDynamic()
    {
        return Verifier.Verify(MethodWithYieldDynamic().GetType().SimpleName());
    }

    [Fact]
    public Task RuntimeEnumerableWithSelect()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        return Verifier.Verify(MethodWithYield().Select(x => x != null).GetType().SimpleName());
    }

    [Fact]
    public Task RuntimeEnumerableDynamicWithSelect()
    {
        return Verifier.Verify(MethodWithYieldDynamic().Select(x => x != null).GetType().SimpleName());
    }

    [Fact]
    public Task RuntimeEnumerableDynamicWithInnerSelect()
    {
        return Verifier.Verify(MethodWithYield().Select(x => new {X = x.ToString()}).GetType().SimpleName());
    }

    [Fact]
    public Task EnumerableOfArray()
    {
        return Verifier.Verify(typeof(IEnumerable<TargetWithNamespace[]>).SimpleName());
    }

    static IEnumerable<TargetWithNamespace> MethodWithYield()
    {
        yield return new();
    }

    static IEnumerable<dynamic> MethodWithYieldDynamic()
    {
        yield return new {X = "1"};
    }

    [Fact]
    public Task ListOfArray()
    {
        return Verifier.Verify(typeof(List<TargetWithNamespace[]>).SimpleName());
    }

    [Fact]
    public Task ArrayOfList()
    {
        return Verifier.Verify(typeof(List<TargetWithNamespace>[]).SimpleName());
    }

    class TargetWithNested
    {
    }

    [Fact]
    public Task NestedWithParentGeneric()
    {
        return Verifier.Verify(typeof(Parent<int>.Child).SimpleName());
    }

    [Fact]
    public Task NestedWithParentGeneric_Def()
    {
        return Verifier.Verify(typeof(Parent<>.Child).SimpleName());
    }

    public class Parent<T>
    {
        public class Child : Parent<T>
        {
            public T Value { get; }

            public Child(T value)
            {
                Value = value;
            }
        }
    }

    [Fact]
    public Task NestedWithParentGenericTriple()
    {
        return Verifier.Verify(typeof(One<int>.Two<string>.Three).SimpleName());
    }

    [Fact]
    public Task NestedWithParentGenericTriple_Def()
    {
        return Verifier.Verify(typeof(One<>.Two<>.Three).SimpleName());
    }

    public class One<T>
    {
        public class Two<K> : One<T>
        {
            public class Three : Two<K>
            {
                public T Value { get; }

                public Three(T value)
                {
                    Value = value;
                }
            }
        }
    }

    [Fact]
    public Task Redirect()
    {
        TypeNameConverter.AddRedirect<From, To>();
        return Verifier.Verify(typeof(From).SimpleName());
    }

    public class From
    {
    }

    public class To
    {
    }
}

namespace MyNamespace
{
    public class TargetWithNamespace
    {
    }
}