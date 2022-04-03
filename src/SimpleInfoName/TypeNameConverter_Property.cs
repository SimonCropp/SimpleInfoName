namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    public static string SimpleName(this PropertyInfo property) =>
        infoCache.GetOrAdd(property, _ =>
        {
            if (property.DeclaringType is null)
            {
                return $"Module.{property.Name}";
            }
            var declaringType = SimpleName(property.DeclaringType);
            return $"{declaringType}.{property.Name}";
        });
}