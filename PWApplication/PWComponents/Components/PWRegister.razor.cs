using Microsoft.AspNetCore.Components.Forms;
using PWComponents.Models;
using Microsoft.AspNetCore.Components;

namespace PWComponents.Components
{
	public partial class PWRegister
	{
        [Parameter]
        public RegisterModel RegisterModel { get; set; }

        [Parameter]
        public IEnumerable<string> Errors { get; set; }

		[Parameter]
        public EventCallback OnCancelClickCallback { get; set; }

        [Parameter]
        public EventCallback<EditContext> OnSubmitCallback { get; set; }
	}
}
