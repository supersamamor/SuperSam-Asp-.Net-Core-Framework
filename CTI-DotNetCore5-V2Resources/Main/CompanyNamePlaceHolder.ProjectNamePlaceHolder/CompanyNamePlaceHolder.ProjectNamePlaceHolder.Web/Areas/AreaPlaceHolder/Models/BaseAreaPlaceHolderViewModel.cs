using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models
{
    public record BaseAreaPlaceHolderViewModel
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
    }
}
