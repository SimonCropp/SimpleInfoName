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
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(Dictionary<string, ConcurrentDictionary<string, string>>)));
    }

    [Fact]
    public Task Simple()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(string)));
    }

    [Fact]
    public Task GenericTypeDefinition()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(IEnumerable<>)));
    }

    [Fact]
    public Task GenericArguments()
    {
        var type = typeof(IEnumerable<>)
            .GetGenericArguments()
            .First();
        return Verifier.Verify(TypeNameConverter.SimpleName(type));
    }

    [Fact]
    public Task Nested()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(TargetWithNested)));
    }

    [Fact]
    public Task Nullable()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(int?)));
    }

    [Fact]
    public Task Array()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(int[])));
    }

    [Fact]
    public Task ArrayMulti()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(int[,])));
    }

    [Fact]
    public Task ArrayNullable()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(int?[])));
    }

    [Fact]
    public Task List()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(List<TargetWithNamespace>)));
    }

    [Fact]
    public Task Enumerable()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(IEnumerable<TargetWithNamespace>)));
    }

    [Fact]
    public Task Dictionary()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(Dictionary<string,int>)));
    }
    [Fact]
    public Task Dictionary2()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(Dictionary<IEnumerable<TargetWithNamespace>,IEnumerable<TargetWithNamespace>>)));
    }

    [Fact]
    public Task Dynamic()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(new {Name = "foo"}.GetType()));
    }

    [Fact]
    public Task RuntimeEnumerable()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(MethodWithYield().GetType()));
    }

    [Fact]
    public Task RuntimeEnumerableDynamic()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(MethodWithYieldDynamic().GetType()));
    }

    [Fact]
    public Task RuntimeEnumerableWithSelect()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        return Verifier.Verify(TypeNameConverter.SimpleName(MethodWithYield().Select(x => x != null).GetType()));
    }

    [Fact]
    public Task RuntimeEnumerableDynamicWithSelect()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(MethodWithYieldDynamic().Select(x => x != null).GetType()));
    }

    [Fact]
    public Task RuntimeEnumerableDynamicWithInnerSelect()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(MethodWithYield().Select(x => new {X = x.ToString()}).GetType()));
    }

    [Fact]
    public Task EnumerableOfArray()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(IEnumerable<TargetWithNamespace[]>)));
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
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(List<TargetWithNamespace[]>)));
    }

    [Fact]
    public Task ArrayOfList()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(List<TargetWithNamespace>[])));
    }

    class TargetWithNested
    {
    }
    
    [Fact]
    public Task NestedWithParentGeneric()
    {
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(Parent<int>.Child)));
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
        return Verifier.Verify(TypeNameConverter.SimpleName(typeof(One<int>.Two<string>.Three)));
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
}

namespace MyNamespace
{
    public class TargetWithNamespace{}
}