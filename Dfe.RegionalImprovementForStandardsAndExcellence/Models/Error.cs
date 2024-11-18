﻿using System.Collections.Generic;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Models;

public class Error
{
   public string Key { get; set; }
   public string Message { get; set; }
   public List<string> InvalidInputs { get; set; } = new();
}
