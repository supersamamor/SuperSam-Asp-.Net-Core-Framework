using OfficeOpenXml;
using OfficeOpenXml.Attributes;
using OfficeOpenXml.Table;
using System.Reflection;

namespace CTI.TenantSales.ExcelProcessor.Extensions
{
    public static class EpplusExtensions
    {
        public static ExcelRangeBase LoadFromCollectionFiltered<T>(this ExcelRangeBase @this, IEnumerable<T> collection)
        {
            MemberInfo[] membersToInclude = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !Attribute.IsDefined(p, typeof(EpplusIgnore)))
                .ToArray();

            return @this.LoadFromCollection<T>(collection, true,
                TableStyles.None,
                BindingFlags.Instance | BindingFlags.Public,
                membersToInclude);
        }

    }
}
