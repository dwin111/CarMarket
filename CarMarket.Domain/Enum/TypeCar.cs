﻿
using System.ComponentModel.DataAnnotations;

namespace CarMarket.Domain.Enum
{
    public enum TypeCar
    {
        [Display(Name = "Passenger car")]
        PassengerCar = 0,

        [Display(Name = "Sedan")]
        Sedan = 1,

        [Display(Name = "HatchBack")]
        HatchBack = 2,

        [Display(Name = "Minivan")]
        Minivan = 3,

        [Display(Name = "Sport car")]
        SportCar = 4,

        [Display(Name = "Suv")]
        Suv = 5,
    }
}
