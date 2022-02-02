using MyNamespace;
using SimpleInfoName;

[UsesVerify]
public class Tests
{
    [Fact]
    public Task WithOneGeneric()
    {
        return Verify(typeof(Dictionary<string, ConcurrentDictionary<string, string>>).SimpleName());
    }

    [Fact]
    public Task Simple()
    {
        return Verify(typeof(string).SimpleName());
    }

    [Fact]
    public Task GenericTypeDefinition()
    {
        return Verify(typeof(IEnumerable<>).SimpleName());
    }

    [Fact]
    public Task GenericArguments()
    {
        var type = typeof(IEnumerable<>)
            .GetGenericArguments()
            .First();
        return Verify(type.SimpleName());
    }

    [Fact]
    public Task Nested()
    {
        return Verify(typeof(TargetWithNested).SimpleName());
    }

    [Fact]
    public Task Nullable()
    {
        return Verify(typeof(int?).SimpleName());
    }

    [Fact]
    public Task Array()
    {
        return Verify(typeof(int[]).SimpleName());
    }

    [Fact]
    public Task ArrayMulti()
    {
        return Verify(typeof(int[,]).SimpleName());
    }

    [Fact]
    public Task ArrayNullable()
    {
        return Verify(typeof(int?[]).SimpleName());
    }

    [Fact]
    public Task List()
    {
        return Verify(typeof(List<TargetWithNamespace>).SimpleName());
    }

    [Fact]
    public Task Enumerable()
    {
        return Verify(typeof(IEnumerable<TargetWithNamespace>).SimpleName());
    }

    [Fact]
    public Task Dictionary()
    {
        return Verify(typeof(Dictionary<string, int>).SimpleName());
    }

    [Fact]
    public Task Dictionary2()
    {
        return Verify(typeof(Dictionary<IEnumerable<TargetWithNamespace>, IEnumerable<TargetWithNamespace>>).SimpleName());
    }

    [Fact]
    public Task Dynamic()
    {
        return Verify(new {Name = "foo"}.GetType().SimpleName());
    }

    [Fact]
    public Task RuntimeEnumerable()
    {
        return Verify(MethodWithYield().GetType().SimpleName());
    }

    [Fact]
    public Task RuntimeEnumerableDynamic()
    {
        return Verify(MethodWithYieldDynamic().GetType().SimpleName());
    }

    [Fact]
    public Task RuntimeEnumerableWithSelect()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        return Verify(MethodWithYield().Select(x => x != null).GetType().SimpleName());
    }

    [Fact]
    public Task RuntimeEnumerableDynamicWithSelect()
    {
        return Verify(MethodWithYieldDynamic().Select(x => x != null).GetType().SimpleName());
    }

    [Fact]
    public Task RuntimeEnumerableDynamicWithInnerSelect()
    {
        return Verify(MethodWithYield().Select(x => new {X = x.ToString()}).GetType().SimpleName());
    }

    [Fact]
    public Task EnumerableOfArray()
    {
        return Verify(typeof(IEnumerable<TargetWithNamespace[]>).SimpleName());
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
        return Verify(typeof(List<TargetWithNamespace[]>).SimpleName());
    }

    [Fact]
    public Task ArrayOfList()
    {
        return Verify(typeof(List<TargetWithNamespace>[]).SimpleName());
    }

    class TargetWithNested
    {
    }

    [Fact]
    public Task NestedWithParentGeneric()
    {
        return Verify(typeof(Parent<int>.Child).SimpleName());
    }

    [Fact]
    public Task NestedWithParentGeneric_Def()
    {
        return Verify(typeof(Parent<>.Child).SimpleName());
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
        return Verify(typeof(One<int>.Two<string>.Three).SimpleName());
    }

    [Fact]
    public Task NestedWithParentGenericTriple_Def()
    {
        return Verify(typeof(One<>.Two<>.Three).SimpleName());
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
        return Verify(typeof(From).SimpleName());
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