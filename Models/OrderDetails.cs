using System.Net.Http.Headers;

namespace Project_WebDev.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public Item Item { get; set; } = null!;
    }
}
