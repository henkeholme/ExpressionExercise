using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start
{
    [Serializable]
    public class Expression
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }
        public bool IsValue { get; set; }
        public Operators Oper { get; set; }
        public Expression Ex1 { get; set; }
        public Expression Ex2 { get; set; }

        public Expression(Expression ex, Expression e2, Operators op)
        {
            Ex1 = ex;
            Ex2 = e2;
            Oper = op;
            IsValue = false;
        }

        public Expression(int num, int denom)
        {
            Denominator = denom;
            Numerator = num;
            IsValue = true;
        }
    }
}
