namespace CodeHub.DotnetAcademy.TinyCrmWeb.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string VatNumber { get; set; } = default!;

        public string Address { get; set; } = default!;
    }
}
