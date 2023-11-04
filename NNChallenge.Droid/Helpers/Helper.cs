using Android.App;
using Android.Content;
using Android.Net;
using Android.Widget;
using Xamarin.Essentials;

public static class Helper
{
    public static bool IsInternetConnectionAvailable(Context context)
    {
        return Connectivity.NetworkAccess == NetworkAccess.Internet;
    }

    public static void ShowToast(Context context, string message)
    {
        Toast.MakeText(context, message, ToastLength.Short).Show();
    }
}
