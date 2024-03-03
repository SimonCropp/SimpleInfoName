// ReSharper disable UnusedParameter.Local
public class Tests
{
    [Fact]
    public Task WithOneGeneric() =>
        Verify(typeof(Dictionary<string, ConcurrentDictionary<string, string>>).SimpleName());

    [Fact]
    public Task Simple() =>
        Verify(typeof(string).SimpleName());

    [Fact]
    public Task TryGetSimpleName()
    {
        var result = typeof(string).TryGetSimpleName(out var name);
        Assert.True(result);
        return Verify(name);
    }

    [Fact]
    public Task GenericTypeDefinition() =>
        Verify(typeof(IEnumerable<>).SimpleName());

    [Fact]
    public Task GenericArguments()
    {
        var type = typeof(IEnumerable<>)
            .GetGenericArguments()
            .First();
        return Verify(type.SimpleName());
    }

    [Fact]
    public Task Nested() =>
        Verify(typeof(TargetWithNested).SimpleName());

    [Fact]
    public Task Nullable() =>
        Verify(typeof(int?).SimpleName());

    [Fact]
    public Task Array() =>
        Verify(typeof(int[]).SimpleName());

    [Fact]
    public Task ArrayMulti() =>
        Verify(typeof(int[,]).SimpleName());

    [Fact]
    public Task ArrayNullable() =>
        Verify(typeof(int?[]).SimpleName());

    [Fact]
    public Task List() =>
        Verify(typeof(List<TargetWithNamespace>).SimpleName());

    [Fact]
    public Task Enumerable() =>
        Verify(typeof(IEnumerable<TargetWithNamespace>).SimpleName());

    [Fact]
    public Task Dictionary() =>
        Verify(typeof(Dictionary<string, int>).SimpleName());

    [Fact]
    public Task Dictionary2() =>
        Verify(typeof(Dictionary<IEnumerable<TargetWithNamespace>, IEnumerable<TargetWithNamespace>>).SimpleName());

    [Fact]
    public Task Dynamic() =>
        Verify(new {Name = "foo"}.GetType().SimpleName());

    [Fact]
    public Task RuntimeEnumerable() =>
        Verify(MethodWithYield().GetType().SimpleName());

    [Fact]
    public Task RuntimeEnumerableDynamic() =>
        Verify(MethodWithYieldDynamic().GetType().SimpleName());

    [Fact]
    public Task RuntimeEnumerableWithSelect() =>
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        Verify(MethodWithYield().Select(_ => _ != null).GetType().SimpleName());

    [Fact]
    public Task RuntimeEnumerableDynamicWithSelect() =>
        Verify(MethodWithYieldDynamic().Select(_ => _ != null).GetType().SimpleName());

    [Fact]
    public Task RuntimeEnumerableDynamicWithInnerSelect() =>
        Verify(MethodWithYield().Select(_ => new {X = _.ToString()}).GetType().SimpleName());

    [Fact]
    public Task EnumerableOfArray() =>
        Verify(typeof(IEnumerable<TargetWithNamespace[]>).SimpleName());

    static IEnumerable<TargetWithNamespace> MethodWithYield()
    {
        yield return new();
    }

    static IEnumerable<dynamic> MethodWithYieldDynamic()
    {
        yield return new {X = "1"};
    }

    [Fact]
    public Task ListOfArray() =>
        Verify(typeof(List<TargetWithNamespace[]>).SimpleName());

    [Fact]
    public Task ArrayOfList() =>
        Verify(typeof(List<TargetWithNamespace>[]).SimpleName());

    class TargetWithNested;

    [Fact]
    public Task NestedWithParentGeneric() =>
        Verify(typeof(Parent<int>.Child).SimpleName());

    [Fact]
    public Task NestedWithParentGeneric_Def() =>
        Verify(typeof(Parent<>.Child).SimpleName());

    public class Parent<T>
    {
        public class Child(T value) : Parent<T>
        {
            public T Value { get; } = value;
        }
    }

    [Fact]
    public Task NestedWithParentGenericTriple() =>
        Verify(typeof(One<int>.Two<string>.Three).SimpleName());

    [Fact]
    public Task NestedWithParentGenericTriple_Def() =>
        Verify(typeof(One<>.Two<>.Three).SimpleName());

    public class One<T>
    {
        public class Two<K> : One<T>
        {
            public class Three(T value) : Two<K>
            {
                public T Value { get; } = value;
            }
        }
    }

    [Fact]
    public Task Constructor() =>
        Verify(typeof(WithConstructor).GetConstructors().First().SimpleName());

    public class WithConstructor;

    [Fact]
    public Task ConstructorAndParam() =>
        Verify(typeof(WithConstructorAndParam).GetConstructors().First().SimpleName());

    public class WithConstructorAndParam
    {
        public WithConstructorAndParam(string param)
        {
        }
    }

    [Fact]
    public Task Redirect()
    {
        TypeNameConverter.AddRedirect<From, To>();
        return Verify(typeof(From).SimpleName());
    }

    public class From;

    public class To;
}

namespace MyNamespace
{
    public class TargetWithNamespace;
}