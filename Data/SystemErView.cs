using System.ComponentModel.DataAnnotations.Schema;

namespace ModelX.Data
{

    [Table("system_er_view")]

    public class SystemErView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int AppId { get; set; }
        [NotMapped]
        public List<SystemErViewRelation> SystemErViewRelations { get; set; }= new List<SystemErViewRelation>();

    }
    [Table("system_er_view_relation")]

    public class SystemErViewRelation
    {
        public int Id { get; set; }
        public string MasterTableName { get; set; }
        public string MasterTableColumnName { get; set; }
        public string SubTableName { get; set; }
        public string SubTableColumnName { get; set; }

        public RelationType RelationType { get; set; } = RelationType.OneToOne;
        public int SystemErViewId { get; set; }

    }

    public enum RelationType
    {
        OneToOne = 0,
        OneToMany = 1,
    }
}
