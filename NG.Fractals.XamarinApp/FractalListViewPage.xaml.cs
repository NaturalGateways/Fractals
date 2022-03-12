using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NG.Fractals.XamarinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FractalListViewPage : ContentPage
    {
        public ObservableCollection<FractalListItemViewModel> Items { get; set; }

        public FractalListViewPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<FractalListItemViewModel>(FractalFactory.CreateFractals().Select(x => new FractalListItemViewModel(x)));

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Check selected fractal
            FractalListItemViewModel selectedVM = e.Item as FractalListItemViewModel;
            if (selectedVM == null)
                return;

            // Go to fractal
            await this.Navigation.PushAsync(new FractalPage(selectedVM.Fractal));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
