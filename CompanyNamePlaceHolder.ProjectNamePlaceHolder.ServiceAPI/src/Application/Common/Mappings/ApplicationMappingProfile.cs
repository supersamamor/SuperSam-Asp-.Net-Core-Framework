using AutoMapper;
using Cti.Core.Application.Common.Mappings;
using System;
using System.Linq;
using System.Reflection;

namespace ProjectNamePlaceHolder.Services.Application.Common.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && (
                        i.GetGenericTypeDefinition() == typeof(IMapFrom<>)
                        || i.GetGenericTypeDefinition() == typeof(IMapTo<>)
                        )))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("MapFrom")
                    ?? type.GetInterface("IMapFrom`1")?.GetMethod("MapFrom");

                methodInfo?.Invoke(instance, new object[] { this });

                methodInfo = type.GetMethod("MapTo")
                    ?? type.GetInterface("IMapTo`1")?.GetMethod("MapTo");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}