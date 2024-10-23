using DotNetWeb.Models.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetWeb.Models
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("Category Name")]
        [MaxLength(30)]
        [UniqueCategoryName]
        public required string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100)]
        public required int DisplayOrder { get; set; }
    }
}
