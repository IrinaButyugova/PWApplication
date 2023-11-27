using Microsoft.AspNetCore.Components;

namespace PWComponents.Components
{
	public partial class PWSubmitButton
	{
		[Parameter]
		public string Title { get; set; } = "Submit";
	}
}
