using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MinhasContas
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        // Reescrita do método OnAppearing
        // Sempre que esta tela aparecer o BD faz a busca dos registros e atualiza a listview
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Conta> listanaopagas = await App.Database.GetContasNaoPagas();
            listanaopagas.OrderBy(c => DateTime.ParseExact(c.DataVencimento, "dd/MM/yyyy", CultureInfo.InvariantCulture));

            List<Conta> listapagas = await App.Database.GetContasPagas();
            listapagas.OrderByDescending(c => DateTime.ParseExact(c.DataVencimento, "dd/MM/yyyy", CultureInfo.InvariantCulture));

            List<Conta> listacontas = new List<Conta>();
            listacontas.AddRange(listanaopagas);
            listacontas.AddRange(listapagas);

            lvConta.ItemsSource = listacontas;
        }


        // Método que abre a PageConta com os dados do item selecionado na listview
        private void OnItemSelected(object Sender, SelectedItemChangedEventArgs args)
        {
            if (args != null)
            {
                Navigation.PushAsync(new PageConta() {BindingContext = args.SelectedItem as Conta});
            }
        }


        // Método do botão que abre a PageConta para cadastro de uma nova conta
        private void OnClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new PageConta() { BindingContext = new Conta() });
        }

    }

    
}
