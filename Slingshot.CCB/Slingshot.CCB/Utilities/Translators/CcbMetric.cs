using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Slingshot.Core;
using Slingshot.Core.Model;

namespace Slingshot.CCB.Utilities.Translators
{
    public static class CcbMetric
    {
        public static Metric Translate( XElement inputAttendance, List<EventDetail> eventDetails )
        {
            Metric metric = null;
            var eventId = inputAttendance.Attribute( "id" ).Value.AsInteger();
            var detail = eventDetails.Where( e => e.EventId == eventId ).FirstOrDefault();
            var date = inputAttendance.Element( "occurrence" ).Value.AsDateTime();
            var name = inputAttendance.Element( "name" ).Value;
            var value = inputAttendance.Element( "head_count" ).Value.AsInteger();
            if ( detail != null && date.HasValue && value > 0 )
            {
                metric = new Metric
                {
                    Id = eventId,
                    Title = name,
                    Description = detail.ScheduleName,
                    Date = date.Value, 
                    Value = value,
                    CategoryId = detail.CategoryId,
                    Category = detail.CategoryName,
                };
            }
            
            return metric;
        }
    }
}
