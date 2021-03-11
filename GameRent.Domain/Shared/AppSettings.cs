namespace GameRent.Domain.Shared
{
    public class AppSettings
    {
        public class TokenSettings
        {
            public string TokenSecret { get; set; }
            public int ExpirationMinutes { get; set; }
        }
    }
}