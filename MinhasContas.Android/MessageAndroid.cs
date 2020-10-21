
using Android.App;
using Android.Widget;
using MinhasContas.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace MinhasContas.Droid
{

    // Implementação do toast de exibição na plataforma IOS
    public class MessageAndroid: IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }


        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}