using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.EF;

namespace AssociationsDemo
{
    public class ReservationEfficiencyDTO {
        public DateTime Date { get; set; }
        public bool HasTicket { get; set; }
        public int Place { get; set; }
        public int Carriage { get; set; }
    }

    public class SelectSpecializedDataSet
    {
        public static List<ReservationEfficiencyDTO> GetAllReservationsWithTicketsForTrain(int trainId, DateTime from, DateTime to) {
            using (var ticketsContext = new TicketsContext()) {
                return ticketsContext.Reservations.Where(r => r.PlaceInRun.Run.TrainId == trainId && r.Start > from && r.End < to)
                    .Select(reservation => new ReservationEfficiencyDTO() { 
                        Date = reservation.Start,
                        HasTicket = reservation.TicketId.HasValue,
                        Place = reservation.PlaceInRun.Number,
                        Carriage = reservation.PlaceInRun.CarriageNumber,
                    }).ToList();
            }
        }
    }
}
