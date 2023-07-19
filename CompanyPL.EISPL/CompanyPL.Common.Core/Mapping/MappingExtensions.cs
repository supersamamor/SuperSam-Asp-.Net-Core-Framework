using AutoMapper;
using CompanyPL.Common.Core.Base.Models;

namespace CompanyPL.Common.Core.Mapping;

/// <summary>
/// Extends the functionality of <see cref="AutoMapper"/>.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Ignore <see cref="BaseEntity"/> properties during mapping.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="mapping"></param>
    /// <returns></returns>
    public static IMappingExpression<TSource, TDestination> IgnoreBaseEntityProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mapping)
    where TSource : BaseEntity
    where TDestination : BaseEntity
    {
        mapping.ForMember(e => e.Id, c => c.Ignore());
        mapping.ForMember(e => e.Entity, c => c.Ignore());
        mapping.ForMember(e => e.CreatedDate, c => c.Ignore());
        mapping.ForMember(e => e.CreatedBy, c => c.Ignore());
        mapping.ForMember(e => e.LastModifiedDate, c => c.Ignore());
        mapping.ForMember(e => e.LastModifiedBy, c => c.Ignore());

        return mapping;
    }
}
