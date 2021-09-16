using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models
{
    public record BaseCommand()
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
    }
}
