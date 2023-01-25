namespace MongoDBDemoApp.Core.Util;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}