using System;

namespace Handy.App.Configuration
{
    public static class AppConfig
    {
//        public static string DbConnectionString => Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
//        public static string AppUrl => Environment.GetEnvironmentVariable("APP_URL");
//        public static string TelegramApiToken => Environment.GetEnvironmentVariable("TELEGRAM_API_TOKEN");
//        public static string TelegramWebhookUrl => Environment.GetEnvironmentVariable("TELEGRAM_WEBHOOK_URL");
//        public static string JwtSecurityKey => Environment.GetEnvironmentVariable("JWT_SECURITY_KEY");
//        public static int JwtExpirationTime => int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRATION_TIME"));

        public static string DbConnectionString => "host=localhost;port=5432;database=pisya3;username=postgres;password=123";
        public static string AppUrl => "http://localhost:8443";
        public static string TelegramApiToken => "773495748:AAHiN_08Q8EUtDCBTxiSOtPAt-u7D1uKsIw";
        public static string TelegramWebhookUrl => "https://feb083f5.ngrok.io/api/webhook";
        public static string JwtSecurityKey => "UbeJoux01ULXUIfQkv";
        public static int JwtExpirationTime => 336;
    }
}