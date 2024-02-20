using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectNamePlaceHolder.Services.Infrastructure.Configurations
{
    public class DbContextOptions
    {
        public bool UseIsolationLevelReadUncommitted { get; set; }
        public int CommandTimeout { get; set; } = 30;
    }
}
