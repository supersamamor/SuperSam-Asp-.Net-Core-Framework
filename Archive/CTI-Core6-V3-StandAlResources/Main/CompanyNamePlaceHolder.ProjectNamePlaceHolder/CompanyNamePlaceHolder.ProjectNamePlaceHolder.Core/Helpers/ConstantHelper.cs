using System.Reflection;
namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Helpers
{
    public static class ConstantHelper
    {
        public static string? GetPropertyNameByValue(Type type, string? value)
        {            
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.GetValue(null)?.ToString() == value)
                {
                    return field.Name;
                }
            }
            return value;
        }
    }
}
