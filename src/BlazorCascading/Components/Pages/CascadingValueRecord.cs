namespace BlazorCascading.Components.Pages;

public class CascadingValueList : List<CascadingValueRecord>
{
    public void Add<T>(string? name, T value) => Add(CascadingValueRecord.From(name, value));

    public void Add(string? name, object? value, Type valueType) => Add(CascadingValueRecord.From(name, value, valueType));
}

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
