using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class TransportationType
    {
        public int TransportationTypeId { get; set; }
        [Required]
        public string TransportationTypeName { get; set; }
        public string Description { get; set; }
    }
}
