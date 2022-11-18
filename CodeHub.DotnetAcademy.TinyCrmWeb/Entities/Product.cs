namespace CodeHub.DotnetAcademy.TinyCrmWeb.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Code { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}
