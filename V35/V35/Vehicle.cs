using System;
using System.Collections.Generic;
using System.Text;

namespace V35
{
    public enum Category
    {
        Motorcycle, Car, Truck
    }
    public class Vehicle
    {
        public double Weight { get; set; }
        public bool IsEco { get; set; }

        public Category TypeOfVehicle { get; set; }

        public Vehicle(double weight, bool isEco, Category typeOfVehicle)
        {
            Weight = weight;
            IsEco = isEco;
            TypeOfVehicle = typeOfVehicle;
        }
    }
}
