using System.Globalization;

namespace CTI.TenantSales.PdfGenerator.Models
{
    public class SalesGrowthDataModel
    {
        public SalesGrowthDataModel(int year, int month, IList<Theme> salesGrowthData)
        {
            this.ThemeList = salesGrowthData;
            Year = year;
            Month = month;
        }
        public IList<Theme> ThemeList { get; set; } = new List<Theme>();
        public int Year { get; set; }
        public int PrevYear
        {
            get
            {
                return this.Year - 1;
            }
        }
        public int Month { get; set; }
        public string MonthName
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.Month);
            }
        }
        public string ProjectName
        {
            get
            {               
                if (this.ThemeList?.FirstOrDefault()?.ClassificationList?.FirstOrDefault()?.CategoryList?.FirstOrDefault()?.TenantList?.FirstOrDefault()?.Project != null)
                {
                    return ThemeList!.FirstOrDefault()!.ClassificationList!.FirstOrDefault()!.CategoryList!.FirstOrDefault()!.TenantList!.FirstOrDefault()!.Project!.Name;
                }
                return "";
            }
        }
        public HideMonthColumn HideColumn
        {
            get
            {
                return new HideMonthColumn(this.Month);
            }
        }
        public ReportSalesGrowthPerformanceMonth SalesGrowthGrandSummary
        {
            get
            {
                //Average Per Tenant Class Group               
                ReportSalesGrowthPerformanceMonth salesGrowthSummary = new() { Label = "GRAND TOTAL :" };
                int divisor = 0;
                foreach (var item in this.ThemeList)
                {
                    foreach (var classItem in item.ClassificationList!)
                    {
                        if (classItem.SalesGrowthSummary.Month != 0)
                        {
                            salesGrowthSummary.Area += classItem.SalesGrowthSummary.Area;
                            salesGrowthSummary.PrevYearYTD += classItem.SalesGrowthSummary.PrevYearYTD;
                            salesGrowthSummary.CurrentYearYTD += classItem.SalesGrowthSummary.CurrentYearYTD;
                            salesGrowthSummary.PercentVariance += classItem.SalesGrowthSummary.PercentVariance;
                            salesGrowthSummary.PercRentAirConCAMCOverCurrentMonthSales += classItem.SalesGrowthSummary.PercRentAirConCAMCOverCurrentMonthSales;
                            salesGrowthSummary.PercRentAirConCAMCOverCurrentYTDSales += classItem.SalesGrowthSummary.PercRentAirConCAMCOverCurrentYTDSales;
                            salesGrowthSummary.Jan += classItem.SalesGrowthSummary.Jan;
                            salesGrowthSummary.Feb += classItem.SalesGrowthSummary.Feb;
                            salesGrowthSummary.Mar += classItem.SalesGrowthSummary.Mar;
                            salesGrowthSummary.Apr += classItem.SalesGrowthSummary.Apr;
                            salesGrowthSummary.May += classItem.SalesGrowthSummary.May;
                            salesGrowthSummary.Jun += classItem.SalesGrowthSummary.Jun;
                            salesGrowthSummary.Jul += classItem.SalesGrowthSummary.Jul;
                            salesGrowthSummary.Aug += classItem.SalesGrowthSummary.Aug;
                            salesGrowthSummary.Sep += classItem.SalesGrowthSummary.Sep;
                            salesGrowthSummary.Oct += classItem.SalesGrowthSummary.Oct;
                            salesGrowthSummary.Nov += classItem.SalesGrowthSummary.Nov;
                            salesGrowthSummary.Dec += classItem.SalesGrowthSummary.Dec;
                            salesGrowthSummary.Month = classItem.SalesGrowthSummary.Month;
                            divisor++;
                        }
                    }
                }
                divisor = divisor == 0 ? 1 : divisor;
                salesGrowthSummary.PrevYearYTD /= divisor;
                salesGrowthSummary.CurrentYearYTD /= divisor;
                salesGrowthSummary.PercentVariance /= divisor;
                salesGrowthSummary.PercRentAirConCAMCOverCurrentMonthSales /= divisor;
                salesGrowthSummary.PercRentAirConCAMCOverCurrentYTDSales /= divisor;
                salesGrowthSummary.Jan /= divisor;
                salesGrowthSummary.Feb /= divisor;
                salesGrowthSummary.Mar /= divisor;
                salesGrowthSummary.Apr /= divisor;
                salesGrowthSummary.May /= divisor;
                salesGrowthSummary.Jun /= divisor;
                salesGrowthSummary.Jul /= divisor;
                salesGrowthSummary.Aug /= divisor;
                salesGrowthSummary.Sep /= divisor;
                salesGrowthSummary.Oct /= divisor;
                salesGrowthSummary.Nov /= divisor;
                salesGrowthSummary.Dec /= divisor;
                return salesGrowthSummary;
            }
        }
    }
    public class Theme
    {
        public string Name { get; set; } = "";
        public bool DisplaySalesGrowthByTheme
        {
            get
            {
                return (this.ClassificationList?.SelectMany(l => l.CategoryList!)?.SelectMany(l => l.TenantList!)?.SelectMany(l => l.ReportSalesGrowthPerformanceMonthList!)?.Any() == true);               
            }
        }
        public IList<Classification>? ClassificationList { get; set; }
    }
    public class Classification
    {
        public string Name { get; set; } = "";
        public IList<Category>? CategoryList { get; set; }
        public bool Show
        {
            get
            {
                return this.SalesGrowthSummary.Show;
            }
        }
        public ReportSalesGrowthPerformanceMonth SalesGrowthSummary
        {
            get
            {
                //Average Per Tenant Class Group               
                ReportSalesGrowthPerformanceMonth salesGrowthSummary = new() { Label = "TOTAL " + Name + ":" };
                int divisor = 0;
                foreach (var item in this.CategoryList!)
                {
                    if (item.SalesGrowthSummary != null)
                    {
                        salesGrowthSummary.Area += item.SalesGrowthSummary.Area;
                        salesGrowthSummary.PrevYearYTD += item.SalesGrowthSummary.PrevYearYTD;
                        salesGrowthSummary.CurrentYearYTD += item.SalesGrowthSummary.CurrentYearYTD;
                        salesGrowthSummary.PercentVariance += item.SalesGrowthSummary.PercentVariance;
                        salesGrowthSummary.PercRentAirConCAMCOverCurrentMonthSales += item.SalesGrowthSummary.PercRentAirConCAMCOverCurrentMonthSales;
                        salesGrowthSummary.PercRentAirConCAMCOverCurrentYTDSales += item.SalesGrowthSummary.PercRentAirConCAMCOverCurrentYTDSales;
                        salesGrowthSummary.Jan += item.SalesGrowthSummary.Jan;
                        salesGrowthSummary.Feb += item.SalesGrowthSummary.Feb;
                        salesGrowthSummary.Mar += item.SalesGrowthSummary.Mar;
                        salesGrowthSummary.Apr += item.SalesGrowthSummary.Apr;
                        salesGrowthSummary.May += item.SalesGrowthSummary.May;
                        salesGrowthSummary.Jun += item.SalesGrowthSummary.Jun;
                        salesGrowthSummary.Jul += item.SalesGrowthSummary.Jul;
                        salesGrowthSummary.Aug += item.SalesGrowthSummary.Aug;
                        salesGrowthSummary.Sep += item.SalesGrowthSummary.Sep;
                        salesGrowthSummary.Oct += item.SalesGrowthSummary.Oct;
                        salesGrowthSummary.Nov += item.SalesGrowthSummary.Nov;
                        salesGrowthSummary.Dec += item.SalesGrowthSummary.Dec;
                        salesGrowthSummary.Month = item.SalesGrowthSummary.Month;
                        salesGrowthSummary.Show = item.SalesGrowthSummary.Month != 0;
                        divisor++;
                    }
                }
                divisor = divisor == 0 ? 1 : divisor;
                salesGrowthSummary.PrevYearYTD /= divisor;
                salesGrowthSummary.CurrentYearYTD /= divisor;
                salesGrowthSummary.PercentVariance /= divisor;
                salesGrowthSummary.PercRentAirConCAMCOverCurrentMonthSales /= divisor;
                salesGrowthSummary.PercRentAirConCAMCOverCurrentYTDSales /= divisor;
                salesGrowthSummary.Jan /= divisor;
                salesGrowthSummary.Feb /= divisor;
                salesGrowthSummary.Mar /= divisor;
                salesGrowthSummary.Apr /= divisor;
                salesGrowthSummary.May /= divisor;
                salesGrowthSummary.Jun /= divisor;
                salesGrowthSummary.Jul /= divisor;
                salesGrowthSummary.Aug /= divisor;
                salesGrowthSummary.Sep /= divisor;
                salesGrowthSummary.Oct /= divisor;
                salesGrowthSummary.Nov /= divisor;
                salesGrowthSummary.Dec /= divisor;
                return salesGrowthSummary;
            }
        }
    }
    public class Category
    {
        public string Name { get; set; } = "";
        public IList<Tenant>? TenantList { get; set; }
        public bool Show
        {
            get
            {
                return this.SalesGrowthSummary.Show;
            }
        }
        public ReportSalesGrowthPerformanceMonth SalesGrowthSummary
        {
            get
            {
                //Average Per Tenant Category Group
                ReportSalesGrowthPerformanceMonth salesGrowthSummary = new() { Label = "SUB-TOTAL " + this.Name + ": AREA; AVE. SALES, EFF. RENT & RENT/SALES" };
                int divisor = 0;
                foreach (var item in this.TenantList!)
                {
                    if (item.SalesGrowth != null)
                    {
                        salesGrowthSummary.Area += item.SalesGrowth.Area;
                        salesGrowthSummary.PrevYearYTD += item.SalesGrowth.PrevYearYTD;
                        salesGrowthSummary.CurrentYearYTD += item.SalesGrowth.CurrentYearYTD;
                        salesGrowthSummary.PercentVariance += item.SalesGrowth.PercentVariance;
                        salesGrowthSummary.PercRentAirConCAMCOverCurrentMonthSales += item.SalesGrowth.PercRentAirConCAMCOverCurrentMonthSales;
                        salesGrowthSummary.PercRentAirConCAMCOverCurrentYTDSales += item.SalesGrowth.PercRentAirConCAMCOverCurrentYTDSales;
                        salesGrowthSummary.Jan += item.SalesGrowth.Jan;
                        salesGrowthSummary.Feb += item.SalesGrowth.Feb;
                        salesGrowthSummary.Mar += item.SalesGrowth.Mar;
                        salesGrowthSummary.Apr += item.SalesGrowth.Apr;
                        salesGrowthSummary.May += item.SalesGrowth.May;
                        salesGrowthSummary.Jun += item.SalesGrowth.Jun;
                        salesGrowthSummary.Jul += item.SalesGrowth.Jul;
                        salesGrowthSummary.Aug += item.SalesGrowth.Aug;
                        salesGrowthSummary.Sep += item.SalesGrowth.Sep;
                        salesGrowthSummary.Oct += item.SalesGrowth.Oct;
                        salesGrowthSummary.Nov += item.SalesGrowth.Nov;
                        salesGrowthSummary.Dec += item.SalesGrowth.Dec;
                        salesGrowthSummary.Month = item.SalesGrowth.Month;
                        salesGrowthSummary.Show = true;
                        divisor++;
                    }
                }
                divisor = divisor == 0 ? 1 : divisor;
                salesGrowthSummary.PrevYearYTD /= divisor;
                salesGrowthSummary.CurrentYearYTD /= divisor;
                salesGrowthSummary.PercentVariance /= divisor;
                salesGrowthSummary.PercRentAirConCAMCOverCurrentMonthSales /= divisor;
                salesGrowthSummary.PercRentAirConCAMCOverCurrentYTDSales /= divisor;
                salesGrowthSummary.Jan /= divisor;
                salesGrowthSummary.Feb /= divisor;
                salesGrowthSummary.Mar /= divisor;
                salesGrowthSummary.Apr /= divisor;
                salesGrowthSummary.May /= divisor;
                salesGrowthSummary.Jun /= divisor;
                salesGrowthSummary.Jul /= divisor;
                salesGrowthSummary.Aug /= divisor;
                salesGrowthSummary.Sep /= divisor;
                salesGrowthSummary.Oct /= divisor;
                salesGrowthSummary.Nov /= divisor;
                salesGrowthSummary.Dec /= divisor;
                return salesGrowthSummary;
            }
        }
    }
    public class Tenant
    {
        public IList<ReportSalesGrowthPerformanceMonth>? ReportSalesGrowthPerformanceMonthList { get; set; }
        public IList<TenantLotMonthYear>? TenantLotMonthYearList { get; set; }
        public Project? Project { get; set; }
        public ReportSalesGrowthPerformanceMonth? SalesGrowth
        {
            get
            {
                return this.ReportSalesGrowthPerformanceMonthList?.FirstOrDefault();
            }
        }
        public bool Show
        {
            get
            {
                //Will Hide Row if no Sales Growth Data
                bool show = false;
                foreach (var sgItem in ReportSalesGrowthPerformanceMonthList!)
                {
                    show = true;
                    break;
                }
                return show;
            }
        }
        public string Name { get; set; } = "";
        public DateTime OpeningDate
        {
            get; set;
        }
        public string OpeningDateFormatted
        {
            get
            {
                string openingDate = "";
                if (this.ReportSalesGrowthPerformanceMonthList != null && this.ReportSalesGrowthPerformanceMonthList.Count > 0)
                {
                    openingDate = this.OpeningDate.Year == this.ReportSalesGrowthPerformanceMonthList!.FirstOrDefault()!.Year ? this.OpeningDate.ToString("MM/dd/yyyy") : "";
                }
                return openingDate;
            }
        }
    }
    public class ReportSalesGrowthPerformanceMonth
    {
        public string Label { get; set; } = "";
        public bool Show { get; set; }
        public decimal Area { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal PrevYearYTD { get; set; }
        public decimal CurrentYearYTD { get; set; }
        public decimal PercentVariance { get; set; }
        public decimal PercRentAirConCAMCOverCurrentMonthSales { get; set; }
        public decimal PercRentAirConCAMCOverCurrentYTDSales { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
        public string AreaFormatted
        {
            get
            {
                return this.Area == 0 ? "-" : this.Area.ToString("##,##.00");
            }
        }
        public string PrevYearYTDFormatted
        {
            get
            {
                return this.PrevYearYTD == 0 ? "-" : this.PrevYearYTD.ToString("##,##.00");
            }
        }
        public string CurrentYearYTDFormatted
        {
            get
            {
                return this.CurrentYearYTD == 0 ? "-" : this.CurrentYearYTD.ToString("##,##.00");
            }
        }
        public string PercentVarianceFormatted
        {
            get
            {
                return this.PercentVariance == 0 ? "-" : this.PercentVariance.ToString("##,##.00");
            }
        }
        public string PercRentAirConCAMCOverCurrentMonthSalesFormatted
        {
            get
            {
                return this.PercRentAirConCAMCOverCurrentMonthSales == 0 ? "-" : this.PercRentAirConCAMCOverCurrentMonthSales.ToString("##,##.00");
            }
        }
        public string PercRentAirConCAMCOverCurrentYTDSalesFormatted
        {
            get
            {
                return this.PercRentAirConCAMCOverCurrentYTDSales == 0 ? "-" : this.PercRentAirConCAMCOverCurrentYTDSales.ToString("##,##.00");
            }
        }
        public string JanFormatted
        {
            get
            {
                return this.Jan == 0 ? "-" : this.Jan.ToString("##,##.00");
            }
        }
        public string FebFormatted
        {
            get
            {
                return this.Feb == 0 ? "-" : this.Feb.ToString("##,##.00");
            }
        }
        public string MarFormatted
        {
            get
            {
                return this.Mar == 0 ? "-" : this.Mar.ToString("##,##.00");
            }
        }
        public string AprFormatted
        {
            get
            {
                return this.Apr == 0 ? "-" : this.Apr.ToString("##,##.00");
            }
        }
        public string MayFormatted
        {
            get
            {
                return this.May == 0 ? "-" : this.May.ToString("##,##.00");
            }
        }
        public string JunFormatted
        {
            get
            {
                return this.Jun == 0 ? "-" : this.Jun.ToString("##,##.00");
            }
        }
        public string JulFormatted
        {
            get
            {
                return this.Jul == 0 ? "-" : this.Jul.ToString("##,##.00");
            }
        }
        public string AugFormatted
        {
            get
            {
                return this.Aug == 0 ? "-" : this.Aug.ToString("##,##.00");
            }
        }
        public string SepFormatted
        {
            get
            {
                return this.Sep == 0 ? "-" : this.Sep.ToString("##,##.00");
            }
        }
        public string OctFormatted
        {
            get
            {
                return this.Oct == 0 ? "-" : this.Oct.ToString("##,##.00");
            }
        }
        public string NovFormatted
        {
            get
            {
                return this.Nov == 0 ? "-" : this.Nov.ToString("##,##.00");
            }
        }
        public string DecFormatted
        {
            get
            {
                return this.Dec == 0 ? "-" : this.Dec.ToString("##,##.00");
            }
        }
        public HideMonthColumn HideColumn
        {
            get
            {
                return new HideMonthColumn(this.Month);
            }
        }

    }
    public class TenantLotMonthYear
    {
        public decimal Area { get; set; }
        public string AreaFormatted
        {
            get
            {
                return this.Area == 0 ? "-" : this.Area.ToString("##,##.00");
            }
        }
    }
    public class Project
    {
        public string Name { get; set; } = "";
    }
    public class HideMonthColumn
    {
        private int Month { get; set; }
        public HideMonthColumn(int month)
        {
            this.Month = month;
        }
        public bool HideJan
        {
            get
            {
                return (1 > Month);
            }
        }
        public bool HideFeb
        {
            get
            {
                return (2 > Month);
            }
        }
        public bool HideMar
        {
            get
            {
                return (3 > Month);
            }
        }
        public bool HideApr
        {
            get
            {
                return (4 > Month);
            }
        }
        public bool HideMay
        {
            get
            {
                return (5 > Month);
            }
        }
        public bool HideJun
        {
            get
            {
                return (6 > Month);
            }
        }
        public bool HideJul
        {
            get
            {
                return (7 > Month);
            }
        }
        public bool HideAug
        {
            get
            {
                return (8 > Month);
            }
        }
        public bool HideSep
        {
            get
            {
                return (9 > Month);
            }
        }
        public bool HideOct
        {
            get
            {
                return (10 > Month);
            }
        }
        public bool HideNov
        {
            get
            {
                return (11 > Month);
            }
        }
        public bool HideDec
        {
            get
            {
                return (12 > Month);
            }
        }
    }
}
