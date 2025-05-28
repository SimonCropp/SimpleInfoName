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

    public static string SimpleName(this MethodInfo method) =>
        cache.GetOrAdd(
            method,
            _ =>
            {
                var declaringType = SimpleName(method.DeclaringType!);

                var parameters = method.GetParameters();
                if (parameters.Length == 0)
                {
                    return $"{declaringType}.{method.Name}()";
                }

                var builder = new StringBuilder($"{declaringType}.{method.Name}(");
                foreach (var parameter in parameters)
                {
                    builder.Append(SimpleName(parameter.ParameterType));
                    builder.Append(' ');
                    builder.Append(parameter.Name);
                    builder.Append(", ");
                }

                builder.Length -= 2;
                builder.Append(')');
                return builder.ToString();
            });
}