namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    public static string SimpleName(this ParameterInfo parameter) =>
        cache.GetOrAdd(
            parameter,
            _ =>
            {
                var member = SimpleName(parameter.Member);
                return $"'{parameter.Name}' of {member}";
            });
}