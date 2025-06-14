using BusinessLayer.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models;

public class AddGiftCardViewModel
{
    public decimal Discount { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int CouponCodeCount { get; set; } = 0;
}
