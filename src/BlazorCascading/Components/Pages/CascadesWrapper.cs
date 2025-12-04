using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorCascading.Components.Pages;

// Renders the supplied ChildContent wrapped in cascading values provided via the Cascades parameter.
[Route("/cascades-wrapper")]
public class CascadesWrapper : ComponentBase
{
    [Parameter] public IReadOnlyList<(string Name, object? Value)> Cascades { get; set; } = Array.Empty<(string, object?)>();
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var cascades = Cascades ?? Array.Empty<(string Name, object? Value)>();
        RenderFragment content = ChildContent ?? (_ => { });

        // Build nested cascading values from the provided list.
        for (int i = cascades.Count - 1; i >= 0; i--)
        {
            var item = cascades[i];
            var inner = content;

            content = b =>
            {
                b.OpenComponent<CascadingValue<object?>>(0);
                b.AddComponentParameter(1, nameof(CascadingValue<object?>.Name), item.Name);
                b.AddComponentParameter(2, nameof(CascadingValue<object?>.Value), item.Value);
                b.AddComponentParameter(3, "ChildContent", inner);
                b.CloseComponent();
            };
        }

        builder.AddContent(0, content);
    }
}
