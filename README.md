# TelegramBotiSharp

**TelegramBotiSharp** - абстакция над библиотекой [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot).
Использовав данную небольшую библиотеку, она обеспечит вам удобство написания более сложных телеграм-ботов на **C#**

## Преимущество
1. Не надо проверять тип обновления - Вам больше не нужно проверять конкретные типы обновления, так как существуют классы-обработчики конкретного типа обновления
2. Айди пользователя теперь в одном месте - Вам больше не нужно получать Айди пользователя из типа обновления, теперь он находится в классе TelegramContext
3. Машина состояния - Вы сможете сохранять состояние пользователя и обрабатывать его
4. Кастомизация - Вы сможете самостоятельно делать свои фильтры
   
## Быстрый старт
1. Клонируйте данный репозиторий
2. Токен поместите в appsettings.json
3. Файл appsettings.json поместите со своим токеном в ../TelegramBotiSharp.Exemples\bin\Debug\net8.0
4. Запустите

### Файл Program.cs
В файле Program.cs используется Dependency Injection, вы можете отказаться от этого, переписав на следующий код в данном файле:
```C#
await new UpdateHandler(
    [new StartCommandHandler()],
    new TelegramBotClient("TOKEN"),
    new MemoryStorage()).StartBot();
```

## Краткая документация
### Создание класса обработчика
```C#
internal class StartCommandHandler : MessageHandler
{
    [CommandFilter("start")]
    public override async Task HandleUpdateAsync(TelegramContext context)
    {
        await context.BotClient.SendTextMessageAsync(context.UserId, "Hello, World!");
    }
}
```
В данном примере обработчик StartCommandHandler будет обрабатываться а том случае, если пользователь отправил сообщение-команду "/start"

**Основные обработчки:**
* CallbackQueryHandler - триггер на Telegram.Bot.Types.CallbackQuery
* MessageHandler - триггер на Telegram.Bot.Types.Message

### Фильтры
В примере выше был использован фильтр-aттрибут CommandFilter("start"), обработчик сработает в том случае если пользователь отправит сообщение-команду "/start"

**Основные фильтры:**
* CommandFilter - Cообщение является командой
* StateFilter - Состояние пользователя
* DataFilter - Данные отправленные пользователем

Фильтры могут комбинироваться

```C#
internal class ExempleHandler : CallbackQueryHandler
{
    [StateFilter(nameof(State.Proccess))]
    [DataFilter("Далее")]
    public override async Task HandleUpdateAsync(TelegramContext context)
    {
        //code...
    }
}
```
В примере выше, данный обработчик сработает в том случае, если состояние пользователя будет State.Proccess и он нажал на кнопку InlineKeyboardButton в которой содержится данные "Далее"
### Кастомные фильтры
Чтобы сделать кастомный фильтр, создайте класс, наследованный от FilterAttribute, и реализуйте его. Пример кастомного фильттра:
```C#
internal class ExempleFilter(string? data) : FilterAttribute(data)
{
    public override async Task<bool> CallAsync(TelegramContext context)
    {
        //code...
        return true;
    }
}
```
