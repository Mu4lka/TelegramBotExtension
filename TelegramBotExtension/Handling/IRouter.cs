﻿using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotExtension.Types.Contexts;

namespace TelegramBotExtension.Handling
{
    public interface IRouter
    {
        delegate Task Message(MessageContext context);
        delegate Task EditedMessage(MessageContext context);
        delegate Task ChannelPost(MessageContext context);
        delegate Task EditedChannelPost(MessageContext context);
        delegate Task InlineQuery(InlineQueryContext context);
        delegate Task ChosenInlineResult(ChosenInlineResultContext context);
        delegate Task CallbackQuery(CallbackQueryContext context);
        delegate Task ShippingQuery(ShippingQueryContext context);
        delegate Task PreCheckoutQuery(PreCheckoutQueryContext context);
        delegate Task Poll(PollContext context);
        delegate Task PollAnswer(PollAnswerContext context);
        delegate Task MyChatMember(ChatMemberUpdatedContext context);
        delegate Task ChatMember(ChatMemberUpdatedContext context);
        delegate Task ChatJoinRequest(ChatJoinRequestContext context);
        delegate Task Error(ErrorContext context);
        delegate Task Unknown(UpdateContext context);

        event Message OnMessage;
        event EditedMessage OnEditedMessage;
        event ChannelPost OnChannelPost;
        event EditedChannelPost OnEditedChannelPost;
        event InlineQuery OnInlineQuery;
        event ChosenInlineResult OnChosenInlineResult;
        event CallbackQuery OnCallbackQuery;
        event ShippingQuery OnShippingQuery;
        event PreCheckoutQuery OnPreCheckoutQuery;
        event Poll OnPoll;
        event PollAnswer OnPollAnswer;
        event MyChatMember OnMyChatMember;
        event ChatMember OnChatMember;
        event ChatJoinRequest OnChatJoinRequest;
        event Error OnError;
        event Unknown OnUnknown;

        Task<bool> TryHandleRouterAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}