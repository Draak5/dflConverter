using dflConverter.impl.Parser;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dflConverter.impl
{
    public static class DFL
    {
        private static string fullInput = "";
        public static JObject convertCode(string inputText)
        {
            fullInput = inputText;

            JObject result = new JObject();
            result.Add("blocks", GetBlocksArray());
            return result;
        }

        private static JArray GetBlocksArray()
        {
            JArray result = new JArray();
            List<DFLBlock> codeline = new List<DFLBlock>();

            DFLBlock block = new DFLBlock("block", "func", data: "test");
            DFItem item = new DFItem("bl_tag", 26);
            item.AddData("option", "False");
            item.AddData("tag", "Is Hidden");
            item.AddData("action", "dynamic");
            item.AddData("block", "func");
            block.addItem(item);
            codeline.Add(DFLBlock.FunctionBlock("test"));

            
            block = new DFLBlock("block", "player_action", "SendMessage");
            block.addItem(DFItem.TextItem("hello world", 0));
            block.addItem(DFItem.TextItem("goodbye earth", 1));
            codeline.Add(block);

            codeline = CodeParser.ParseCode(fullInput);

            codeline.ForEach(i => result.Add(i.GetJson()));

            return result;
        }
    }
}
