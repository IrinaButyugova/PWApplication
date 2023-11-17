using Microsoft.AspNetCore.Components;

namespace PWBlazorApplication.Components
{
    public partial class InputRow
    {
		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
