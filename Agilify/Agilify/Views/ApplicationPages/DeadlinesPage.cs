using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Agilify.Models;
using Xamarin.Forms;
using Syncfusion.SfSchedule.XForms;

namespace Agilify.Views.ApplicationPages
{
	public class DeadlinesPage : ContentPage
	{
        public SfSchedule Scheduler { get; set; } = new SfSchedule();
        public ObservableCollection<Deadline> Deadlines { get; set; } = new ObservableCollection<Deadline>();
        public DeadlinesPage ()
		{
		    Title = "Deadlines";

		    ScheduleAppointmentMapping mapping = new ScheduleAppointmentMapping
		    {
		        SubjectMapping = "Title",
                ColorMapping = "Color"
		    };

		    Scheduler.AppointmentMapping = mapping;

		    Scheduler.DataSource = Deadlines;

            Scheduler.ScheduleView = ScheduleView.MonthView;
		    Scheduler.ShowAppointmentsInline = true;

		    MonthInlineViewStyle style = new MonthInlineViewStyle
		    {
                BackgroundColor = Color.FromHex("#E3E3E3"),
                TextSize = 16,
                TimeTextColor = Color.Transparent
		    };

		    Scheduler.OnMonthInlineLoadedEvent += (sender, args) =>
		    {
		        args.monthInlineViewStyle = style;
		    };

		    Scheduler.OnMonthInlineAppointmentLoadedEvent += (sender, args) =>
		    {
		        

		    };

		    Scheduler.MonthInlineAppointmentTapped += (sender, args) =>
		    {
		        
                
		    };

		    Content = Scheduler;
		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
            var sprints = await App.SyncManager.SprintsStore.GetItemsAsync();
            var projects = await App.SyncManager.ProjectsStore.GetItemsAsync();
            foreach (var sprint in sprints)
	        {
	            Deadlines.Add(new Deadline
	            {
	                Sprint = sprint,
                    Project = projects.FirstOrDefault(p => p?.Id == sprint?.ProjectId),
                    Color = Color.Blue,
                    From = sprint.StartDate,
                    To = sprint.EndDate

	            });
	        }
        }
    }
}
