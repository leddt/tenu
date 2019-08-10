namespace Tenu.FrontEnd.PropertyConverters
{
    public interface IPropertyConverter<out TTarget>
    {
        string PropertyTypeAlias { get; }
        TTarget Convert(string rawValue);
    }

    public interface IDefaultPropertyConverter
    {
        string PropertyTypeAlias { get; }
        object ConvertDefault(string rawValue);
    }
}