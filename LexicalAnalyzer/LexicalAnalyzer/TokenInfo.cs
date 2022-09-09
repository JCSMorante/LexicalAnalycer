using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LexicalAnalyzer
{
    public class TokenInfo
    {
        public readonly Regex regex;
        public readonly TokenEnum token;
        public TokenInfo(Regex regex, TokenEnum token)
        {
            this.regex = regex;
            this.token = token;
        }
    }
}
