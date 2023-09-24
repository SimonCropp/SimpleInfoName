namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    public static string SimpleName(this ConstructorInfo constructor) =>
        infoCache.GetOrAdd(
            constructor,
            _ =>
            {
                var declaringType = SimpleName(constructor.DeclaringType!);

                if (constructor.IsStatic)
                {
                    return $"{declaringType}.cctor()";
                }

                var parameters = constructor.GetParameters();

                if (parameters.Length == 0)
                {
                    return $"{declaringType}.ctor()";
                }

                var builder = new StringBuilder(declaringType);
                builder.Append(".ctor(");

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