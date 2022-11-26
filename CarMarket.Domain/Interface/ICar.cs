
namespace CarMarket.Domain.Interface
{
    public interface ICar
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Model { get; set; }

        public double Speed { get; set; }

        public decimal Price { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
