using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class PlantingMetadata
    {
        [Display(Name = "Phân loại cây trồng")]
        public Taxonomy Taxonomy { get; set; } = new();

        [Display(Name = "Ngày trồng")]
        public DateTime PlantingDate { get; set; }

    }
}
