using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string messages) : base(success: true, messages)
        {
        }
        public SuccessResult() : base(true)
        {            
        }
    }
}
