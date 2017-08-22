using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DragViewSample
{
    public partial class MainPage : ContentPage
    {
   
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();

            dragLayout.BackgroundColor = new Color(0, 0, 0, 0.7);
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var monkey = e.SelectedItem as Monkey;
            image.Source = monkey.Image;
            switch(monkey.Name)
            {
                case "Marino":
                    dragView.DragMode = DragMode.LongPress;
                    dragView.DragDirection = DragDirectionType.Vertical;
                    break;
                case "Carlos":
                    dragView.DragMode = DragMode.Touch;
                    dragView.DragDirection = DragDirectionType.Horizontal;
                    break;
                default:
                    dragView.DragMode = DragMode.LongPress;
                    dragView.DragDirection = DragDirectionType.All;
                    break;
            }
            dragView.RestorePositionCommand.Execute(null);
     
            dragLayout.IsVisible = true;
            
        }

        void OnCloseTapped(object sender, System.EventArgs e)
        {
            dragLayout.IsVisible = false;
            list.SelectedItem = null;
        }
    }
}
