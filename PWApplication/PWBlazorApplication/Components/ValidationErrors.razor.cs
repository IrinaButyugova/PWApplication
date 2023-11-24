using Microsoft.AspNetCore.Components;

namespace PWBlazorApplication.Components
{
	public partial class ValidationErrors
    {
		[Parameter]
		public List<string> Errors { get; set; } = new List<string>();
	}
}
