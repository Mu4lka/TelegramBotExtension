﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotExtension.FiniteStateMachine;
using TelegramBotExtension.Types;

namespace TelegramBotExtension.Handling.Handlers;

public abstract class CallbackQueryHandler : IUpdateTypeHandler
{
    public UpdateType UpdateType => UpdateType.CallbackQuery;

    public abstract Task HandleUpdateAsync(TelegramContext context);

    public TelegramContext GetContext(ITelegramBotClient botClient, IStorage<long> storage, Update update)
        => new(
            botClient,
            storage,
            update,
            update.CallbackQuery!.From,
            update.CallbackQuery.Data!
            );
}
