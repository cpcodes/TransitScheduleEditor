using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransitScheduleEditor.Models;
using System.Data.Entity.Infrastructure;
using System.Web.ModelBinding;
using TransitScheduleEditor.Controllers;

namespace TransitScheduleEditor
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ScheduleGrid_CallingDataMethods(object sender, CallingDataMethodsEventArgs e)
        {
            e.DataMethodsObject = new ScheduleBL();
        }
    }
}