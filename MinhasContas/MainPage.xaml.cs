using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MinhasContas
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Pagas> ListaAgrupada { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }


        // Reescrita do método OnAppearing
        // Sempre que esta tela aparecer o BD faz a busca dos registros, remonta as listas 
        // e atualiza a listview
        protected override async void OnAppearing()
        {
            await Task.Delay(800);
            base.OnAppearing();
            CarregarLista();
        }


        // Faz a montagem da lista a ser carregada no listview
        private async void CarregarLista()
        {
            ListaAgrupada = new ObservableCollection<Pagas>();


            // Montagem da lista de Contas não pagas
            var contasNaoPagas = new Pagas() { LongName = "NÃO PAGAS" };
            List<Conta> listanaopagas = await App.Database.GetContasNaoPagas();
            listanaopagas.Sort((a, b) => a.DataVencimento.CompareTo(b.DataVencimento)); // Ordenação ascendente
            foreach (Conta conta in listanaopagas)
            {
                contasNaoPagas.Add(conta);
            }

            ListaAgrupada.Add(contasNaoPagas);


            // Montagem da lista de Contas pagas
            var contasPagas = new Pagas() { LongName = "PAGAS" };
            List<Conta> listapagas = await App.Database.GetContasPagas();
            listapagas.Sort((a, b) => b.DataVencimento.CompareTo(a.DataVencimento)); // Ordenação descendente

            foreach (Conta conta in listapagas)
            {
                contasPagas.Add(conta);
            }

            ListaAgrupada.Add(contasPagas);


            // Alimentando a listview com a Lista agrupada e categorizada
            lvConta.ItemsSource = null;
            lvConta.ItemsSource = ListaAgrupada;
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
