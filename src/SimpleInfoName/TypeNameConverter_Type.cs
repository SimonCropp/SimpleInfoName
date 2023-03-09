namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    static ConcurrentDictionary<Type, Func<Type, Type>> redirects = new();

    public static void AddRedirect<TFrom, TTo>() =>
        AddRedirect(typeof(TFrom), typeof(TTo));

    public static void AddRedirect(Type from, Type to) =>
        AddRedirect(from, _ => to);

    public static void AddRedirect(Type from, Func<Type, Type> convert) =>
        redirects[from] = convert;

    static ConcurrentDictionary<Type, string> cacheDictionary = new(
        new List<KeyValuePair<Type, string>>
        {
            new(typeof(object), "object"),
            new(typeof(char), "char"),
            new(typeof(char?), "char?"),
            new(typeof(string), "string"),
            new(typeof(sbyte), "sbyte"),
            new(typeof(sbyte?), "sbyte?"),
            new(typeof(byte), "byte"),
            new(typeof(byte?), "byte?"),
            new(typeof(bool), "bool"),
            new(typeof(bool?), "bool?"),
            new(typeof(short), "short"),
            new(typeof(short?), "short?"),
            new(typeof(ushort), "ushort"),
            new(typeof(ushort?), "ushort?"),
            new(typeof(int), "int"),
            new(typeof(int?), "int?"),
            new(typeof(uint), "uint"),
            new(typeof(uint?), "uint?"),
            new(typeof(long), "long"),
            new(typeof(long?), "long?"),
            new(typeof(ulong), "ulong"),
            new(typeof(ulong?), "ulong?"),
            new(typeof(nint), "nint"),
            new(typeof(nint?), "nint"),
            new(typeof(nuint), "nuint"),
            new(typeof(nuint?), "nuint?"),
            new(typeof(decimal), "decimal"),
            new(typeof(decimal?), "decimal?"),
            new(typeof(float), "float"),
            new(typeof(float?), "float?"),
            new(typeof(double), "double"),
            new(typeof(double?), "double?"),
            new(typeof(Guid), "Guid"),
            new(typeof(Guid?), "Guid?"),
            new(typeof(DateTime), "DateTime"),
            new(typeof(DateTime?), "DateTime?"),
            new(typeof(DateTimeOffset), "DateTimeOffset"),
            new(typeof(DateTimeOffset?), "DateTimeOffset?"),
            new(typeof(TimeSpan), "TimeSpan"),
            new(typeof(TimeSpan?), "TimeSpan?"),
#if NET5_0_OR_GREATER
            new(typeof(Half), "Half"),
            new(typeof(Half?), "Half?"),
#endif
#if NET6_0_OR_GREATER
            new(typeof(Date), "DateOnly"),
            new(typeof(Date?), "DateOnly?"),
            new(typeof(Time), "TimeOnly"),
            new(typeof(Time?), "TimeOnly?"),
#endif
        });

    public static string SimpleName(this Type type) =>
        cacheDictionary.GetOrAdd(type, Inner);

    static string Inner(Type type)
    {
        foreach (var redirect in redirects)
        {
            if (redirect.Key.IsAssignableFrom(type))
            {
                return redirect.Value(type).SimpleName();
            }
        }

        var name = type.Name;

        if (IsAnonType(name))
        {
            return "dynamic";
        }

        if (name.StartsWith('<') ||
            type.IsNested &&
            type.DeclaringType == typeof(Enumerable))
        {
            foreach (var iface in type.GetInterfaces())
            {
                if (!iface.IsGenericType ||
                    iface.GetGenericTypeDefinition() != typeof(IEnumerable<>))
                {
                    continue;
                }

                if (iface.GetGenericArguments()[0].IsAnonType())
                {
                    return "IEnumerable<dynamic>";
                }

                return SimpleName(iface);
            }
        }

        if (type.IsGenericParameter)
        {
            return name;
        }

        if (type.IsArray)
        {
            var elementType = type.GetElementType()!;
            var rank = type.GetArrayRank();

            return $"{SimpleName(elementType)}[{new string(',', rank - 1)}]";
        }

        Type? declaringType = null;
        if (type.IsGenericType)
        {
            var genericArguments = type.GetGenericArguments();
            if (type.IsNested)
            {
                declaringType = type.DeclaringType!;
                if (declaringType.IsGenericTypeDefinition)
                {
                    var parentGenericCount = declaringType.GetGenericArguments().Length;
                    var typeArguments = genericArguments.Take(parentGenericCount).ToArray();
                    declaringType = declaringType.MakeGenericType(typeArguments);
                    genericArguments = genericArguments.Skip(parentGenericCount).ToArray();
                }
            }

            if (genericArguments.Length != 0)
            {
                var tick = name.IndexOf('`');
                var builder = new StringBuilder(name.Substring(0, tick));
                builder.Append('<');
                foreach (var argument in genericArguments)
                {
                    builder.Append(SimpleName(argument));
                    builder.Append(", ");
                }

                builder.Length -= 2;
                builder.Append('>');
                name = builder.ToString();
            }
        }
        else if (type.IsNested)
        {
            declaringType = type.DeclaringType!;
        }

        if (declaringType == null)
        {
            return name;
        }

        return $"{SimpleName(declaringType)}.{name}";
    }

    static bool IsAnonType(this Type type) =>
        type.Name.Contains("AnonymousType");

    static bool IsAnonType(string name) =>
        name.Contains("AnonymousType");
}