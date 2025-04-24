using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HackathonHealthMed.Autenticacao.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString())
                                      .FirstOrDefault();

            if (memberInfo == null) return enumValue.ToString();

            var displayAttribute = memberInfo
                .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumValue.ToString();
        }

        public static TEnum? GetEnumByDisplayName<TEnum>(string displayName) where TEnum : struct, Enum
        {
            foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute<DisplayAttribute>();
                if (attribute?.Name?.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return (TEnum)field.GetValue(null)!;
                }
            }

            return null;
        }
    }
}
