using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? PasswordHash { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PromoUsage> PromoUsages { get; set; } = new List<PromoUsage>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
