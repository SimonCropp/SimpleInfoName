namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    public static string SimpleName(this ConstructorInfo constructor) =>
        infoCache.GetOrAdd(constructor, _ =>
        {
            var declaringType = SimpleName(constructor.DeclaringType!);
            var builder = new StringBuilder($"{declaringType}");
            if (constructor.IsStatic)
            {
                builder.Append(".cctor(");
            }
            else
            {
                builder.Append(".ctor(");
            }
            var parameters = constructor.GetParameters()
                .Select(x => $"{SimpleName(x.ParameterType)} {x.Name}");
            builder.Append(string.Join(", ", parameters));
            builder.Append(')');
            return builder.ToString();
        });
}