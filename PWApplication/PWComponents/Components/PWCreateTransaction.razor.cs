using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using PWComponents.Models;

namespace PWComponents.Components
{
	public partial class PWCreateTransaction
	{
		[Parameter]
		public IEnumerable<string> Users { get; set; }
		[Parameter]
		public CreateTransactionModel CreateModel { get; set; }
		[Parameter]
        public IEnumerable<string> Errors { get; set; }
		[Parameter]
		public EventCallback<EditContext> OnSubmitCallback { get; set; }
	}
}
