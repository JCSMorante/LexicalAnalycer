using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzer
{
    public class Token
    {
        public readonly TokenEnum token;
        public readonly String sequence;
        public readonly int pos;

        public Token(TokenEnum token, String sequence, int pos)
        {
            this.token = token;
            this.sequence = sequence;
            this.pos = pos;
        }

        public Token clone()
        {
            return new Token(this.token, this.sequence, this.pos);
        }
    }
}
