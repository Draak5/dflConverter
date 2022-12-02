using dflConverter.impl.DFRef;
using dflConverter.impl.Parser;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dflConverter.impl
{
    public class DFItem
    {

        protected string id;
        protected JObject data;
        protected int slot;

        public DFItem(string id, int slot)
        {
            this.id = id;
            data = new JObject();
            this.slot = slot;
        }


        public JObject GetJson()
        {
            JObject itemJson = new JObject();
            itemJson.Add("id", id);
            itemJson.Add("data", data);
            
            JObject result = new JObject();
            result.Add("item", itemJson);
            result.Add("slot", slot);
            return result;
        }


        public void SetData(JObject data)
        {
            this.data = data;
        }

        public void SetDataName(string name)
        {
            data.Add("name", name);
        }

        public static DFItem TextItem(string v, int slot)
        {
            DFItem item = new DFItem("txt", slot);

            JObject data = new JObject();
            data.Add("name", v);
            item.SetData(data);

            return item;
        }

        internal void AddData(string key, string value)
        {
            data.Add(key, value);
        }
        internal void AddData(string key, float value)
        {
            data.Add(key, value);
        }

        internal int GetSlot()
        {
            return slot;
        }

        public static DFItem ParseTag(string tag, DFLBlock _block, string action, string block)
        {
            int slot = _block.appendSlot();
            int bltagSlot = _block.appendSlotI();

            if (float.TryParse(tag, out float value))
            {
                return new DFValue(value, slot);
            }
            if (tag.StartsWith("num"))
            {
                DFItem item = new DFItem("num", slot);
                item.AddData("name", tag.Substring(4, tag.Length - 5));
                return item;
            }
            if (tag.StartsWith("\""))
            {
                return new DFValue(tag.Substring(1, tag.Length - 2), slot);
            }
            if (tag.StartsWith("item"))
            {
                string itemTag = tag.Substring(5, tag.Length - 6);
                DFItem item = new DFItem("item", slot);
                item.AddData("item", itemTag);
                return item;
            }
            if (tag.StartsWith("var"))
            {
                string itemTag = tag.Substring(4, tag.Length - 5);
                string scope = itemTag.Split(',').Last().Trim();
                string varName = itemTag.Replace(scope, "").Trim();
                varName = varName.Substring(1, varName.Length - 3);

                DFItem item = new DFItem("var", slot);
                item.AddData("name", varName);
                item.AddData("scope", scope);
                return item;
            }
            if (tag.StartsWith("gval"))
            {
                string itemTag = tag.Substring(5, tag.Length - 6);
                List<string> args = itemTag.Split(',').ToList();

                DFItem item = new DFItem("g_val", slot);
                item.AddData("type", args[0].Trim().Substring(1, args[0].Trim().Length-2));
                item.AddData("target", args[1].Trim());
                return item;
            }
            if (tag.StartsWith("sound"))
            {
                string itemTag = tag.Substring(6, tag.Length - 7);
                List<string> args = itemTag.Split(',').ToList();

                float pitch = 0f;
                float volume = 0f;
                float.TryParse(args[1].Trim(), out pitch);
                float.TryParse(args[2].Trim(), out volume);

                DFItem item = new DFItem("snd", slot);
                item.AddData("sound", args[0].Substring(1, args[0].Length - 2));
                item.AddData("pitch", pitch);
                item.AddData("vol", volume);
                return item;
            }
            if (tag.StartsWith("part"))
            {
                string itemTag = tag.Substring(5, tag.Length - 5);
                string name = itemTag.Split('"')[1];

                List<string> args = itemTag.SplitArguments();
                List<string> clusterArgs = args[1].SplitArguments();

                DFItem item = new DFItem("part", slot);

                JObject data = new JObject();
                data.Add("particle", name);

                JObject cluster = new JObject();
                cluster.Add("amount", clusterArgs[0]);
                cluster.Add("horizontal", clusterArgs[1]);
                cluster.Add("vertical", clusterArgs[2]);
                data.Add("cluster", cluster);

                JObject subData = new JObject();
                if (args[2].Contains("("))
                {
                    List<string> vecArgs = args[2].SplitArguments();
                    subData.Add("x", vecArgs[0]);
                    subData.Add("y", vecArgs[1]);
                    subData.Add("z", vecArgs[2]);
                    subData.Add("motionVariation", args[3]);
                }
                else
                {
                    subData.Add("rgb", args[2]);
                    subData.Add("colorVariation", args[3]);
                    subData.Add("size", args[4]);
                    subData.Add("sizeVariation", args[5]);
                }

                data.Add("data", subData);

                item.SetData(data);
                return item;
            }
            if (tag.StartsWith("tag"))
            {
                string itemTag = tag.Substring(4, tag.Length - 5);
                string option = itemTag.Split('"')[3];
                string name = itemTag.Split('"')[1];

                DFItem item = new DFItem("bl_tag", bltagSlot);
                item.AddData("option", option);
                item.AddData("tag", name);
                item.AddData("action", action);
                item.AddData("block", block);
                return item;
            }

            return new DFValue(0, slot);
        }
    }
}
