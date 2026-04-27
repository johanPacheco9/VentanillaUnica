using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace VentanillaUnica.Generics.GenericDisplayName
{
    public static class EnumExtensions
    {
        public static string GetDisplayName<T>(this T value) where T : Enum
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
                return value.ToString();

            var attribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? value.ToString();
        }

        public static T? FromDisplayName<T>(this string displayName) where T : Enum
        {
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if (value.GetDisplayName() == displayName)
                    return value;
            }
            return default;
        }
    }
}