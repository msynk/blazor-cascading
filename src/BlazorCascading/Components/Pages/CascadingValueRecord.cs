namespace BlazorCascading.Components.Pages;

public class CascadingValueRecord
{
    private CascadingValueRecord(string? name, object? value, Type valueType)
    {
        Name = name;
        Value = value;
        ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
    }

    public string? Name { get; }
    public object? Value { get; }
    public Type ValueType { get; }

    public static CascadingValueRecord From<T>(string? name, T value) => new(name, value, typeof(T));

    public static CascadingValueRecord From(string? name, object? value, Type valueType) => new(name, value, valueType);
}
