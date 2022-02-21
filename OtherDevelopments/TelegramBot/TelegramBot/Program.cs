using Google.Cloud.Dialogflow.V2;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        static TelegramBotClient botClient;

        static async Task Main(string[] _)
        {
            botClient = new TelegramBotClient("5214002643:AAHVO5cakNsLj1r9g1zaZqnT140XEtyBl2g");
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "small-talk-srqi.json");
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ReceiverOptions receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };
            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationTokenSource.Token);
            User me = await botClient.GetMeAsync();
            Console.WriteLine("Start listening for {0}", me.Username);
            Console.ReadKey();
            cancellationTokenSource.Cancel();
        }

        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message != null && update.Message.Type == MessageType.Text)
            {
                long chatId = update.Message.Chat.Id;
                string messageText = update.Message.Text;
                if (messageText == "/start")
                {
                    string menu = "/start - запуск\n/inline - меню\n/keyboard - сообщения\n/clearkeyboard - убрать кнопки";
                    await botClient.SendTextMessageAsync(chatId, menu, cancellationToken: cancellationToken);
                }
                else if (messageText == "/inline")
                {
                    InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("VK","https://vk.com/mishanya167"),
                            InlineKeyboardButton.WithUrl("LinkedIn","https://www.linkedin.com/in/%D0%BC%D0%B8%D1%85%D0%B0%D0%B8%D0%BB-%D0%BA%D0%BE%D0%B2%D0%B0%D0%BB%D1%91%D0%B2-79637b164/")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Дата и время", "DateTime"),
                            InlineKeyboardButton.WithCallbackData("Картинка", "Photo")
                        }
                    });
                    await botClient.SendTextMessageAsync(chatId, "Выберите пункт меню: ", replyMarkup: inlineKeyboard, cancellationToken: cancellationToken);
                }
                else if (messageText == "/keyboard")
                {
                    ReplyKeyboardMarkup replayKeyBoard = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("Привет"),
                            new KeyboardButton("Как дела?")
                        },
                        new[]
                        {
                            KeyboardButton.WithRequestContact("Контакт"),
                            KeyboardButton.WithRequestLocation("Геолокация")
                        }
                    });
                    await botClient.SendTextMessageAsync(chatId, "Выберите сообщение.", replyMarkup: replayKeyBoard, cancellationToken: cancellationToken);
                }
                else if (messageText == "/clearkeyboard")
                {
                    await botClient.SendTextMessageAsync(chatId, "Кнопки убраны.", replyMarkup: new ReplyKeyboardRemove(), cancellationToken: cancellationToken);
                }
                else
                {
                    SessionsClient sessionsClient = await SessionsClient.CreateAsync();
                    SessionName sessionName = new SessionName("small-talk-srqi", Guid.NewGuid().ToString());
                    QueryInput queryInput = new QueryInput
                    {
                        Text = new TextInput
                        {
                            Text = messageText,
                            LanguageCode = "ru-Ru"
                        }
                    };
                    DetectIntentResponse response = await sessionsClient.DetectIntentAsync(sessionName, queryInput);
                    if (response.QueryResult.FulfillmentMessages.Count > 0)
                    {
                        await botClient.SendTextMessageAsync(chatId, response.QueryResult.FulfillmentText, cancellationToken: cancellationToken);
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(chatId, "Прости, я тебя не понимаю.", cancellationToken: cancellationToken);
                    }
                }
            }
            if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery.Message != null)
            {
                string data = update.CallbackQuery.Data;
                if (data == "DateTime")
                {
                    await botClient.AnswerCallbackQueryAsync(update.CallbackQuery.Id, DateTime.Now.ToString(), cancellationToken: cancellationToken);
                }
                if (data == "Photo")
                {
                    await botClient.SendPhotoAsync(update.CallbackQuery.Message.Chat.Id, "https://big-rostov.ru/wp-content/uploads/2021/04/i-7-1.jpg", cancellationToken: cancellationToken);
                }
            }
        }

        static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ApiRequestException)
            {
                ApiRequestException apiRequestException = exception as ApiRequestException;
                Console.WriteLine("Telegram API Error:\n[{0}]\n{1}", apiRequestException.ErrorCode, apiRequestException.Message);
            }
            else
            {
                Console.WriteLine(exception.ToString());
            }
            return Task.CompletedTask;
        }
    }
}
