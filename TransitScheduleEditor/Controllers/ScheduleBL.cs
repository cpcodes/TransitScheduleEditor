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
        public void UpdateTrain(int id, ModelMethodContext context)
        {
            StaticTrain item = db.StaticTrains.Find(id);
            if (item == null)
            {
                context.ModelState.AddModelError("",
                    $"Item with id {id} was not found.");
                return;
            }

            context.TryUpdateModel(item);
            if (context.ModelState.IsValid)
            {
                db.SaveChanges();
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void DeleteTrain(int id, ModelMethodContext context)
        {
            var item = new StaticTrain { StaticTrainId = id };
            db.Entry(item).State = EntityState.Deleted;
            try
            {
                db.SaveChanges();
            }
            catch
            {
                context.ModelState.AddModelError("",
                    $"Item with id {id} no longer exists in the database.");
            }
        }

        public void InsertTrain(ModelMethodContext context)
        {
            var item = new StaticTrain();
            context.TryUpdateModel(item);
            if (context.ModelState.IsValid)
            {
                db.StaticTrains.Add(item);
                db.SaveChanges();
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