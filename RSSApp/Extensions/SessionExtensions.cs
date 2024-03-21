using System.Text.Json; // Importing the System.Text.Json namespace for JSON serialization/deserialization

namespace RSSApp.Extensions
{
    // Extension methods for ISession interface
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            // Serialize the object to JSON string and store it in session
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            // Deserialize JSON string to object of type T
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}

