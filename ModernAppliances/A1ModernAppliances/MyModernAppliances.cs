using ModernAppliances.Entities;
using ModernAppliances.Entities.Abstract;
using ModernAppliances.Helpers;
using System;
using System.Collections.Generic;

namespace ModernAppliances
{
    /// <summary>
    /// Manager class for Modern Appliances
    /// </summary>
    internal class MyModernAppliances : ModernAppliances
    {
        /// <summary>
        /// Option 1: Performs a checkout
        /// </summary>
        public override void Checkout()
        {
            Console.WriteLine("Enter the item number of an appliance: ");
            if (!long.TryParse(Console.ReadLine(), out long itemNumber))
            {
                Console.WriteLine("Invalid item number format.");
                return;
            }

            Appliance? foundAppliance = null;

            foreach (var appliance in Appliances)
            {
                if (appliance.ItemNumber == itemNumber)
                {
                    foundAppliance = appliance;
                    break;
                }
            }

            if (foundAppliance == null)
            {
                Console.WriteLine("No appliances found with that item number.");
                Console.WriteLine("");
                return;
            }

            if (foundAppliance.IsAvailable)
            {
                foundAppliance.Checkout();
                Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("The appliance is not available to be checked out.");
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Option 2: Finds appliances by brand
        /// </summary>
        public override void Find()
        {
            Console.WriteLine("Enter brand to search for:");
            string brand = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(brand))
            {
                Console.WriteLine("Brand cannot be empty.");
                return;
            }

            var found = new List<Appliance>();

            foreach (var appliance in Appliances)
            {
                if (appliance.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase))
                {
                    found.Add(appliance);
                }
            }

            if (found.Count == 0)
            {
                Console.WriteLine("No appliances found for that brand.");
                Console.WriteLine("");
                return;
            }

            Console.WriteLine("Matching Appliances:");
            DisplayAppliancesFromList(found, 0);

        }

        /// <summary>
        /// Displays appliances of type Refrigerators
        /// </summary>
        public override void DisplayRefrigerators()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("2 - Double doors");
            Console.WriteLine("3 - Three doors");
            Console.WriteLine("4 - Four doors");
            Console.Write("Enter number of doors: 2 (double door), 3 (three doors) or 4 (four doors):\n ");

            if (!int.TryParse(Console.ReadLine(), out int numberOfDoors))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var found = new List<Appliance>();

            foreach (var appliance in Appliances)
            {
                if (appliance is Refrigerator refrigerator)
                {
                    if (numberOfDoors == 0 || refrigerator.Doors == numberOfDoors)
                    {
                        found.Add(appliance);
                    }
                }
            }

            DisplayAppliancesFromList(found, 0);
        }


        /// <summary>
        /// Displays appliances of type Vacuums
        /// </summary>
        public override void DisplayVacuums()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Residential");
            Console.WriteLine("2 - Commercial");
            Console.Write("Enter grade: ");
            string gradeInput = Console.ReadLine();
            string grade = gradeInput switch
            {
                "0" => "Any",
                "1" => "Residential",
                "2" => "Commercial",
                _ => null
            };

            if (grade == null)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Low");
            Console.WriteLine("2 - High");
            Console.Write("Enter battery voltage value: ");
            string voltageInput = Console.ReadLine();
            int voltage = voltageInput switch
            {
                "0" => 0,
                "1" => 18,
                "2" => 24,
                _ => -1
            };

            if (voltage == -1)
            {
                Console.WriteLine("Invalid voltage option.");
                return;
            }

            var found = new List<Appliance>();

            foreach (var appliance in Appliances)
            {
                if (appliance is Vacuum vacuum)
                {
                    if ((grade == "Any" || vacuum.Grade == grade) && (voltage == 0 || vacuum.BatteryVoltage == voltage))
                    {
                        found.Add(appliance);
                    }
                }
            }

            Console.WriteLine("Matching vacuums:");
            DisplayAppliancesFromList(found,0);
        }

        /// <summary>
        /// Displays appliances of type Microwaves
        /// </summary>
        public override void DisplayMicrowaves()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Kitchen");
            Console.WriteLine("2 - Work site");
            Console.Write("Room where the microwave will be installed: K (kitchen) or W (work site): ");

            string roomInput = Console.ReadLine();
            char roomType = roomInput switch
            {
                "0" => 'A',
                "1" => 'K',
                "2" => 'W',
                _ => '\0'
            };

            if (roomType == '\0')
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            var found = new List<Appliance>();

            foreach (var appliance in Appliances)
            {
                if (appliance is Microwave microwave && (roomType == 'A' || microwave.RoomType == roomType))
                {
                    found.Add(appliance);
                }
            }

            Console.WriteLine("Matching microwaves:");
            DisplayAppliancesFromList(found,0);
        }

        /// <summary>
        /// Displays appliances of type Dishwashers
        /// </summary>
        public override void DisplayDishwashers()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Quietest");
            Console.WriteLine("2 - Quieter");
            Console.WriteLine("3 - Quiet");
            Console.WriteLine("4 - Moderate");
            Console.Write("Enter the sound rating of the dishwasher: ");

            string soundInput = Console.ReadLine();
            string soundRating = soundInput switch
            {
                "0" => "Any",
                "1" => "Qt",
                "2" => "Qr",
                "3" => "Qu",
                "4" => "M",
                _ => null
            };

            if (soundRating == null)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            var found = new List<Appliance>();

            foreach (var appliance in Appliances)
            {
                if (appliance is Dishwasher dishwasher && (soundRating == "Any" || dishwasher.SoundRating == soundRating))
                {
                    found.Add(appliance);
                }
            }

            Console.WriteLine("Matching dishwashers:");
            DisplayAppliancesFromList(found,0);
        }

        /// <summary>
        /// Produces a random list of appliances
        /// </summary>
        public override void RandomList()
        {
            Console.WriteLine("Appliance Types:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Refrigerators");
            Console.WriteLine("2 - Vacuums");
            Console.WriteLine("3 - Microwaves");
            Console.WriteLine("4 - Dishwashers");
            Console.Write("Enter type of appliance: ");

            if (!int.TryParse(Console.ReadLine(), out int applianceType))
            {
                Console.WriteLine("Invalid appliance type.");
                return;
            }

            Console.Write("Enter number of appliances: ");
            if (!int.TryParse(Console.ReadLine(), out int numAppliances) || numAppliances <= 0)
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            var found = new List<Appliance>();

            foreach (var appliance in Appliances)
            {
                if (applianceType == 0 || (applianceType == 1 && appliance is Refrigerator) ||
                    (applianceType == 2 && appliance is Vacuum) || (applianceType == 3 && appliance is Microwave) ||
                    (applianceType == 4 && appliance is Dishwasher))
                {
                    found.Add(appliance);
                }
            }

            found.Sort(new RandomComparer());

            Console.WriteLine("Random appliances:");
            DisplayAppliancesFromList(found, numAppliances);
        }
    }
}


