﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetWeb.Models
{
  public class ProductImage
  {
    public int Id { get; set; }

    [Required]
    public string ImageUrl { get; set; }
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [ValidateNever]
    public Product Product { get; set; }
  }
}
