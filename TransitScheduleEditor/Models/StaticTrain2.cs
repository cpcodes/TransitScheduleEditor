using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace TransitScheduleEditor.Models
{

    [MetadataType(typeof(StaticTrainMetadata))]
    public partial class StaticTrain
    {
        public enum DayType
        {
            Weekdays,
            Weekends,
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
            [Display(Name="North Bound")]
            NorthBound,
            [Display(Name="San Diego")] SanDiego,
            [Display(Name="South Bound")] SouthBound
        }

        public enum PlatformType
        {
            [Display(Name ="Platform 1")] Platform1,
            [Display(Name = "Platform 2")] Platform2,
            [Display(Name = "Platform 3")] Platform3,
            SPRINTER
        }

        [DataType(DataType.Time, ErrorMessage = "Please enter a time in either 12 or 24-hour format.")]
        public DateTime Departure
        {
            get { return DateTime.Parse(minutesToTime(DepartTime)); }
            set {
                string result = timeToMinutes(value.ToString());
                if (result == "") throw new ArgumentOutOfRangeException("Departure", "The value provided does not map to a recognized time format"); // Only save the new value if it is valid
                else
                {
                    DepartTime = result;
                }
            }
        }

        public PlatformType PlatformName
        {
            get { return (PlatformType) Enum.Parse(typeof(PlatformType), Platform.Replace(" ", string.Empty)); }
            set { Platform = value.GetDisplayName(); }
        }

        public DirectionType DirectionName
        {
            get { return (DirectionType)Enum.Parse(typeof(DirectionType), Direction.Replace(" ", string.Empty)); }
            set { Direction = value.GetDisplayName(); }
        }

        public TrainType TrainName
        {
            get { return (TrainType)Enum.Parse(typeof(TrainType), Train); }
            set { Train = value.GetDisplayName(); }
        }

        public DayType DayName
        {
            get { return (DayType)Enum.Parse(typeof(DayType), Days); }
            set { Days = value.GetDisplayName(); }
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

    public class StaticTrainMetadata
    {
        [Key]
        public int StaticTrainId { get; set; }

        [Required]
        //[EnumDataType(typeof(StaticTrain.TrainType))]
        public string Train { get; set; }

        [Required]
        public string Number { get; set; }

        //[EnumDataType(typeof(StaticTrain.DirectionType))]
        public string Direction { get; set; }

        [Required]
        [Display(Name = "Depart Time")]
        public string DepartTime { get; set; }

        [Required]
        //[EnumDataType(typeof(StaticTrain.PlatformType))]
        public string Platform { get; set; }

        [Required]
        [Display(Name = "Run Days")]
        //[EnumDataType(typeof (StaticTrain.DayType))]
        public string Days { get; set; }

        public string TrainID { get; set; }
        public string BlockID { get; set; }
        public string StopID { get; set; }
        public int stop_sequence { get; set; }
        public string Trip_id { get; set; }
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null || !Enum.IsDefined(enumValue.GetType(), enumValue)) return null;
            var enumMember = enumValue.GetType().GetMember(enumValue.ToString()).First();
            if (enumMember == null) return null;

            var displayAttribute = enumMember.GetCustomAttribute<DisplayAttribute>();
            
            return displayAttribute == null ? enumMember.Name : displayAttribute.GetName();
        }
    }
}