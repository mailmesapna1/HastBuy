using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public int? CategoryId { get; set; }

    public string? Brand { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<InventoryLog> InventoryLogs { get; set; } = new List<InventoryLog>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
