﻿using System;
using System.Collections.Generic;

namespace HastBuyDAL.Models;
public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public int? ParentCategoryId { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }
}
