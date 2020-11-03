using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.XML
{
    class XMLSettingsService
    {
        
        public string HolidaysXMLPath
        {
            get
            {
                return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\XmlHolidayRepository.xml";
            }
        }
        public string PlacesXMLPath
        {
            get
            {
                return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\XMLRepositoryPlaces.xml";
            }
        }

        public string RepXMLPath
        {
            get
            {
                return $@"{AppDomain.CurrentDomain.BaseDirectory}\App_Data\XMLRepository.xml";
            }
        }
    }
}
