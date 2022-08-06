using AutoMapper;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Commands;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTI.TenantSales.Scheduler.Mapping
{
    public class SalesProcessingProfile : Profile
    {
        public SalesProcessingProfile()
        {
            CreateMap<TenantPOSSalesState, SalesItem>();
            CreateMap<SalesItem, EditFailedTenantPOSSalesCommand>();
            CreateMap<SalesItem, AddTenantPOSSalesCommand>();          
        }
    }
}
