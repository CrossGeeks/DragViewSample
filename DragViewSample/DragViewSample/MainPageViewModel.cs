using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DragViewSample
{
    public class MainPageViewModel
    {
        public ObservableCollection<Monkey> Items { get; set; }= new ObservableCollection<Monkey>();
        public MainPageViewModel()
        {
            Items.Add(new Monkey(){Name="Pedro", Image="monkey1.png"});
            Items.Add(new Monkey() { Name = "Sam", Image = "monkey2.png" });
            Items.Add(new Monkey() { Name = "Carlos", Image = "monkey3.png" });
            Items.Add(new Monkey() { Name = "Marino", Image = "monkey4.png" });
        }
    }
}
