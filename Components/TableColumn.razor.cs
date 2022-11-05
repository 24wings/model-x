using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Data.Common;
using System.Text.Json.Serialization;
using static MudBlazor.CategoryTypes;

namespace ModelX.Components
{
    public class ColumnPort : PortModel
    {
        public ColumnPort(NodeModel parent, Column column, PortAlignment alignment = PortAlignment.Bottom)
            : base(parent, alignment, null, null)
        {
            Column = column;
        }



        [JsonIgnore]
        public Column Column { get; }

        public override bool CanAttachTo(PortModel port)
        {
            // Avoid attaching to self port/node
            if (!base.CanAttachTo(port))
                return false;

            var targetPort = port as ColumnPort;
            var targetColumn = targetPort.Column;

            if (Column.Type != targetColumn.Type)
                return false;

            if (Column.Primary && targetColumn.Primary)
                return false;

            if (Column.Primary && targetPort.Links.Count > 0 ||
                targetColumn.Primary && Links.Count > 1) // Ongoing link
                return false;

            return true;
        }
    }
    public class Column
    {
        public event Action Changed;

        public string Name { get; set; }
        public ColumnType Type { get; set; }
        public bool Primary { get; set; }
        public string Remark { get; set; } = "备注";
        public void Refresh() => Changed?.Invoke();
        public string DataType { get; set; }
    }

    public enum ColumnType
    {
        Boolean,
        Char,
        String,
        SByte,
        Short,
        Integer,
        Long
    }
    public class Table : NodeModel
    {
        public string Comment { get; set; }
        public Table(List<Column> Columns,Point position = null) : base(position, RenderLayer.HTML)
        {
            this.Columns = Columns;
            foreach(var column in Columns)
            {
                if (column.Primary)
                {
                    AddPort(column, PortAlignment.Right);
                }
                else
                {
                    AddPort(column, PortAlignment.Left);
                }
            }

            //AddPort(Columns[0], PortAlignment.Right);
            //AddPort(Columns[1], PortAlignment.Left);
        }

        public string Name { get; set; } = "Table";
        public List<Column> Columns { get; set; }
        public bool HasPrimaryColumn => Columns.Any(c => c.Primary);

        public ColumnPort GetPort(Column column) => Ports.Cast<ColumnPort>().FirstOrDefault(p => p.Column == column);

        public void AddPort(Column column, PortAlignment alignment) => AddPort(new ColumnPort(this, column, alignment));
    }
    public partial class TableColumn : IDisposable
    {
        private bool _shouldRender = true;

        [Parameter]
        public Table Table { get; set; }

        [Parameter]
        public Column Column { get; set; }

        public bool HasLinks => Table.GetPort(Column)?.Links.Count > 0;

        public void Dispose()
        {
            Column.Changed -= ReRender;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Column.Changed += ReRender;
        }

        protected override bool ShouldRender() => _shouldRender;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _shouldRender = false;
            await base.OnAfterRenderAsync(firstRender);
        }

        private void ReRender()
        {
            _shouldRender = true;
            StateHasChanged();
        }
    }
}
