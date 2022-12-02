using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dflConverter.impl
{
    public class DFLBlock
    {
        private string id;
        private string block;
        private string data;
        private string action;

        // brackets
        private string direct;
        private string type;

        private List<DFItem> items;
        public int appendSlot()
        {
            for (int i = 0; i < 36; i++)
            {
                bool result = true;
                foreach (DFItem item in items)
                {
                    if (item == null) continue;
                    if (item.GetSlot() == i)
                    {
                        result = false;
                        break;
                    }
                }
                if (result) return i;
            }
            return 0;
        }
        public int appendSlotI()
        {
            for (int i = 26; i > 1; i--)
            {
                bool result = true;
                foreach (DFItem item in items)
                {
                    if (item == null) continue;
                    if (item.GetSlot() == i)
                    {
                        result = false;
                        break;
                    }
                }
                if (result) return i;
            }
            return 0;
        }

        public DFLBlock(string id)
        {
            this.id = id;
        }

        public DFLBlock(string id, string block, string action = "", string data = "")
        {
            this.id = id;
            this.block = block;
            this.action = action;
            this.data = data;

            items = new List<DFItem>();
        }

        public void SetBracketInfo(string direct, string type)
        {
            this.direct = direct;
            this.type = type;
        }
        public void addItem(DFItem item)
        {
            items.Add(item);
        }


        public JObject GetJson()
        {
            JObject result = new JObject();

            result.Add("id", id);
            if (id.Equals("bracket"))
            {
                result.Add("direct", direct);
                result.Add("type", type);
                return result;
            }

            result.Add("block", block);
            if (data.Length > 0) result.Add("data", data);

            JArray argsItems = new JArray();
            items.ForEach(item => argsItems.Add(item.GetJson()));

            JObject argsItemsWrapper = new JObject();
            argsItemsWrapper.Add("items", argsItems);
            result.Add("args", argsItemsWrapper);

            if (action.Length > 0) result.Add("action", action);

            return result;
        }

        public static DFLBlock FunctionBlock(string name)
        {
            DFLBlock block = new DFLBlock("block", "func", data: name);
            DFItem item = new DFItem("bl_tag", 26);
            item.AddData("option", "False");
            item.AddData("tag", "Is Hidden");
            item.AddData("action", "dynamic");
            item.AddData("block", "func");
            block.addItem(item);

            return block;
        }
        public static DFLBlock ProcessBlock(string name)
        {
            DFLBlock block = new DFLBlock("block", "process", data: name);
            DFItem item = new DFItem("bl_tag", 26);
            item.AddData("option", "False");
            item.AddData("tag", "Is Hidden");
            item.AddData("action", "dynamic");
            item.AddData("block", "process");
            block.addItem(item);

            return block;
        }
    }
}
