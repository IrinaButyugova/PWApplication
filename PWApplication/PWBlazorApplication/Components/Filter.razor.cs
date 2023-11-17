using Microsoft.AspNetCore.Components;
using PWBlazorApplication.Models;

namespace PWBlazorApplication.Components
{
	public partial class Filter
	{
		[Parameter]
		public FilterModel FilterModel { get; set; }

		[Parameter]
		public EventCallback OnClickCallback { get; set; }
	}
}
