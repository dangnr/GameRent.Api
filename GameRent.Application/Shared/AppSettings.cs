namespace GameRent.Application.Shared
{
    public class AppSettings
    {
        public class TokenSettings
        {
            public string TokenSecret { get; set; }
            public int ExpirationMinutes { get; set; }
        }

        public class ConnectionStrings
        {
            public string DefaultConnection { get; set; }
        }
    }
}