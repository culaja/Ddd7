namespace Ddd7.Common
{
    public interface IMaybe
    {
        bool HasValue { get; }

        bool HasNoValue { get; }
    }
}