using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorCascading.Components.Pages;

public class ManualProvider : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var current = ChildContent;

        var p1 = current;
        string? v = null;
        current = b => CreateCascadingValue(b, 8, "Gender", v, p1);

        var p2 = current;
        current = b => CreateCascadingValue(b, 4, "Age", 41, p2);

        CreateCascadingValue(builder, 0, "Name", "Saleh Xafan", current);

        base.BuildRenderTree(builder);
    }

    public static void CreateCascadingValue<T>(RenderTreeBuilder builder, int seq, string name, T value, RenderFragment child)
    {
        builder.OpenComponent<CascadingValue<T>>(seq);
        builder.AddComponentParameter(++seq, "Name", name);
        builder.AddComponentParameter(++seq, "Value", value);
        builder.AddComponentParameter(++seq, "ChildContent", child);
        builder.CloseComponent();
    }
}
