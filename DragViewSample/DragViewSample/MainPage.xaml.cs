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

            image.Source = (e.SelectedItem as Monkey).Image;
            dragLayout.IsVisible = true;
        }

        void OnCloseTapped(object sender, System.EventArgs e)
        {
            dragLayout.IsVisible = false;
            list.SelectedItem = null;
        }
    }
}
