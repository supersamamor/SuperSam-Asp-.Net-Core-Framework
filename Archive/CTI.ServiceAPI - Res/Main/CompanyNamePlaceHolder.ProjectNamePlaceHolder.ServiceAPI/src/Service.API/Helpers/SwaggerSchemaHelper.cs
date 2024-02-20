using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.API.Helpers
{
    public class SwaggerSchemaHelper
    {
        private readonly Dictionary<string, List<string>> _schemaNameRepetition = new Dictionary<string, List<string>>();

        // borrowed from https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/95cb4d370e08e54eb04cf14e7e6388ca974a686e/src/Swashbuckle.AspNetCore.SwaggerGen/SchemaGenerator/SchemaGeneratorOptions.cs#L44
        private string DefaultSchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType) return modelType.Name.Replace("[]", "Array");

            var prefix = modelType.GetGenericArguments()
                .Select(genericArg => DefaultSchemaIdSelector(genericArg))
                .Aggregate((previous, current) => previous + current);

            return prefix + modelType.Name.Split('`').First();
        }

        public string GetSchemaId(Type modelType)
        {
            string id = DefaultSchemaIdSelector(modelType);

            if (!_schemaNameRepetition.ContainsKey(id))
                _schemaNameRepetition.Add(id, new List<string>());

            var modelNameList = _schemaNameRepetition[id];
            var fullName = modelType.FullName ?? "";
            if (!string.IsNullOrEmpty(fullName) && !modelNameList.Contains(fullName))
                modelNameList.Add(fullName);

            int index = modelNameList.IndexOf(fullName);

            //Replace id Suffix if ended with Query or Command
            id = id.Replace("Query", "Request");
            id = id.Replace("Command", "Request");

            return $"{id}{(index >= 1 ? index.ToString() : "")}";
        }
    }
}
