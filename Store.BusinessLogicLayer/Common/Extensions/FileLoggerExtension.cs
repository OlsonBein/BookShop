using Microsoft.Extensions.Logging;

namespace Store.BusinessLogicLayer.Common.Extensions
{
    public static class FileLoggerExtension
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }
    }
}
