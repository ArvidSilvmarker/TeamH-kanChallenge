using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V35
{
    public static class TollCalculator
    {
        private const int BASEPRICE_LOW_WEIGHT = 500;
        private const int BASEPRICE_HIGH_WEIGHT = 1000;
        private const int WEIGHT_CUTOFF = 1000;
        private const int BASEPRICE_TRUCK = 2000;
        private const double MODIFIER_MC = 0.7;
        private const double MODIFIER_ECO = 0;
        private const double MODIFIER_LOW_TRAFFIC = 0.5;
        private const double MODIFIER_WEEKEND = 2;

        public static double Calculate(Vehicle v, DateTime dt)
        {
            return CalculateBasePrice(v) * CalculateCoefficient(v, dt);
        }

        private static int CalculateBasePrice(Vehicle v)
        {
            if (v.TypeOfVehicle == Category.Truck)
                return BASEPRICE_TRUCK;
            if (v.Weight < WEIGHT_CUTOFF)
                return BASEPRICE_LOW_WEIGHT;
            else
                return BASEPRICE_HIGH_WEIGHT;
        }

        private static double CalculateCoefficient(Vehicle v, DateTime dt)
        {
            double modifier = 1;
            if (v.TypeOfVehicle == Category.Motorcycle)
                modifier *= MODIFIER_MC;
            if (IsHolidayOrWeekend(dt))
                modifier *= MODIFIER_WEEKEND;
            else if (IsLowTrafic(dt))
                modifier *= MODIFIER_LOW_TRAFFIC;
            if (v.IsEco)
                modifier *= MODIFIER_ECO;
            return modifier;

        }

        private static bool IsHolidayOrWeekend(DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                return true;

            List<DateTime> weekdayHolidays2018Sweden = new List<DateTime>
            {
                new DateTime(2018, 01, 01),
                new DateTime(2018, 03, 30),
                new DateTime(2018, 04, 02),
                new DateTime(2018, 05, 01),
                new DateTime(2018, 05, 10),
                new DateTime(2018, 06, 06),
                new DateTime(2018, 06, 23),
                new DateTime(2018, 12, 25),
                new DateTime(2018, 12, 26)
            };

            if (weekdayHolidays2018Sweden.Any(holiday => holiday.Date == dt.Date))
                return true;

            return false;

        }

        private static bool IsLowTrafic(DateTime dt)
        {
            if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday && ( dt.Hour >= 18 || dt.Hour <= 06))
                return true;
            return false;

        }
    }
}