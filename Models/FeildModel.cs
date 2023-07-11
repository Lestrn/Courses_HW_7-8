using Courses_HW_7_8.DB.Models;
using System.Net;

namespace Courses_HW_7_8.Models
{
    public class FeildModel
    {
        public FeildModel(List<CostCategories> costCategories, List<CostFields> costFields)
        {
            CostCategories = costCategories;
            CostFields = costFields;
        }
        public List<CostCategories> CostCategories { get; set; }
        public List<CostFields> CostFields { get; set; }

    }
}
