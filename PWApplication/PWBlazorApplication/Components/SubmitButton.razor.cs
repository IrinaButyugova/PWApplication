using Microsoft.AspNetCore.Components;

namespace PWBlazorApplication.Components
{
	public partial class SubmitButton
	{
		[Parameter]
		public string Title { get; set; } = "Submit";
	}
}
