using System;

namespace Handy.App.Configuration
{
    public static class AppConfig
    {
        public static string DbConnectionString => Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        public static string AppUrl => Environment.GetEnvironmentVariable("APP_URL");
        public static string TelegramApiToken => Environment.GetEnvironmentVariable("TELEGRAM_API_TOKEN");
        public static string TelegramWebhookUrl => Environment.GetEnvironmentVariable("TELEGRAM_WEBHOOK_URL");
        public static string JwtSecurityKey => Environment.GetEnvironmentVariable("JWT_SECURITY_KEY");
        public static int JwtExpirationTime => int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRATION_TIME"));
    }
}