using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MinhasContas
{
    public partial class App : Application
    {
        internal static ContaDatabase database;
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }


        // Faz a conexão com o Banco de Dados
        internal static ContaDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ContaDatabase(
                        DependencyService.Get<IFileHelper>().
                        GetLocalFilePath("ContaSQLite.db3"));
                }
                return database;
            }
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
