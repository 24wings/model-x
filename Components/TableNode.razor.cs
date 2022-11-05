using Microsoft.AspNetCore.Components;
using static MudBlazor.CategoryTypes;

namespace ModelX.Components
{

    public partial class TableNode
    {
        [Parameter]
        public Table Node { get; set; }
    }
}
