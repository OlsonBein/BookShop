namespace Store.BusinessLogicLayer.Common.Interfaces
{
    public interface IFileLoggerProvider
    {
        ILogger CreateLogger(string categoryName);

        void Dispose();
    }
}
