using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.EF;
using System.Data.Entity;
using TicketsDemo.Data.Entities;

namespace AssociationsDemo
{
    public class AccessNavigationPropertiesInLoop
    {
        public static void ListPlacesForAllTrainsLazyLoading()
        {
            using (var ctx = new TicketsContext())
            {
                foreach (var train in ctx.Trains) {
                    Console.WriteLine("Inspecting train {0}: {1}-{2}", train.Number, train.StartLocation, train.EndLocation);

                    foreach (var carriage in train.Carriages)
                    {
                        Console.WriteLine("Enter carriage {0}", train.Number, train.StartLocation, train.EndLocation);

                        foreach (var place in carriage.Places) {
                            Console.WriteLine("Place {0} price rate {1}", place.Number, place.PriceMultiplier);
                        }
                    }
                }
            }
        }

        public static void ListPlacesForAllTrainsEagerLoading()
        {
            using (var ctx = new TicketsContext())
            {
                foreach (var train in ctx.Trains.Include(x => x.Carriages.Select(carr => carr.Places)))
                {
                    Console.WriteLine("Inspecting train {0}: {1}-{2}", train.Number, train.StartLocation, train.EndLocation);

                    foreach (var carriage in train.Carriages)
                    {
                        Console.WriteLine("Enter carriage {0}", train.Number, train.StartLocation, train.EndLocation);

                        foreach (var place in carriage.Places)
                        {
                            Console.WriteLine("Place {0} price rate {1}", place.Number, place.PriceMultiplier);
                        }
                    }
                }
            }
        }

        public static void ListPlacesForAllTrainsLazyLostContext() {
            var trains = new List<Train>();

            using (var ctx = new TicketsContext())
            {
                trains = ctx.Trains.ToList();
            }

            trains.ForEach(InspectTrain);
        }

        private static void InspectTrain(Train train) {
            Console.WriteLine("Inspecting train {0}: {1}-{2}", train.Number, train.StartLocation, train.EndLocation);

            foreach (var carriage in train.Carriages)
            {
                Console.WriteLine("Enter carriage {0}", train.Number, train.StartLocation, train.EndLocation);

                foreach (var place in carriage.Places)
                {
                    Console.WriteLine("Place {0} price rate {1}", place.Number, place.PriceMultiplier);
                }
            }
        }
    }
}
