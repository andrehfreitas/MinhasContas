using Plugin.Clipboard;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MinhasContas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageConta : ContentPage
    {
        public PageConta()
        {
            InitializeComponent();
        }


        /* 
         * Método trata quais elementos da interface aparecerão na tela quando a mesma é exibida
         * Se for novo cadastro aparece apenas o botão salvar, se for atualização também aparece
         * a lixeira para exclusão do registro
         */
        protected override void OnAppearing()
        {
            ToolbarItem itemSalvar = new ToolbarItem();
            itemSalvar.Priority = 0;
            itemSalvar.Order = ToolbarItemOrder.Primary;
            itemSalvar.IconImageSource = "savedisk.png";
            itemSalvar.Clicked += OnSaveConta;
            this.ToolbarItems.Add(itemSalvar);

            ToolbarItem itemDeletar = new ToolbarItem();
            itemDeletar.Priority = 0;
            itemDeletar.Order = ToolbarItemOrder.Primary;
            itemDeletar.IconImageSource = "trash.png";
            itemDeletar.Clicked += OnDeleteConta;

            Conta conta = BindingContext as Conta;
            if (conta.Id != 0)
            {
                this.ToolbarItems.Add(itemDeletar);
            } else
            {
                entryValor.Text = "";
            }
        }


        // Método que salva o cadastro ou a atualização de uma conta
        private void OnSaveConta(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(entryNome.Text) || string.IsNullOrEmpty(entryValor.Text))
            {
                DisplayAlert("Atenção", "Nome e valor são dados obrigatórios", "OK");
            } else
            {
                Conta c = BindingContext as Conta;
                lblInfComp.Focus();
                App.Database.SaveConta(c);
                Navigation.PopAsync();
            }
        }


        // Método que deleta uma conta
        private void OnDeleteConta(object sender, EventArgs args)
        {
            Conta c = BindingContext as Conta;
            App.Database.DeleteConta(c);
            Navigation.PopAsync();
        }

        
        // Método que copia o código de barras para a área de transferência
        private void CopiaCodigoBarras(object sender, EventArgs e)
        {
            CrossClipboard.Current.SetText(entryCodigoBarras.Text);
            DependencyService.Get<IMessage>().LongAlert("Código de barras copiado");
        }


        // Máscara para o campo Valor
        private void Mascara(object sender, EventArgs e)
        {
            var ev = e as TextChangedEventArgs;
            if (ev.NewTextValue != ev.OldTextValue)
            {
                var entry = (Entry)sender;
                entry.TextChanged -= Mascara;
                string text = Regex.Replace(ev.NewTextValue.Trim(), @"[^0-9]", "");

                if (text.Length > 2)
                {
                    text = text.Insert(text.Length - 2, ",");
                }

                if (text.Length > 6)
                {
                    text = text.Insert(text.Length - 6, ".");
                }
                if (text.Length > 10)
                {
                    text = text.Insert(text.Length - 10, ".");
                }

                if (entry.Text != text)
                    entry.Text = text;
                entry.TextChanged += Mascara;
            }
        }

    }
}