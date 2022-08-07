using AutoMapper;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.ExcelProcessor.Models;

namespace CTI.TenantSales.Web.Areas.Reports.Mapping
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<TenantState, TenantDailySales>()
                .ForPath(e => e.ProjectName, o => o.MapFrom(s => s.Project!.Name))
                .ForPath(e => e.TenantName, o => o.MapFrom(s => s.Name))
                .ForPath(e => e.TenantCode, o => o.MapFrom(s => s.Code));
            CreateMap<TenantPOSState, TenantPOSDailySales>();
            CreateMap<TenantPOSSalesState, DailySales>();


            CreateMap<TenantState, PdfGenerator.Models.TenantDailySales>()
               .ForPath(e => e.ProjectName, o => o.MapFrom(s => s.Project!.Name))
               .ForPath(e => e.TenantName, o => o.MapFrom(s => s.Name))
               .ForPath(e => e.TenantCode, o => o.MapFrom(s => s.Code));
            CreateMap<TenantPOSState, PdfGenerator.Models.TenantPOSDailySales>();
            CreateMap<TenantPOSSalesState, PdfGenerator.Models.DailySales>();

            CreateMap<ThemeState, PdfGenerator.Models.Theme>();
        }
    }
}
