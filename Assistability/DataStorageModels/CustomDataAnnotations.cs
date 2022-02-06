using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataStorageModels
{
    class CustomDataAnnotations
    {
        public class CurrentDateAttribute : ValidationAttribute
        {
            public CurrentDateAttribute()
            {

            }
            public override bool IsValid(object value)
            {
                var dt = (DateTime)value;
                if (dt >= DateTime.Today)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
