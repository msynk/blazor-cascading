using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorCascading.Components.Pages;

public class CascadingValueProvider : ComponentBase
{
    [Parameter] public List<(string Name, object Value)> Values { get; set; } = [];

    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var list = Values;

        RenderFragment current = ChildContent;

        for (int i = list.Count - 1; i > 0; i--)
        {
            var item = list[i];
            var prev = current;
            current = b => CreateCascadingValue(b, i * 4, item.Name, item.Value, prev);
        }

        CreateCascadingValue(builder, 0, list[0].Name, list[0].Value, current);

        base.BuildRenderTree(builder);
    }

    private static readonly Type _cascadingValueType = typeof(CascadingValue<>);

    public static void CreateCascadingValue(RenderTreeBuilder builder, int seq, string name, object value, RenderFragment? innerBuilder)
    {
        builder.OpenComponent(seq, _cascadingValueType.MakeGenericType(value.GetType()));
        builder.AddComponentParameter(seq++, "Name", name);
        builder.AddComponentParameter(seq++, "Value", value);
        builder.AddComponentParameter(seq++, "ChildContent", innerBuilder);
        builder.CloseComponent();
    }
}
