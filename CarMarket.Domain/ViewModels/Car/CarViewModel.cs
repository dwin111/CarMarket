﻿using System;
using CarMarket.Domain.Interface;
using Microsoft.AspNetCore.Http;

namespace CarMarket.Domain.ViewModels.Car
{
    public class CarViewModel : ICar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public double Speed { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; }
        public string TypeCar { get; set; }
        public IFormFile Avatar { get; set; }
        public byte[] Image { get; set; }
    }
}
