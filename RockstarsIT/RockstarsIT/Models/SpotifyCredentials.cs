namespace RockstarsIT.Models
{
    public class SpotifyCredentials
    {
        private readonly string _clientId = "f5bfb02b6b684aa0a51059c31b4fd0f2";
        private readonly string _clientSecret = "a9f624e83e96464b9685ce5cbcbf19c2";
        public string ClientId { get => _clientId; }
        public string ClientSecret { get => _clientSecret; }
    }
}