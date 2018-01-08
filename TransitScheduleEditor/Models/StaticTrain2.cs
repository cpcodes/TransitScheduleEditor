using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransitScheduleEditor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class StaticTrain
    {
        public enum DayType
        {
            Weekday,
            Weekend,
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }

        public enum TrainType
        {
            Amtrak,
            Coaster,
            Metrolink,
            Sprinter
        }

        public enum DirectionType
        {
            Escondido,
            [Display(Name="North Bound")] NorthBound,
            [Display(Name="San Diego")] SanDiego,
            [Display(Name="South Bound")] SouthBound
        }

        public string Departure
        {
            get { return minutesToTime(DepartTime); }
            set {
                string result = timeToMinutes(value);
                if (result == "") throw new ArgumentOutOfRangeException("Departure", "The value provided does not map to a recognized time format"); // Only save the new value if it is valid
                else
                {
                    DepartTime = result;
                }
            }
        }

        public static string timeToMinutes(string timeString)
        {
            if (!DateTime.TryParse(timeString, out DateTime timeValue)) return "";
            DateTime midnight = DateTime.Parse("00:00");
            return Math.Round((timeValue - midnight).TotalMinutes).ToString();
        }

        public static string minutesToTime(string minutes)
        {
            if (!int.TryParse(minutes, out int minutesInt)) return "";
            return new TimeSpan(0, minutesInt, 0).ToString(@"hh\:mm");
        }
    }
}