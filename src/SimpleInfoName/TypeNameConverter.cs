namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    static ConcurrentDictionary<ICustomAttributeProvider, string> infoCache = new();
}