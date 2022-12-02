using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dflConverter.impl.DFRef
{
    public class DFValue : DFItem
    {
        public DFValue(float numberValue, int slot) : base("num", slot)
        {
            data.Add("name", numberValue.ToString());
        }
        public DFValue(string textValue, int slot) : base("txt", slot)
        {
            data.Add("name", textValue.ToString());
        }

    }
}
