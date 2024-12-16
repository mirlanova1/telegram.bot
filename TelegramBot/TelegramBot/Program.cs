
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using System.Globalization;

class Program
{
    static async Task Main(string[] args)
    {
        string token = "8027591217:AAEoz_nVMJQdRz_tcwrZ0uqyY6QlIoLI-Jo";
        var botClient = new TelegramBotClient(token);

        var botInfo = await botClient.GetMeAsync();
        Console.WriteLine($"{botInfo}бот работает");

        using var cts = new CancellationTokenSource();
        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        botClient.StartReceiving(
          HandleUpdateAsync,
          HandleErrorAsync,
          receiverOptions: receiverOptions,
          cancellationToken: cts.Token
          );
         Console.WriteLine("Нажмите на любую клавишу,что завершить работу");
         Console.ReadKey(); 
         cts.Cancel();
                                
    }

    /* async Task-ассинхронный сразу все работает*/

    static async Task HandleUpdateAsync(ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    /* ||-или */
    {
        if (update.Message is not { } message || message.Text is not { } messageText)
        {
            return;
        }
        var chatId = message.Chat.Id;
        Console.WriteLine($"сообщение пришло: {message.Text}, чат: {chatId}");

        await botClient.SendTextMessageAsync(chatId: chatId, text: $"вы написали: {messageText}", cancellationToken: cancellationToken);

    }
    static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,CancellationToken cancellationToken)
    {
        Console.WriteLine($"вышла ошибка: {exception.Message}");
        return Task.CompletedTask;
    }


}