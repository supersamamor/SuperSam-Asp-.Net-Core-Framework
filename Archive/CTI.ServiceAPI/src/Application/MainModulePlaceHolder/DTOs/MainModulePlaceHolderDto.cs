using AutoMapper;
using Cti.Core.Application.Common.Mappings;

namespace ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.DTOs
{
    public class MainModulePlaceHolderDto : IMapFrom<Domain.Entities.MainModulePlaceHolder>
    {
        public string ReferenceObject { get; set; } = "";
        public string LotUnitShareNumber { get; set; }
        public string MainModulePlaceHolderNumber { get; set; }
    }
}
