using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransitScheduleEditor.Controllers;
using TransitScheduleEditor.Models;

namespace TransitScheduleEditor
{
    public partial class AddTrain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addTrainForm_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            Response.Redirect("~/Default");
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default");
        }

        protected void addTrainForm_CallingDataMethods(object sender, CallingDataMethodsEventArgs e)
        {
            e.DataMethodsObject = new ScheduleBL();
        }
    }
}