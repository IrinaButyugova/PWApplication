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

		private async Task Clear()
		{
			FilterModel.StartDate = null;
			FilterModel.EndDate = null;
			FilterModel.CorrespondentName = String.Empty;
			FilterModel.StartAmount = null;
			FilterModel.EndAmount = null;
			await OnClickCallback.InvokeAsync();
		}
	}
}
