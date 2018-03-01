using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransitScheduleEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitScheduleEditor.Models.Tests
{
    [TestClass()]
    public class EnumExtensionsTests
    {
        [TestMethod()]
        public void GetDisplayNameTest()
        {
            // Arrange
            var platform = new StaticTrain.PlatformType();
            var day = new StaticTrain.DayType();

            // Act
            platform = StaticTrain.PlatformType.Platform1;
            day = StaticTrain.DayType.Friday;

            // Assert
            Assert.AreEqual("Platform 1", platform.GetDisplayName());
            Assert.AreEqual("Friday", day.GetDisplayName());
        }

        [TestMethod()]
        public void GetDisplayNameByIntValueTest()
        {
            // Arrange
            var platform = new StaticTrain.PlatformType();

            // Act
            platform = (StaticTrain.PlatformType)Enum.Parse(typeof(StaticTrain.PlatformType), "0");

            // Assert
            Assert.AreEqual("Platform 1", platform.GetDisplayName());
        }
    }
}