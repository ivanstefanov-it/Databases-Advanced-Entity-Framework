using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniORM.App.Data.Entities
{
    public class EmployeeProjects
    {
        [Key]
        [ForeignKey(nameof(Employees))]
        public int EmployeeId { get; set; }

        [Key]
        [ForeignKey(nameof(Projects))]
        public int ProjectId { get; set; }

        public Employees Employees { get; set; }

        public Projects Projects { get; set; }
    }
}