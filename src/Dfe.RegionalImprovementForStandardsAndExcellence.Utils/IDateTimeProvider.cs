﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Utils
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
