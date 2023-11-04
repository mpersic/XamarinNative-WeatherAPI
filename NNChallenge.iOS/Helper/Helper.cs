using System.Runtime.Remoting.Contexts;
using Xamarin.Essentials;

public static class Helper
{
    public static bool IsInternetConnectionAvailable()
    {
        return Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}