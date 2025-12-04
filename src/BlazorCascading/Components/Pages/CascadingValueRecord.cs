namespace BlazorCascading.Components.Pages;

public class CascadingValueRecord(string? name, object? value, Type? valuetype = null)
{
    public string? Name { get; } = name;
    public object? Value { get; } = value;
    public Type ValueType { get; } = valuetype ?? value?.GetType() ?? throw new InvalidOperationException("Value type is missing");
}
