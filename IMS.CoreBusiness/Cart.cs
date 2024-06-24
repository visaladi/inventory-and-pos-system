using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class Cart
    {
        public int CartId { get; set; }
        public string? UserId { get; set; }
        public ICollection<CartItem> Items { get; set; } = new HashSet<CartItem>(); // One-to-Many relationship with CartItem
    }
}
