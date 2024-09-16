﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum CompanySize
    {
        [EnumMember(Value ="Small")]
        Small,
        [EnumMember(Value = "Medium")]
        Medium,
        [EnumMember(Value = "Large")]
        Large
    }
}
