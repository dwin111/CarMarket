using System;
using CarMarket.Domain.Interface;


namespace CarMarket.Domain.ViewModels.Car
{
    public class CarViewModel : ICar
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public double Speed { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; }
        public string TypeCar { get; set; }
    }
}
