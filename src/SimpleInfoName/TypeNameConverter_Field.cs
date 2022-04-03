namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    public static string SimpleName(this FieldInfo field) =>
        infoCache.GetOrAdd(field, _ =>
        {
            if (field.DeclaringType is null)
            {
                return $"Module.{field.Name}";
            }
            var declaringType = SimpleName(field.DeclaringType);
            return $"{declaringType}.{field.Name}";
        });
}