using Android.App;
using Android.Content;
using Android.Net;
using Android.Widget;

public static class ConnectivityHelper
{
    public static bool IsInternetConnectionAvailable(Context context)
    {
        ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
        NetworkInfo activeNetworkInfo = connectivityManager.ActiveNetworkInfo;

        return activeNetworkInfo != null && activeNetworkInfo.IsConnected;
    }

    public static void ShowToast(Context context, string message)
    {
        Toast.MakeText(context, message, ToastLength.Short).Show();
    }
}
