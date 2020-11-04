using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.XML;


namespace TicketsDemo.Domain.DefaultImplementations
{
    public class WeekendsAndHolidaysPriceCalculationStrategy : IPriceCalculationStrategy
    {
        private IHolidayRepository _holidayRepository;

        private IPriceCalculationStrategy _calculationStrategy;
        public WeekendsAndHolidaysPriceCalculationStrategy(IPriceCalculationStrategy strategy, IHolidayRepository holidayRepository)
        {
            _calculationStrategy = strategy;
            _holidayRepository = holidayRepository;
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var components = new List<PriceComponent>();
            var priceComponents = _calculationStrategy.CalculatePrice(placeInRun);
            var runDate = placeInRun.Run.Date;

            foreach (Holiday holiday in _holidayRepository.GetHolidaysList())
            {
                if (runDate.Day == holiday.Date.Day && runDate.Month == holiday.Date.Month && runDate.Year == holiday.Date.Year)
                {
                    var value = priceComponents.Select(x => x.Value * holiday.Markup).Sum();

                    var HolidayComponent = new PriceComponent()
                    {
                        Name = $"Holiday service tax for {holiday.Name}",
                        Value = value
                    };
                    components.Add(HolidayComponent);
                    return components;
                }

            }
            if (runDate.DayOfWeek == DayOfWeek.Saturday || runDate.DayOfWeek == DayOfWeek.Sunday)
            {
                var value = priceComponents.Select(x => x.Value * 0.3m).Sum();
                var WeekendComponent = new PriceComponent()
                {
                    Name = "Weekend service tax",
                    Value = value
                };
                components.Add(WeekendComponent);
            }
            else
            {
                components = null;
            }

            return components;
        }
    }
}