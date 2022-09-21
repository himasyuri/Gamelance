namespace GamelanceAuth.Models.Responses
{
    public class TokensResponse
    {
        public string RefreshToken { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;
    }
}
