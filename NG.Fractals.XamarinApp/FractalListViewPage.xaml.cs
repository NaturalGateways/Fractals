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
        private IFractal[] m_fractalArray = null;

        public ObservableCollection<string> Items { get; set; }

        public FractalListViewPage()
        {
            InitializeComponent();

            m_fractalArray = FractalFactory.CreateFractals();
#if DEBUG
            Items = new ObservableCollection<string>(new string[]
                {
                    FractalFactory.CreateSingleFractal().FractalName
                });
#else
            Items = new ObservableCollection<string>(m_fractalArray.Select(x => x.FractalName).ToArray());
#endif

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Check selected fractal
            string selectedFractalName = e.Item as string;
            if (selectedFractalName == null)
                return;

            // Go to fractal
            IFractal selectedFractal = m_fractalArray.FirstOrDefault(x => x.FractalName == selectedFractalName);
            if (selectedFractal != null)
            {
                await this.Navigation.PushAsync(new FractalPage(selectedFractal));
            }

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
