using Microsoft.AspNetCore.Components;

namespace Whispertail_Shared;


/// <summary />
public partial class Spacer
{
    /// <summary>
    /// Gets or sets the width of the spacer (in pixels)
    /// </summary>
    [Parameter]
    public int? Width { get; set; }
}
