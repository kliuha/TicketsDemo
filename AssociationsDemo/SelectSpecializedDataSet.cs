using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.EF;

namespace AssociationsDemo
{
    public class ReservationEfficiencyDTO {
        public DateTime Date { get; set; }
        public bool HasTicket { get; set; }
        public int Place { get; set; }
        public int Carriage { get; set; }

        public List<string> PriceComponentsList { get; set; }
    }

    public static class SelectSpecializedDataSet
    {
        public static List<ReservationEfficiencyDTO> GetAllReservationsWithTicketsForTrain(int trainId, DateTime from, DateTime to) {
            using (var ticketsContext = new TicketsContext()) {
                var searchQuery = ticketsContext.Reservations.Where(r => r.PlaceInRun.Run.TrainId == trainId && r.Start > from && r.End < to);
                
                /*
                 * uncomment to see the querry formed to get the presentation with placeInRun objects
                var fullEntityesQuery = searchQuery
                    .Include(x => x.PlaceInRun);*/

                var limitedFieldsQuery = searchQuery.Select(reservation => new ReservationEfficiencyDTO()
                {
                    Date = reservation.Start,
                    Place = reservation.PlaceInRun.Number,
                    Carriage = reservation.PlaceInRun.CarriageNumber,
                });

                
                var limitedFieldsQueryWithTranslatedHasValue = searchQuery.Select(reservation => new ReservationEfficiencyDTO() {
                    Date = reservation.Start,
                    HasTicket = reservation.TicketId.HasValue,
                    Place = reservation.PlaceInRun.Number,
                    Carriage = reservation.PlaceInRun.CarriageNumber,
                    PriceComponentsList = reservation.Ticket.PriceComponents.Select(x => x.Name).ToList(),
                });

                /*
                // uncomment to see an error caused by function CheckIfTicketIdExists not being known to linq to entities
                var limitedFieldsQueryWithCustomFunction = searchQuery.Select(reservation => new ReservationEfficiencyDTO()
                {
                    Date = reservation.Start,
                    HasTicket = CheckIfTicketIdExists(reservation),
                    Place = reservation.PlaceInRun.Number,
                    Carriage = reservation.PlaceInRun.CarriageNumber,
                });*/

                return limitedFieldsQueryWithTranslatedHasValue.ToList();
            }
        }

        private static bool CheckIfTicketIdExists(Reservation reservation) {
            return reservation.TicketId.HasValue;
        }
    }
}
