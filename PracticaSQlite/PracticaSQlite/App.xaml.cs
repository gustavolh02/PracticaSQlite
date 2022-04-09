using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticaSQlite
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDet { get; set; }  
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
