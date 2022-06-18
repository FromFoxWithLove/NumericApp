using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Characters
    {
        public static readonly IEnumerable<char> Numbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static readonly IEnumerable<char> Operators = new List<char> { '+', '-', '/', '*', '^' };

        public static readonly IEnumerable<char> Parentheses = new List<char> { '(', ')' };

        public static readonly IEnumerable<char> Points = new List<char> { '.', ',' };
    }
}
