using dflConverter.impl.DFRef;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace dflConverter.impl.Parser
{
    public static class CodeParser
    {
        public static string midSubstring(this string str, int a, int b)
        {
            return str.Substring(a+1, a - b-1);
        }

        public static List<string> SplitArguments(this string str)
        {
            List<string> strings = new List<string>();
            str = str.Remove(0, 1).Remove(str.Length - 2, 1);

            string substr;

            char openBracket = '(';
            char closeBracket = ')';

            int bracketsOpened = 0;
            int index = 0;
            int prevSplit = 0;
            foreach (char c in str)
            {
                if (c.Equals(openBracket)) bracketsOpened++;
                if (c.Equals(closeBracket)) bracketsOpened--;
                if (bracketsOpened == 0)
                {
                    if (c.Equals(','))
                    {
                        substr = str.Substring(prevSplit, index - prevSplit);
                        if (substr.StartsWith(",")) substr = substr.Substring(1);
                        strings.Add(substr.Trim());
                        prevSplit = index;
                    }
                }
                index++;
            }
            substr = str.Substring(prevSplit);
            if (substr.StartsWith(",")) substr = substr.Substring(1);
            strings.Add(substr.Trim());
            return strings;
        }

        public static List<DFLBlock> ParseCode(string code)
        {
            List<DFLBlock> result = new List<DFLBlock>();

            List<string> codeLines = code.Split('\n').ToList();
            codeLines.ForEach(line => HandleLine(line, ref result));

            return result;
        }

        private static int FindPartnerBracket(string txt)
        {
            char openBracket = txt[0];
            char closeBracket = txt[0];
            if (openBracket == '{' || openBracket == '}') closeBracket = '}';
            if (openBracket == '(' || openBracket == ')') closeBracket = ')';

            int bracketsOpened = 0;
            int index = 0;
            foreach(char c in txt)
            {
                if (c.Equals(openBracket)) bracketsOpened++;
                if (c.Equals(closeBracket)) bracketsOpened--;
                if (bracketsOpened == 0) return index;
                index++;
            }
            return 0;
        }
        private static string FindKeyword(string line)
        {
            if (line.Length == 0) return null;
            char firstChar = line[0];
            if (firstChar.Equals('"') || firstChar.Equals('('))
            {
                // mid-line
            } else
            {
                //line.IndexOf('.', )
            }

            return line[0]+"";
        }

        private static void HandleLine(string line, ref List<DFLBlock> result)
        {
            line = line.Trim();

            string firstWord = line.Split(' ', '.', '(', ')')[0];
            string keyword = FindKeyword(line);

            Console.WriteLine(line);

            switch (firstWord)
            {
                case "event":
                    EventCase(line, ref result);
                    break;
                case "func":
                    FuncCase(line, ref result);
                    break;
                case "process":
                    ProcessCase(line, ref result);
                    break;
                case "else":
                    ElseCase(line, ref result);
                    break;
                case "{":
                case "}":
                case "[":
                case "]":
                    BracketCase(line, ref result);
                    break;
                case "call":
                    CallCase(line, ref result);
                    break;
                default:
                    ActionCase(line, ref result);
                    break;
            }
            

        }

        private static void ElseCase(string line, ref List<DFLBlock> result)
        {
            result.Add(new DFLBlock("block", "else"));
        }

        private static void CallCase(string line, ref List<DFLBlock> result)
        {
            DFLBlock block = new DFLBlock("block", "call_func", "", line.Substring(5));
            result.Add(block);
        }

        private static void BracketCase(string line, ref List<DFLBlock> result)
        {
            DFLBlock block = new DFLBlock("bracket");

            string direct = "close";
            if (line.Equals("[") || line.Equals("{")) direct = "open";
            string type = "norm";
            if (line.Equals("[") || line.Equals("]")) type = "repeat";

            block.SetBracketInfo(direct, type);
            result.Add(block);
        }

        private static void ActionCase(string line, ref List<DFLBlock> result)
        {
            if (line.Length == 0) return;

            List<string> split = line.Replace('(', '.').Split('.').ToList();
            string actionBlock = split[0];

            int actionIdInd = line.IndexOf('.');
            int argsStartInd = line.IndexOf('(');
            string actionId = line.Substring(actionIdInd+1, argsStartInd - actionIdInd-1);

            DFLBlock block = new DFLBlock("block", actionBlock, actionId);

            string tagText = line.Substring(argsStartInd);


            List<string> newTagRef = tagText.SplitArguments();
            foreach (string tag in newTagRef)
            {
                if (tag.Length == 0) continue;
                block.addItem(DFItem.ParseTag(tag, block, actionId, actionBlock));
            }

            
            result.Add(block);
        }

        private static void ProcessCase(string line, ref List<DFLBlock> result)
        {
            result.Add(DFLBlock.ProcessBlock(line.Substring(8)));
        }

        private static void FuncCase(string line, ref List<DFLBlock> result)
        {
            if (line.Length < 5)
            {
                result.Add(DFLBlock.FunctionBlock("NewFunc"));
                return;
            }
            result.Add(DFLBlock.FunctionBlock(line.Substring(5)));
        }

        private static void EventCase(string line, ref List<DFLBlock> result)
        {
            string name = line.Substring(line.IndexOf('.')+1);

            DFLBlock block = new DFLBlock("block", "event", name);
            result.Add(block);
        }

    }
}
