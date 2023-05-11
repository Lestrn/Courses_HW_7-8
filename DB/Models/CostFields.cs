using Courses_HW_7_8.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Courses_HW_7_8.DB.Models
{
    public class CostFields : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public CostCategories Category { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [StringLength(100)]
        public string? Description { get; set; }
        
       
    }
}
