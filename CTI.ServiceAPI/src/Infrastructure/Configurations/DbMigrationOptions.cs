using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectNamePlaceHolder.Services.Infrastructure.Configurations
{
    public class DbMigrationOptions
    {
        public bool ApplyDbMigration { get; set; } = false;
        public bool ApplySeed { get; set; } = false;
    }
}
