namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdateContextFeaturesCollectionStore
{
    IFeaturesCollection GetFeaturesCollection(long id);
}