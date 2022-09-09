using LexicalAnalyzer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

public class Tokenizer
{

    private List<TokenInfo> tokenInfos = new();
    private List<Token> tokens = new();
    public Tokenizer()
    {
        tokenInfos = new();
        tokens = new();
    }

    public void tokenize(String str)
    {
        String s = str.Trim();
        tokens.Clear();
        var checkFinish = tokenInfos.Find(f => f.token == TokenEnum.Variable);
        while (!s.Equals(""))
        {
            bool match = false;
            foreach (TokenInfo info in tokenInfos)
            {
                MatchCollection m = info.regex.Matches(s);

                while (m.Count > 0)
                {
                    Match matchSelect = m[0];
                    if (matchSelect.Groups != null)
                    {
                        Match group = (Match)matchSelect.Groups[0];
                        s = AddTokens(group, info, s);
                        m = info.regex.Matches(s);
                    }
                }

            }
        }

    }


    public string AddTokens(Match group, TokenInfo info, string s)
    {
        if (s.Contains(group.ToString(), StringComparison.CurrentCulture))
        {
            s = s.Substring(0, group.Index).Trim() + " " + s.Substring(group.Index + group.Length).Trim();
            tokens.Add(new Token(info.token, info.token == TokenEnum.Function ? group.ToString().Substring(0, group.Length-1) : group.ToString(), group.Index));
        }
        return s.Trim();
    }

    public List<Token> getTokens()
    {
        return tokens;
    }

    public void add(String regex, TokenEnum token)
    {
        tokenInfos.Add(new TokenInfo(new("(" + regex + ")"), token));
    }

}
