using System;
using System.Collections.Generic;
using Handy.Bot.BotCommands;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Handy.Bot.Core
{
    public class HandyBot
    {
        public TelegramBotClient Api = new TelegramBotClient(Environment.GetEnvironmentVariable("TELEGRAM_API_TOKEN"));
    }
}