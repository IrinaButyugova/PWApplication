using Microsoft.AspNetCore.Components;

namespace PWComponents.Components
{
	public partial class PWInputComponent
	{
		[Parameter]
		public string Title { get; set; } = "";

		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
