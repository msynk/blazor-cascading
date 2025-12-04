namespace BlazorCascading.Components.Pages;

public class CascadingValueRecord
{
    private CascadingValueRecord(object? value, string? name, Type valueType)
    {
        Name = name;
        Value = value;
        ValueType = valueType ?? value?.GetType() ?? throw new ArgumentNullException(nameof(valueType));
    }

    public string? Name { get; }
    public object? Value { get; }
    public Type ValueType { get; }

    public static CascadingValueRecord From<T>(T value, string? name = null) => new(value, name, typeof(T));
}
