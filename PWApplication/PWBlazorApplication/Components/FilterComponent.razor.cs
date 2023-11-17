using Microsoft.AspNetCore.Components;

namespace PWBlazorApplication.Components
{
	public partial class FilterComponent
	{
		[Parameter]
		public string Title { get; set; } = "";

		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
