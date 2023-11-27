using Microsoft.AspNetCore.Components;

namespace PWComponents.Components
{
    public partial class PWInputRow
    {
		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
