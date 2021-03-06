using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var methodName = nameof(IMapFrom<object>.Mapping);
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod(methodName) ?? type.GetInterface($"{nameof(IMapFrom<object>)}`1").GetMethod(methodName); 
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}