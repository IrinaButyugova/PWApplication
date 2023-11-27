using Microsoft.AspNetCore.Components;

namespace PWComponents.Components
{
    public partial class PWHeader
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
