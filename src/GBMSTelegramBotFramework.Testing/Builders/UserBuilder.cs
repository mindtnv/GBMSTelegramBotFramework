using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Testing.Builders;

public class UserBuilder : BuilderBase
{
    private bool _canJoinGroups;
    private bool _canReadAllGroupMessages;
    private string _firstName;
    private int _id;
    private bool _isBot;
    private string _languageCode;
    private string _lastName;
    private bool _supportsInlineQueries;
    private string _username;

    public UserBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public UserBuilder IsBot(bool isBot)
    {
        _isBot = isBot;
        return this;
    }

    public UserBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public UserBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public UserBuilder WithUsername(string username)
    {
        _username = username;
        return this;
    }

    public UserBuilder WithLanguageCode(string languageCode)
    {
        _languageCode = languageCode;
        return this;
    }

    public UserBuilder CanJoinGroups(bool canJoinGroups)
    {
        _canJoinGroups = canJoinGroups;
        return this;
    }

    public UserBuilder CanReadAllGroupMessages(bool canReadAllGroupMessages)
    {
        _canReadAllGroupMessages = canReadAllGroupMessages;
        return this;
    }

    public UserBuilder SupportsInlineQueries(bool supportsInlineQueries)
    {
        _supportsInlineQueries = supportsInlineQueries;
        return this;
    }

    public User Build() =>
        new User
        {
            Id = _id,
            IsBot = _isBot,
            FirstName = _firstName,
            LastName = _lastName,
            Username = _username,
            LanguageCode = _languageCode,
            CanJoinGroups = _canJoinGroups,
            CanReadAllGroupMessages = _canReadAllGroupMessages,
            SupportsInlineQueries = _supportsInlineQueries,
        };
}