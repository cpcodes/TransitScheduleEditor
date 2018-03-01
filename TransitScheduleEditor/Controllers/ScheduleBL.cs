using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using TransitScheduleEditor.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TransitScheduleEditor.Controllers
{
    public class ScheduleBL : IDisposable
    {
        TransitScheduleEntities db = new TransitScheduleEntities();

        public IQueryable<StaticTrain> GetTrains([Control] String DisplayTrain, [Control] String DisplayDays, [Control] String DisplayPlatform)
        {
            var query = db.StaticTrains.Where(x => x.TrainID != "");

            if (DisplayTrain != null)
            {
                query = query.Where(t => t.Train == DisplayTrain);
            }

            if (DisplayDays != null)
            {
                query = query.Where(t => t.Days == DisplayDays);
            }

            if (DisplayPlatform != null)
            {
                query = query.Where(t => t.Platform == DisplayPlatform);
            }

            return query;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void UpdateTrain(int StaticTrainId, ModelMethodContext context)
        {
            StaticTrain item = db.StaticTrains.Find(StaticTrainId);
            if (item == null)
            {
                context.ModelState.AddModelError("",
                    $"Item with id {StaticTrainId} was not found.");
                return;
            }

            try
            {
                context.TryUpdateModel(item);
            }
            catch (ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "Departure")
                {
                    context.ModelState.AddModelError("", $"The value entered for the Departure field is not valid. {e.Message}");
                }
            }
            ValidateTrain(item);
            if (context.ModelState.IsValid)
            {
                db.SaveChanges();
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void DeleteTrain(int StaticTrainId, ModelMethodContext context)
        {
            var item = new StaticTrain { StaticTrainId = StaticTrainId };
            db.Entry(item).State = EntityState.Deleted;
            try
            {
                db.SaveChanges();
            }
            catch
            {
                context.ModelState.AddModelError("",
                    $"Item with id {StaticTrainId} no longer exists in the database.");
            }
        }

        public void InsertTrain(ModelMethodContext context)
        {
            var item = new StaticTrain();
            context.TryUpdateModel(item);
            item.BlockID = "";
            item.Trip_id = "";
            ValidateTrain(item);

            if (context.ModelState.IsValid)
            {
                db.StaticTrains.Add(item);
                db.SaveChanges();
            }
        }

        private static void ValidateTrain(StaticTrain item)
        {
            if (item.Platform.Length == 1) item.Platform = ((StaticTrain.PlatformType)Enum.Parse(typeof(StaticTrain.PlatformType), item.Platform)).GetDisplayName();
            if (item.Direction.Length == 1) item.Direction = ((StaticTrain.DirectionType)Enum.Parse(typeof(StaticTrain.DirectionType), item.Direction)).GetDisplayName();
            if (item.Days.Length == 1) item.Days = ((StaticTrain.DayType)Enum.Parse(typeof(StaticTrain.DayType), item.Days)).GetDisplayName();
            if (item.Train.Length == 1) item.Train = ((StaticTrain.TrainType)Enum.Parse(typeof(StaticTrain.TrainType), item.Train)).GetDisplayName();
            if (item.Train == "Metrolink")
            {
                item.TrainID = "metrolink";
                item.StopID = "29000";
                item.Direction = "North Bound"; // Metrolink only goes NB from OTC. This logic will need to change to support non-OTC stations or if Metro continues south
            }
            else if (item.Train == "Coaster")
            {
                item.TrainID = "398";
                item.StopID = "28000";
                item.Direction = "San Diego"; // Same as Metrolink, except SB
            }
            else if (item.Train == "Sprinter")
            {
                item.TrainID = "399";
                item.StopID = "27000";
                item.Direction = "Escondido"; // Same as Metro/Coaster except Escondido
            }
            else if (item.Direction == "North Bound") // Here and below is Amtrak
            {
                item.TrainID = "amtrak_nb";
                item.StopID = "29001";
            }
            else
            {
                item.TrainID = "amtrak_sb";
                item.StopID = "29002";
                item.Direction = "South Bound"; // At this point, we have a non-NB Amtrak, so we ensure it is SB
            }
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
        }
        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}