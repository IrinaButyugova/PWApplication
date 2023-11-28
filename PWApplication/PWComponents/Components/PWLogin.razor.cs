using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using PWComponents.Models;

namespace PWComponents.Components
{
	public partial class PWLogin
    {
        [Parameter]
        public LoginModel LoginModel { get; set; }

        [Parameter]
		public IEnumerable<string> Errors { get; set; }

		[Parameter]
		public EventCallback OnCancelClickCallback { get; set; }

		[Parameter]
		public EventCallback<EditContext> OnSubmitCallback { get; set; }
    }
}
