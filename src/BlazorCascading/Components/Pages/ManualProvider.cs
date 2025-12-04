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
        current = b => CreateCascadingValue(b, 8, "Gender", "Male", p1);

        var p2 = current;
        current = b => CreateCascadingValue(b, 4, "Age", 41, p2);

        CreateCascadingValue(builder, 0, "Name", "Saleh Xafan", current);

        base.BuildRenderTree(builder);
    }

    private static readonly Type _cascadingValueType = typeof(CascadingValue<>);

    public static void CreateCascadingValue(RenderTreeBuilder builder, int seq, string name, object value, RenderFragment child)
    {
        Console.WriteLine($"seq:{seq}, Name:{name}");
        builder.OpenComponent(seq, _cascadingValueType.MakeGenericType(value.GetType()));
        builder.AddComponentParameter(++seq, "Name", name);
        builder.AddComponentParameter(++seq, "Value", value);
        builder.AddComponentParameter(++seq, "ChildContent", child);
        builder.CloseComponent();
    }
}
