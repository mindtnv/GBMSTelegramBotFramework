using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests.Abstractions;

namespace GBMSTelegramBotFramework.Testing;

public class TelegramTestingBotClient : ITelegramBotClient
{
    public List<IRequest> Requests { get; } = new();
    public bool LocalBotServer { get; }
    public long? BotId { get; }
    public TimeSpan Timeout { get; set; }
    public IExceptionParser ExceptionsParser { get; set; }
    public event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;
    public event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

    public Task<TResponse> MakeRequestAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = new())
    {
        Requests.Add(request);
        return Task.FromResult(default(TResponse))!;
    }

    public Task<bool> TestApiAsync(CancellationToken cancellationToken = new()) => Task.FromResult(true);

    public Task DownloadFileAsync(string filePath, Stream destination, CancellationToken cancellationToken = new()) =>
        throw new NotSupportedException();
}