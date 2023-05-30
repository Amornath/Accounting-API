using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScopoERP.Models;
using System.Reflection.Emit;

namespace ScopoERP.Helpers
{
    public static class SeedData
    {
        public static void SeedDBData(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Administrator", Id = "1", NormalizedName = "ADMINISTRATOR".Normalize(), ConcurrencyStamp = Guid.NewGuid().ToString() }
            );
        //    builder.Entity<Module>().HasData(
        //    new Module() {  Name = "Sales_Order" },
        //    new Module() { Name = "Sales_Order_Return"}
        //    new Module { Id = 3, Name = "Delivery_Challan" },
        //     new Module { Id = 4, Name = "Invoice" },
        //      new Module { Id = 5, Name = "Requisition" },
        //    new Module { Id = 6, Name = "Purchase_Order" },
        //    new Module { Id = 7, Name = "Purchase_Order_Return" },
        //     new Module { Id = 8, Name = "Billing" },
        //    new Module { Id = 9, Name = "Whole_Store_Module" },
        //     new Module { Id = 10, Name = "Inventory_Receive" },
        //    new Module { Id = 11, Name = "Inventory_Issue" },
        //    new Module { Id = 12, Name = "Store_Transfer" },
        //     new Module { Id = 13, Name = "Whole_Production_Module" },
        //    new Module { Id = 14, Name = "Lab_Test" },
        //    new Module { Id = 15, Name = "Formulation" },
        //     new Module { Id = 16, Name = "Product_Batch" },
        //    new Module { Id = 17, Name = "Whole_Hr_Module" }
        //);
        }
    }
}
