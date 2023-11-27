using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace PWComponents.Components
{
    public partial class PWButton
    {
        [Parameter]
        public string Title { get; set; } = "";
        [Parameter]
        public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
    }
}
