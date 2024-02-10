using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public static class Extensions
    {
    
        public static int CalculationAge(this DateTime dateTime)
        {
            var age = DateTime.Today.Year - dateTime.Year;
            if(dateTime.AddYears(age)>DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}
