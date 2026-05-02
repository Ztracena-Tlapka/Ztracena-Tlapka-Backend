namespace Ztracena_Tlapka_Backend.Application.Common;

public static class ResCodes
{
    public const string Success = "S1000";

    public const string InternalError = "E1000";

    public static class Users
    {
        public const string NotFound   = "E2001";
        public const string EmailTaken = "E2002";
        public const string Added = "S2001";
        public const string Updated = "S2002";
        public const string Deleted = "S2003";
        public const string Get = "S2004";
    }

    public static class Auth
    {
        public const string InvalidCredentials = "E3001";
        public const string NotAuthenticated   = "E3002";
        public const string LoggedIn  = "S3001";
        public const string LoggedOut = "S3002";
        public const string Verified  = "S3003";
    }
}
