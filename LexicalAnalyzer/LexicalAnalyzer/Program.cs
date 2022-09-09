using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LexicalAnalyzer
{
    public class Program
    {
        static void Main(string[] args)
        {
            string file = ReadFile();
            file = ClearComments(file);
            file = ClearNewLine(file);
            Tokenizer tokenizer = new Tokenizer();
            tokenizer.add("Json|Table|string|ln|sqrt", TokenEnum.DataType);
            tokenizer.add("\\w+[(]", TokenEnum.Function);
            tokenizer.add("\"([^\"]*)\"|'([^']*)'", TokenEnum.Variable);
            tokenizer.add("[a-zA-Z][a-zA-Z0-9_]*", TokenEnum.Variable);
            tokenizer.add("[^A-Za-z0-9]", TokenEnum.Operators);

            try
            {
                tokenizer.tokenize(file);

                foreach(Token tok in tokenizer.getTokens())
                {
                    Console.WriteLine("" + tok.token + " " + tok.sequence);
                }
                Console.ReadLine();
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static string ClearNewLine(string file) 
        {
            file = file.Trim();
            file = file.Replace("\n", " ");
            file = file.Replace(System.Environment.NewLine, " ");
            file = file.Replace("  ", " ");
            return file;
        }
        private static string ClearComments(string file) 
        {
            if (file != null && file != string.Empty)
            {
                Regex commentBlock = new Regex("\\/\\*(\\*(?!\\/)|[^*])*\\*\\/");

                MatchCollection matches = commentBlock.Matches(file);


                foreach (Match match in matches)
                {
                    foreach (var group in match.Groups)
                    {
                        if (group.ToString().Length > 1 && group.ToString().Substring(0, 2).Equals("/*"))
                        {
                            file = file.Replace(group.ToString(), string.Empty);
                        }
                    }       
                }
                using (StringReader reader = new StringReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        int index = line.IndexOf("//");
                        if(index >= 0)
                            file = file.Replace(line.Substring(index, line.Length), string.Empty);
                    }
                }
                return file;
            }
            else
                return file;
            

        }

        private static string ReadFile()
        {
            try
            {
                string stringFile = string.Empty;
                StreamReader sr = new StreamReader("C:\\new_language.tdb");
                String line = sr.ReadLine();
                while (line != null)
                {
                    stringFile += $"{line} \n ";
                    line = sr.ReadLine();
                    
                }
                sr.Close();
                //Console.ReadLine();
                return stringFile;
            }
            catch (Exception)
            {
                return string.Empty;
            }
            
        }
    }
}
