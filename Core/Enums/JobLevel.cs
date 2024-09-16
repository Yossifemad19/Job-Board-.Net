using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum JobLevel
    {
        [EnumMember(Value ="Intern")]
        Intern,
        [EnumMember(Value = "Junior")]
        Junior,
        [EnumMember(Value = "MidLevel")]

        MidLevel,
        [EnumMember(Value = "Senior")]
        Senior,
        [EnumMember(Value = "TeamLead")]
        TeamLead,
        [EnumMember(Value = "Cto")]
        Cto,
        [EnumMember(Value = "Architect")]
        Architect
    }
}
