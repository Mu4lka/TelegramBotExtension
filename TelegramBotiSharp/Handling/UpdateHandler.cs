﻿using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using System.Reflection;
using TelegramBotExtension.FiniteStateMachine;
using Telegram.Bot.Types.Enums;
using TelegramBotExtension.Filters;
using TelegramBotExtension.Types;

namespace TelegramBotExtension.Handling;

public class UpdateHandler(
    IEnumerable<IUpdateTypeHandler> _handlers,
    ITelegramBotClient _botClient,
    IStorage<long> _storage) : IUpdateHandler
{
    public Task StartBot()
    {
        _botClient.StartReceiving(this);
        return Task.CompletedTask;
    }

    public virtual async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var handlers = _handlers.Where(
            handler => handler.UpdateType == update.Type);

        foreach (var handler in handlers)
        {
            var methodInfo = handler.GetType().GetMethod(nameof(handler.HandleUpdateAsync));
            var context = handler.GetContext(botClient, _storage, update);

            if (await CheckFiltersAsync(methodInfo!, context))
            {
                await handler.HandleUpdateAsync(context);
                return;
            }
        }
    }

    public virtual Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        throw exception;
    }

    private static async Task<bool> CheckFiltersAsync(MethodInfo method, TelegramContext context)
    {
        var filters = method.GetCustomAttributes(false).OfType<FilterAttribute>();

        foreach (FilterAttribute filter in filters)
        {
            if (!await filter.CallAsync(context))
                return false;
        }
        return true;
    }
}
