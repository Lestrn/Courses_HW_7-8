using Courses_HW_7_8.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Courses_HW_7_8.DB.Models
{
    public class CostCategories : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
