namespace ModelX.Data
{
    public class Apps
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<DatabaseTable> DatabaseTables { get; set; } = new List<DatabaseTable>();
    }
}
