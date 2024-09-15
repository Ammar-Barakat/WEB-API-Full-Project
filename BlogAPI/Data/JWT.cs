namespace BlogAPI.Data
{
    public class JWT
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
