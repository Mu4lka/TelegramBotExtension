﻿using Telegram.Bot;

namespace TelegramBotExtension.Types.Contexts.Base;

public abstract class BaseContext(ITelegramBotClient bot, CancellationToken cancellationToken)
{
    public ITelegramBotClient Bot { get; set; } = bot;

    public CancellationToken CancellationToken { get; set; } = cancellationToken;
}
