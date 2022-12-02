using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dflConverter.impl.DFCodeBlocks.Actions
{
    public abstract class Action : CodeBlock
    {
        protected string action;
        protected List<Tag> tags = new List<Tag>();

        private void Initialize()
        {
            //action = typeof(this).Namespace
            SetParams();
        }

        protected virtual void SetParams() {}

        public override string GetLine()
        {
            string line = block + "." + action + "(";
            tags.ForEach(tag => line += tag.ToString());
            return line + ")";
        }


    }
}
