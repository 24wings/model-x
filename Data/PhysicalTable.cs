namespace ModelX.Data
{
    /// <summary>
    /// 物理表
    /// </summary>
    public class PhysicalTable
    {
        public string Name { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock { get; set; }

        public List<TableColumn> Columns { get; set; } = new List<TableColumn>();
    }

    public class TableColumn
    {
        public  string Name { get; set; }
        public string DataType { get; set; }
        public int Length { get; set; }
        public bool IsPrimaryKey { get; set; }
    }
}
