using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using V35;

namespace UnitTestCoreV35
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRule1and5()
        {
            // 1: Om fordonet v�ger �ver 1000 kg s� ska priset vara 1000 SEK
            // 5: Priset f�r regel 1 och 2 g�ller om fordonet �r en personbil.

            Assert.AreEqual(1000, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(1200, false, Category.Car), new DateTime(2018,04,06,12,00,00))); 
        }


        [TestMethod]
        public void TestRule2and5()
        {
            // 2: Om fordonet v�ger under 1000 kg s� ska priset vara 500 SEK
            // 5: Priset f�r regel 1 och 2 g�ller om fordonet �r en personbil.

            Assert.AreEqual(500, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(800, false, Category.Car), new DateTime(2018, 04, 06, 12, 00, 00)));
        }

        [TestMethod]
        public void TestRule3()
        {
            // 3: Om fordonet k�r igenom tullen efter klockan 18:00 och innan 06:00 s� ska priset vara det
            //     ordinarie priset minus 50 %.Detta g�ller endast veckodagar.

            Assert.AreEqual(500, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(1200, false, Category.Car), new DateTime(2018, 04, 06, 19, 00, 00)));
        }

        [TestMethod]
        public void TestRule4()
        {
            // 4: Om fordonet �r en Lastbil g�ller inte regel 1 eller 2 utan d� �r det ett standardpris p� 2000 SEK.

            Assert.AreEqual(2000, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(1200, false, Category.Truck), new DateTime(2018, 04, 06, 12, 00, 00)));
        }

        [TestMethod]
        public void TestRule1and6()
        {
            // 1: Om fordonet v�ger �ver 1000 kg s� ska priset vara 1000 SEK
            // 6: Om fordonet �r en motorcykel �r det priset f�r regel 1 och 2, minus 30%.     

            Assert.AreEqual(700, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(1200, false, Category.Motorcycle), new DateTime(2018, 04, 06, 12, 00, 00)));
        }

        [TestMethod]
        public void TestRule2and6()
        {
            // 2: Om fordonet v�ger under 1000 kg s� ska priset vara 500 SEK
            // 6: Om fordonet �r en motorcykel �r det priset f�r regel 1 och 2, minus 30%.     

            Assert.AreEqual(350, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(800, false, Category.Motorcycle), new DateTime(2018, 04, 06, 12, 00, 00)));
        }

        [TestMethod]
        public void TestRule7weekend()
        {
            // 7: Priserna 1 - 6 g�ller endast veckordagar. Alla helger plus h�gtider vill gr�nsvakterna ha dubbelt
            // betalt och d�rmed �kar priset med 100 %.

            Assert.AreEqual(2000, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(1200, false, Category.Car), new DateTime(2018, 04, 07, 12, 00, 00)));
        }

        [TestMethod]
        public void TestRule7weekdayholiday()
        {
            // 7: Priserna 1 - 6 g�ller endast veckordagar. Alla helger plus h�gtider vill gr�nsvakterna ha dubbelt
            // betalt och d�rmed �kar priset med 100 %.

            Assert.AreEqual(700, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(800, false, Category.Motorcycle), new DateTime(2018, 05, 01, 12, 00, 00)));
        }

        [TestMethod]
        public void TestRule8()
        {
            // 8: Om fordonet �r en milj�bil vilket b�de lastbil, motorcykel och personbil kan vara s� �r kostnaden
            // att passera 0 SEK till H�kanHellstr�m - Landet

            Assert.AreEqual(0, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(1200, true, Category.Car), new DateTime(2018, 04, 06, 12, 00, 00)));
        }

        [TestMethod]
        public void TestRule3and7()
        {
            // 3: Om fordonet k�r igenom tullen efter klockan 18:00 och innan 06:00 s� ska priset vara det
            //     ordinarie priset minus 50 %.Detta g�ller endast veckodagar.
            // 7: Priserna 1 - 6 g�ller endast veckordagar.Alla helger plus h�gtider vill gr�nsvakterna ha dubbelt
            // betalt och d�rmed �kar priset med 100 %.
            
            // Oklart exakt hur reglerna interagerar. Just nu antar jag att 7 har prioritet �ver 3.

            Assert.AreEqual(2000, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(1200, false, Category.Car), new DateTime(2018, 05, 01, 20, 00, 00)));
        }

        [TestMethod]
        public void TestExactly1000Kg()
        {
            // Exakt 1000 kg definieras inte av kraven. Just antar jag att det �vre priset �r >= 1000 kg

            Assert.AreEqual(1000, V35.TollCalculator.CalculateWithRuleNames(new V35.Vehicle(1000, false, Category.Car), new DateTime(2018, 04, 06, 12, 00, 00)));
        }

    }
}
