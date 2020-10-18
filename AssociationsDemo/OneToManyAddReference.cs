using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.EF;
using System.Data.Entity;

namespace AssociationsDemo
{
    public class OneToManyAddReference
    {
        public static void UsingAssotiation()
        {
            using (var ctx = new TicketsContext())
            {
                var firstTrain = ctx.Trains.FirstOrDefault();

                var run1 = new Run()
                {
                    Train = firstTrain,
                    Date = DateTime.Now.AddDays(1),
                };
                ctx.Runs.Add(run1);

                ctx.SaveChanges();
            }
        }

        public static void UsingNavigationKey() { 
            using (var ctx = new TicketsContext())
            {
                var firstTrainId = ctx.Trains.Select(x => x.Id).FirstOrDefault();

                var run2 = new Run()
                {
                    TrainId = firstTrainId,
                    Date = DateTime.Now.AddDays(2),
                };
                ctx.Runs.Add(run2);

                ctx.SaveChanges();
            }
        }

        public static void ByAddingToCollectionLazyLoading()
        {
            using (var ctx = new TicketsContext())
            {
                // rely on lazy loading
                var firstTrain = ctx.Trains.FirstOrDefault();

                AddRunToTrain(firstTrain, 3);

                ctx.SaveChanges();
            }
        }


        public static void ByAddingToCollectionEagerLoading() {
            using (var ctx = new TicketsContext())
            {
                // with eager loading, 
                var firstTrain = ctx.Trains
                    .Include(x => x.Runs) //only this line is actually 'needed', others included only for convenience
                    //.Include(x => x.Runs.Select(r => r.Places.Select(p => p.Reservations.Select(res => res.Ticket))))
                    //.Include(x => x.Carriages)
                    //.Include(x => x.Carriages.Select(c => c.Places))
                    .FirstOrDefault();

                AddRunToTrain(firstTrain, 4);

                ctx.SaveChanges();
            }
        }

        private static Run AddRunToTrain(Train train, int withinDays) {
            var run = new Run()
            {
                Train = train,
                Date = DateTime.Now.AddDays(withinDays),
            };
            train.Runs.Add(run);
            return run;
        }
    }
}
