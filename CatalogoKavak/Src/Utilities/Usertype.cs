using System.Text.Json.Serialization;

namespace CatalogoKavak.Src.Utilities
{
    public class Usertype
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum TypeUser
        {
            REGULAR,
            ADMIN,
            DEV
        }
    }
}
