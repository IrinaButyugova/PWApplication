using Microsoft.AspNetCore.Components;

namespace PWComponents.Components
{
	public partial class PWFilterComponent
	{
		[Parameter]
		public string Title { get; set; } = "";

		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
