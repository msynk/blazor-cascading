using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorCascading.Components.Pages;

public class CascadingValueProvider : ComponentBase
{
    [Parameter] public List<CascadingValueRecord> Values { get; set; } = [];

    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var list = Values;

        RenderFragment current = ChildContent;

        for (int i = list.Count - 1; i > 0; i--)
        {
            var item = list[i];
            var prev = current;
            current = b => CreateCascadingValue(b, i * 4, item, prev);
        }

        CreateCascadingValue(builder, 0, list[0], current);

        base.BuildRenderTree(builder);
    }

    private static readonly Type _cascadingValueType = typeof(CascadingValue<>);

    public static void CreateCascadingValue(RenderTreeBuilder builder, int seq, CascadingValueRecord value, RenderFragment? innerBuilder)
    {
        //builder.OpenComponent(seq, _cascadingValueType.MakeGenericType(value.GetType()));
        builder.OpenComponent(seq, _cascadingValueType.MakeGenericType(value.ValueType));

        builder.AddComponentParameter(seq++, "Name", value.Name);
        builder.AddComponentParameter(seq++, "Value", value.Value);
        builder.AddComponentParameter(seq++, "ChildContent", innerBuilder);
        builder.CloseComponent();
    }
}
