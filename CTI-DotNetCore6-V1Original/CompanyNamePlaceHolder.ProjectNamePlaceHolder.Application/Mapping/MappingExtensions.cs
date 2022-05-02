using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Mapping
{
    public static class MappingExtensions
    {
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
}
