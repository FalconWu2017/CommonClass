namespace CommonClass.JsonSettings
{
    public interface IGetSettins<T> where T : class
    {
        T GetSettingsObject();
    }
}