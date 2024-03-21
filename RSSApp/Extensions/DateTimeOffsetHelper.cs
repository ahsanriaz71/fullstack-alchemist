

namespace RSSApp.Extensions
{
    public class DateTimeOffsetHelper
    {
        // Method to parse a string to a DateTimeOffset object
        public static DateTimeOffset FromString(string offsetString)
        {
            DateTimeOffset offset;

            if (!DateTimeOffset.TryParse(offsetString, out offset))
            {
                offset = DateTimeOffset.Now;
            }

            return offset;
        }
    }
}
