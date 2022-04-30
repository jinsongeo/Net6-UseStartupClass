using Newtonsoft.Json;

namespace Net6_UseStartupClass.Code.JsonSerializer
{
    public class ApiJsonSerializer : IApiJsonSerializer
    {
        private static JsonSerializerSettings _serializerSettings;

        public static void Configure(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
        }

        public string Serialize(object model)
        {
            ValidateSerializerSettings();

            return JsonConvert.SerializeObject(model, Formatting.Indented, _serializerSettings);
        }

        private static void ValidateSerializerSettings()
        {
            if (_serializerSettings == null)
                throw new InvalidOperationException("Serializer settings aren't configured.");
        }

        public T GetSettings<T>()
        {
            return (T)Convert.ChangeType(_serializerSettings, typeof(T));
        }
    }
}
