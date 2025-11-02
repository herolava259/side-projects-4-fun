using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class CalculatorHistory
    {
        public int Id { get; set; }
        public int FirstOperand { get; set; }

        public int LastOperand { get; set; }

        public string AlgOperator { get; set; }

        public float Result { get; set; }

    }
}