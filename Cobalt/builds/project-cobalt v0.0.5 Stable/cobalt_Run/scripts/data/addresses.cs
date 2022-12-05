using System;
using System.IO;

namespace Cobalt.Addresses {

    //This is just to store data for the moment. All data present here will be migrated over to .XML files in the future.
    public class Address {

        public static string cityS;
        public static string rootDir = Main.Program.rootDir;

        public static void GetCity() {
            rootDir = Main.Program.rootDir;
            Random randomNum = new Random();
            string stateNameL = Cobalt.Houses.House.state;
            string addressDir = rootDir + @"\cobalt_Run\data\addresses\" + stateNameL + ".cbstate";

            string[] citiesL = new string[1000];

            int num = 0;
            foreach(string cityR in File.ReadLines(addressDir)) {
                citiesL[num] = cityR; 
                num++;
            }

            

            int num2 = randomNum.Next(0, num);
            cityS = citiesL[num2];
        }


        public class Street {
            public static string[] streetName = {
                "Bliss Street",
                "Java Boulevard",
                "Law Way",
                "Wright Route",
                "Ash Street",
                "Clearance Avenue",
                "Lawn Avenue",
                "Lawn Avenue",
                "Flax Passage",
                "Jade Route",
                "Bay View Way",
                "Lavender Passage",
                "Broadway", 
                "Main Street", 
                "Canal Street", 
                "Fifth Avenue", 
                "Park Avenue", 
                "Madison Avenue", 
                "Lexington Avenue", 
                "Third Avenue", 
                "Second Avenue", 
                "First Avenue", 
                "Central Park West", 
                "Fifth Avenue South", 
                "Fifth Avenue North", 
                "Park Avenue South", 
                "Park Avenue North", 
                "Madison Avenue South", 
                "Madison Avenue North", 
                "Lexington Avenue South", 
                "Lexington Avenue North", 
                "Third Avenue South", 
                "Third Avenue North", 
                "Second Avenue South", 
                "Second Avenue North", 
                "First Avenue South",
                "First Avenue North",
                "Elm Street", 
                "Oak Street", 
                "Maple Street", 
                "Cherry Street", 
                "Pine Street", 
                "Birch Street", 
                "Willow Street", 
                "Hickory Street", 
                "Beech Street", 
                "Chestnut Street", 
                "Locust Street", 
                "Sycamore Street", 
                "Dogwood Street", 
                "Walnut Street", 
                "Poplar Street", 
                "Cedar Street", 
                "Fir Street", 
                "Spruce Street", 
                "Hemlock Street", 
                "Pecan Street", 
                "Persimmon Street", 
                "Gum Street", 
                "Butternut Street", 
                "Basswood Street", 
                "Ash Street", 
                "Sassafras Street"
            };
        }

        public static class States {
            public static string[] USA = {
                "AL", 
                "AK",
                "AZ", 
                "AR", 
                "CA", 
                "CO", 
                "CT", 
                "DE", 
                "FL", 
                "GA", 
                "HI", 
                "ID", 
                "IL", 
                "IN", 
                "IA", 
                "KS", 
                "KY", 
                "LA", 
                "ME", 
                "MD", 
                "MA"
                // "MI", 
                // "MN", 
                // "MS", 
                // "MO", 
                // "MT", 
                // "NE", 
                // "NV", 
                // "NH", 
                // "NJ", 
                // "NM", 
                // "NY", 
                // "NC", 
                // "ND", 
                // "OH", 
                // "OK", 
                // "OR", 
                // "PA", 
                // "RI", 
                // "SC", 
                // "SD", 
                // "TN", 
                // "TX", 
                // "UT", 
                // "VT", 
                // "VA", 
                // "WA", 
                // "WV", 
                // "WI", 
                // "WY"
            };
        }
      
        class MI {
            public static string[] cities = {
                
            };
        }

        class MN {
            public static string[] cities = {
                
            };
        }

        class MS {
            public static string[] cities = {
                
            };
        }

        class MO {
            public static string[] cities = {
                
            };
        }

        class MT {
            public static string[] cities = {
                
            };
        }
    }
}