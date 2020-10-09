using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace techloops.Common
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute() : base(typeof(DateTime), DateTime.Now.ToString(), DateTime.Now.AddYears(1).ToString())
        { }
    }
}
