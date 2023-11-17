using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace PWBlazorApplication.Components
{
	public partial class Button
	{
		[Parameter]
		public string Title { get; set; } = "";
		[Parameter]
		public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
	}
}
