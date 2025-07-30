namespace Framework
{
    public static class EnumExtensionMethods
    {
        public static string GetEnumDisplayName(this Enum enumType)
        {
            var memberInfo = enumType.GetType().GetMember(enumType.ToString());
            if (memberInfo.Length > 0)
            {
                var displayAttribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }
            return enumType.ToString(); // اگه DisplayAttribute نبود، نام خام enum رو برگردون
        }
    }
}
