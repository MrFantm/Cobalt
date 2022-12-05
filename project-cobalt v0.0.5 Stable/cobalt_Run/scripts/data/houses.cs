using System;

namespace Cobalt.Houses {
    public class House {

        public static string address;
        public static string streetName;
        public static string style;
        public static string state;
        public static string city;
        

        public static int streetNumber;
        public static int cityLength;
        public static int bedrooms;
        public static int bathrooms;

        public static int stNameLength = Cobalt.Addresses.Address.Street.streetName.Length;
        public static int stateLength = Cobalt.Addresses.Address.States.USA.Length;

        public static int num;


        public static void GenerateAddress() {
            Random random = new Random();
            num = random.Next(0, stNameLength);
            streetName = Cobalt.Addresses.Address.Street.streetName[num];

            num = random.Next(0, stateLength);
            state = Cobalt.Addresses.Address.States.USA[num];

            try {
                Cobalt.Addresses.Address.GetCity();
                city = Cobalt.Addresses.Address.cityS;
            }
            catch (Exception exception) {
                Console.WriteLine("An error ocurred: " + exception.Message);
                Cobalt.Main.Program.ConsoleDeploy();
            }

            num = random.Next(500, 9999);
            streetNumber = num;

            address = streetNumber + " " + streetName + "  " + city + ", " + state;
            Cobalt.Main.Program.addressL = address;

            Cobalt.Main.Program.WriteData();
        }
    }
}