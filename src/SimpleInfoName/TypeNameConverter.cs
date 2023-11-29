namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    static ConcurrentDictionary<ICustomAttributeProvider, string> infoCache = [];

    public static bool TryGetSimpleName(
        object info,
        [NotNullWhen(true)] out string? name)
    {
        if (info is ICustomAttributeProvider provider)
        {
            return TryGetSimpleName(provider, out name);
        }

        name = null;
        return false;
    }

    public static bool TryGetSimpleName(
        this ICustomAttributeProvider provider,
        [NotNullWhen(true)] out string? name)
    {
        switch (provider)
        {
            case ParameterInfo parameter:
                name = SimpleName(parameter);
                return true;
            case FieldInfo field:
                name = SimpleName(field);
                return true;
            case ConstructorInfo constructor:
                name = SimpleName(constructor);
                return true;
            case MethodInfo method:
                name = SimpleName(method);
                return true;
            case PropertyInfo property:
                name = SimpleName(property);
                return true;
            case TypeInfo type:
                name = SimpleName(type);
                return true;
        }

        name = null;
        return false;
    }
}