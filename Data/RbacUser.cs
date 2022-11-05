using System.ComponentModel.DataAnnotations.Schema;

namespace ModelX.Data
{
    [Table("rbac_user")]
    public class RbacUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
