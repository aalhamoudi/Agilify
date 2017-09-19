using Syncfusion.SfKanban.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Agilify.ViewModels
{
    public class DashboardViewModel
    {
        public ObservableCollection<KanbanModel> Cards { get; set; }

        public DashboardViewModel()
        {
            Cards = new ObservableCollection<KanbanModel>();
            Cards.Add(new KanbanModel()
            {
                ID = 1,
                Title = "iOS - 1002",
                //ImageURL = "microsoft.png",
                Category = "Open",
                Description = "Analyze customer requirements",
                //ColorKey = "Red",
                Tags = new string[] { "Incident", "Customer" }
            });

            Cards.Add(new KanbanModel()
            {
                ID = 2,
                Title = "iOS - 1002",
                //ImageURL = "microsoft.png",
                Category = "Closed",
                Description = "Analyze customer requirements",
                //ColorKey = "Red",
                Tags = new string[] { "Incident", "Customer" }
            });
        }
    }
}
