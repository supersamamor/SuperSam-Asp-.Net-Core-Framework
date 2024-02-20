using Cti.Core.Domain.Common;
using Cti.Core.Domain.Common.Contracts;

namespace ProjectNamePlaceHolder.Services.Domain.Entities
{
    public class MainModulePlaceHolder : AuditableEntity, IAggregateRoot
    {
        public string PrimaryKey { get; set; } = "";
        public string Code { get; set; }        
    }
}
