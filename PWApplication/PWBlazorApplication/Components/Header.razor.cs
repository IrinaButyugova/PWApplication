using Microsoft.AspNetCore.Components;

namespace PWBlazorApplication.Components
{
	public partial class Header
	{
		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
