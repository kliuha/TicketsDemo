using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.EF;

namespace AssociationsDemo
{
    public static class ChangeTrackerSamples
    {
        public static void ChangeAndRevertTrainNumberSample() {
            using (var ctx = new TicketsContext())
            {
                var trains = ctx.Trains.ToList();

                var originalTrainNumber = trains[0].Number;
                var firstTrainEntry = ctx.Entry(trains[0]);

                Debug.WriteLine("Current state of first train is {0}", firstTrainEntry.State);

                // change train number to some random value
                trains[0].Number = 1001;

                Debug.WriteLine("State of first train after changing it's number {0}", firstTrainEntry.State);
                Debug.WriteLine("State of number property of first train train after changing it's number {0}", firstTrainEntry.Property(x => x.Number).IsModified);

                // firstTrainEntry = ctx.Entry(trains[0]); -- uncommet to show automatic changes tracking in action

                // change train number back to some another value
                trains[0].Number = originalTrainNumber;

                Debug.WriteLine("State of first train after reverting number is {0}", ctx.Entry(trains[0]).State);
                Debug.WriteLine("State of number property after revertings train's number {0}", ctx.Entry(trains[0]).Property(x => x.Number).IsModified);


                trains[0].Number = originalTrainNumber;

                ctx.SaveChanges();
            }
        }
    }
}
