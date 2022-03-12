using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NG.Fractals.XamarinApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            NavigationPage navPage = new NavigationPage(new FractalListViewPage());
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
