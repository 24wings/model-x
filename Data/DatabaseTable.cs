using System.ComponentModel.DataAnnotations.Schema;

namespace ModelX.Data
{
    public class DatabaseTable
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        //public string Remark { get; set; }
        public List<DatabaseTableColumn> Columns { get; set; } = new List<DatabaseTableColumn>();
        public string TABLE_COMMENT { get; set; }

    }
    public class DatabaseTableColumn
    {
        //public int Id { get; set; }
        //public int Cid { get; set; }
        //public string Name { get; set; }
        //public bool Notnull { get; set; }
        //public bool Pk { get; set; }

       public string COLUMN_KEY { get; set; }
       public string COLUMN_NAME { get; set; }
       public string COLUMN_TYPE { get; set; }
       public string COLUMN_COMMENT { get; set; }


    }


}
