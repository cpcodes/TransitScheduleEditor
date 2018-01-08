using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransitScheduleEditor.ViewModels;
using TransitScheduleEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitScheduleEditor.ViewModels.Tests
{
    [TestClass()]
    public class ScheduleEditVMTests
    {
        [TestMethod()]
        public void getScheduleTest()
        {
            // Arrange
            var obj = new ScheduleEditVM();

            // Act
            var obj2 = obj.getSchedule();

            // Assert
            Console.WriteLine($"Number of entries: {obj2.Count()}");
            foreach (var item in obj2)
            {
                Console.WriteLine($"Train - {item.Train}, Number - {item.Number}");
            }
        }
    }
}