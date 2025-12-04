namespace BlazorCascading.Components.Pages;

public class CascadingValueList : List<CascadingValueRecord>
{
    public new void Add<T>(T value, string? name = null) => base.Add(CascadingValueRecord.From(value, name));
}
