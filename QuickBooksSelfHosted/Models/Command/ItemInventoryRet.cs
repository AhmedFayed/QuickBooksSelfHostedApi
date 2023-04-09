using System;

namespace QuickBooksAPI.Models
{
    public class ItemInventoryAdd
    {
        public string Name { get; set; }

        public decimal SalesPrice { get; set; }

        public ClassRef IncomeAccountRef { get; set; }

        public ClassRef COGSAccountRef { get; set; }

        public ClassRef AssetAccountRef { get; set; }
    }
}