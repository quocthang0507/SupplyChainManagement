using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Transportation
    {
        public int TransportationId { get; set; }
        [Display(Name = "Shipment Number")]
        public string TransportationName { get; set; }
        [Display(Name = "Sales Order")]
        public DateTimeOffset TransportationDate { get; set; }
        [Display(Name = "Shipment Type")]
        public int TransportationTypeId { get; set; }
        [Display(Name = "Warehouse")]
        public int WarehouseId { get; set; }
        [Display(Name = "Full Shipment")]
        public bool IsFullShipment { get; set; } = true;
    }
}
