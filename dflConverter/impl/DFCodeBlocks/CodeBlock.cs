using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dflConverter.impl.DFCodeBlocks
{
    public abstract class CodeBlock
    {

        protected string block;
        public abstract string GetLine();

        public struct Tag
        {
            private string key;
            private string[] options;
            public string[] Options => options;
            private int option;

            public Tag(string key, params string[] options)
            {
                this.key = key;
                this.options = options;
                option = 0;
            }

            public override string ToString()
            {
                return string.Format("tag(\"{0}\", \"{1}\")", key, options[option]);
            }
        }

    }
}
