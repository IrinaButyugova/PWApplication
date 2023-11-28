using Microsoft.AspNetCore.Components;

namespace PWComponents.Components
{
    public partial class PWValidationErrors
    {
        [Parameter]
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
