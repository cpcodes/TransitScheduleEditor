using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransitScheduleEditor.ViewModels;

namespace TransitScheduleEditor.ViewModels. Tests
{
    [TestClass]
    public class ScheduleEntryTests
    {
        [DataTestMethod]
        [DataRow("2:00", "02:00")]
        [DataRow("02:00", "02:00")]
        [DataRow("01:02:00", "01:02")]
        [DataRow("0:00", "00:00")]
        public void DeparturePropertyReturnsStoredTimes(string value, string expected)
        {
            // Arrange
            var scheduleEntry = new ScheduleEntry();
            string result;

            // Act
            scheduleEntry.Departure = value;
            result = scheduleEntry.Departure;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("25:00")]
        [DataRow("Spam")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("1234")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeparturePropertyThrowsArgumentOutOfRangeExceptionForBadValues(string value)
        {
            // Arrange
            var scheduleEntry = new ScheduleEntry();

            // Act/Assert
            scheduleEntry.Departure = value;
        }
    }
}

