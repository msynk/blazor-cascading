namespace BlazorCascading.Components.Pages;

public class CascadingValueList : List<CascadingValueRecord>
{
    public void Add<T>(string? name, T value) => Add(CascadingValueRecord.From(name, value));

    public void Add(string? name, object? value, Type valueType) => Add(CascadingValueRecord.From(name, value, valueType));
}
