using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetaPoco.Extensions.Linq
{
    public enum QueryOperatorType
    {
        Equal = 0,
        NotEqual = 1,
        GreaterThan = 2,
        LessThan = 3,
        GreaterThanOrEqual = 4,
        LessThanOrEqual = 5,
        Contains = 6,
        StartsWith = 7,
        EndsWith = 8
    }
}
