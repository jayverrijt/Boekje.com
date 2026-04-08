namespace Boekje.Web.Extensions;

public static class SessionExtensions
{
    public static bool IsLoggedIn(this HttpContext context)
    {
        return !string.IsNullOrEmpty(context.Session.GetString("UserEmail"));
    }
}