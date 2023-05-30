using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ScopoERP.ViewModels;
public class BaseViewModel
{
    public string ModifiedBy { get; set; } = "Admin";
    public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
    public bool? IsActive { get; set; } = true;
    public int Status { get; set; } = 0;
    //public BaseViewModel()
    //{
       
        
    //}
}
