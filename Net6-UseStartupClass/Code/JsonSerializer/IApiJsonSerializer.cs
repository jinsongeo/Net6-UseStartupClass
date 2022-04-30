namespace Net6_UseStartupClass.Code.JsonSerializer
{
    public interface IApiJsonSerializer
    {
        string Serialize(object model);

        T GetSettings<T>();
    }
}
