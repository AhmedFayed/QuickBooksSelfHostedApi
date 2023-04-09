namespace QuickBooksSelfHostedApi.Models
{
    public class CustomerAdd
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string CompanyName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        Address BillAddress { get; set; }

        Address ShipAddress { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Cc { get; set; }

        public string Contact { get; set; }
    }
}