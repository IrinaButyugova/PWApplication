using Microsoft.AspNetCore.Components;
using PWComponents.Models;

namespace PWComponents.Components
{
	public partial class PWFilter
	{
		[Parameter]
		public FilterModel FilterModel { get; set; }

		[Parameter]
		public EventCallback OnClickCallback { get; set; }
	}
}
