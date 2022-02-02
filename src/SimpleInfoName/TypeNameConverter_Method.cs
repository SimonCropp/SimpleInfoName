namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    static string SimpleName(this MemberInfo methodBase)
    {
        if (methodBase is ConstructorInfo constructor)
        {
            return SimpleName(constructor);
        }

        if (methodBase is MethodInfo method)
        {
            return SimpleName(method);
        }

        throw new InvalidOperationException();
    }

    public static string SimpleName(this MethodInfo method)
    {
        return infoCache.GetOrAdd(method, _ =>
        {
            var declaringType = SimpleName(method.DeclaringType!);
            StringBuilder builder = new($"{declaringType}.{method.Name}(");
            var parameters = method.GetParameters()
                .Select(x => $"{SimpleName(x.ParameterType)} {x.Name}");
            builder.Append(string.Join(", ", parameters));
            builder.Append(')');
            return builder.ToString();
        });
    }
}