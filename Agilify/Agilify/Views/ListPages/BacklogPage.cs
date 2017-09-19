using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Agilify.Helpers;
using Agilify.Models;
using Agilify.Views.CreatePages;
using Agilify.Views.DetailPages;
using Agilify.Views.EditPages;
using Syncfusion.SfKanban.XForms;
using Xamarin.Forms;

namespace Agilify.Views.ListPages
{
    public class BacklogPage<T, P, D, C, E> : ItemsPage<T, P, D, C, E> where T : BacklogItem, new() where P : IItem, new() where C : CreateItemPage<T, P>, new() where D : ItemDetailPage<T>, new() where E : EditItemPage<T>, new()
    {
        public SfKanban Board { get; set; } = new SfKanban();
        public ObservableCollection<KanbanModel> Cards { get; set; } = new ObservableCollection<KanbanModel>();

        public BacklogPage(Func<T, bool> filter = null) : base(filter)
        {
            Board.BindingContext = VM;


            List<KanbanColorMapping> colormodels = new List<KanbanColorMapping>();
            colormodels.Add(new KanbanColorMapping("Green", Color.Green));
            colormodels.Add(new KanbanColorMapping("Red", Color.Red));
            colormodels.Add(new KanbanColorMapping("Yellow", Color.Yellow));
            colormodels.Add(new KanbanColorMapping("Blue", Color.Blue));
            Board.ColorModel = colormodels;



            Content = Board;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                Board.ItemsSource = Cards;

                VM.Items.CollectionChanged += (sender, args) =>
                {
                    foreach (var item in VM.Items)
                    {
                        KanbanModel card = item;
                        if (!Cards.Select(c => c.ID).Contains(card.ID))
                            Cards.Add(card);
                    }
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
