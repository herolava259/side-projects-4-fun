using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CalculationDTO
    {
        public int FirstOperand { get; set; }

        public int LastOperand { get; set; }

        public string AlgOperator { get; set; }
    }
}