using Courses_HW_7_8.DB.Models;

namespace Courses_HW_7_8.Models
{
    public class HomeModel
    {
        public HomeModel(List<CostCategories> costCategories)
        {
            CostCategories = costCategories;
        }

        public decimal Result { get; set; }
        public List<CostCategories> CostCategories { get; set; }
    }
}
