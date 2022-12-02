using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dflConverter.impl.Decoder
{
    public static class CodeDecoder
    {
        // json string
        private static string JStr(JObject obj, string name) => obj.Value<string>(name);

        private static int _indentation = 0;
        public static string Decode(JObject json)
        {
            _indentation = 0;
            string final = "";
            JArray blocks = json.Value<JArray>("blocks");
            foreach (JObject block in blocks)
            {
                for (int i = 0; i < _indentation; i++) final += "\t";
                string id = block.Value<string>("id");
                switch (id)
                {
                    default:
                        final += "!!!!!!!!!!!!!!" + id;
                        break;
                    case "bracket":
                        string bracketTypes = "{}";
                        if (block.Value<string>("type").Equals("repeat")) bracketTypes = "[]";
                        if (block.Value<string>("direct") == "close")
                        {
                            final = final.Remove(final.Length-1);
                            final += bracketTypes[1];
                            _indentation--;
                        }
                        else
                        {
                            final += bracketTypes[0];
                            _indentation++;
                        }
                        break;
                    case "block":
                        if (block.Value<string>("block") == "func")
                        {
                            // start function
                            final += block.Value<string>("block") + " " + block.Value<string>("data");
                            _indentation++;
                        } else if (block.Value<string>("block") == "event")
                        {
                            // start event
                            final += block.Value<string>("block") + "." + block.Value<string>("action");
                            _indentation++;
                        }
                        else if (block.Value<string>("block") == "call_func")
                        {
                            // call function
                            final += "call " + block.Value<string>("data");
                        }
                        else if (block.Value<string>("block") == "else")
                        {
                            // else statement
                            final += "else";
                        }
                        else if (block.Value<string>("block") == "process")
                        {
                            // start process
                            final += block.Value<string>("block") + " " + block.Value<string>("data");
                            _indentation++;
                        }
                        else
                        {
                            // Its literally any other action
                            final += block.Value<string>("block") + "." + block.Value<string>("action")+"(";

                            // Add items to the end of the line
                            if (block.Value<JObject>("args") != null)
                            {
                                JArray items = block.Value<JObject>("args").Value<JArray>("items");
                                List<string> arguments = itemArguments(items);
                                final += string.Join(", ", arguments) + ")";
                            }
                        }
                        break;
                }

                final += "\n";
            }

            return final;
        }

        private static List<string> itemArguments(JArray items)
        {
            List<string> arguments = new List<string>();
            foreach (JObject itemItt in items)
            {
                JObject item = itemItt.Value<JObject>("item");
                string itemId = item.Value<string>("id");
                JObject itemData = item.Value<JObject>("data");

                switch (itemId)
                {
                    case "num":
                        if (float.TryParse(JStr(itemData, "name"), out float r)) arguments.Add(r.ToString());
                        else arguments.Add(string.Format("num({0})", JStr(itemData, "name")));
                        break;
                    case "var":
                        arguments.Add(string.Format("var(\"{0}\", {1})", JStr(itemData, "name"), JStr(itemData, "scope")));
                        break;
                    case "txt":
                        arguments.Add(string.Format("\"{0}\"", JStr(itemData, "name").Replace("\n", "\\n")));
                        break;
                    case "loc":
                        JObject loc = itemData.Value<JObject>("loc");
                        arguments.Add(string.Format("loc({0}, {1}, {2}, {3}, {4})", JStr(loc, "x"), JStr(loc, "y"), JStr(loc, "z"), JStr(loc, "pitch"), JStr(loc, "yaw")));
                        break;
                    case "part":
                        // part("Dust", (1, 0, 0), (0, 0, 0), 0)
                        // part("Dust", (1, 0, 0), 0, 0, 0, 0)
                        JObject cluster = itemData.Value<JObject>("cluster");
                        JObject subData = itemData.Value<JObject>("data");
                        string str;
                        if (subData.TryGetValue("rgb", out JToken rgb))
                        {
                            str = string.Format(
                                "part(\"{0}\", ({1}, {2}, {3}), {4}, {5}, {6}, {7})",
                                JStr(itemData, "particle"),
                                JStr(cluster, "amount"),
                                JStr(cluster, "horizontal"),
                                JStr(cluster, "vertical"),
                                JStr(subData, "rgb"),
                                JStr(subData, "colorVariation"),
                                JStr(subData, "size"),
                                JStr(subData, "sizeVariation"));
                        }
                        else
                        {
                            str = string.Format(
                                "part(\"{0}\", ({1}, {2}, {3}), ({4}, {5}, {6}), {7})",
                                JStr(itemData, "particle"),
                                JStr(cluster, "amount"),
                                JStr(cluster, "horizontal"),
                                JStr(cluster, "vertical"),
                                JStr(subData, "x"),
                                JStr(subData, "y"),
                                JStr(subData, "z"),
                                JStr(subData, "motionVariation"));
                        }
                        arguments.Add(str);

                        break;
                    case "g_val":
                        arguments.Add(string.Format("gval(\"{0}\", {1})", JStr(itemData, "type"), JStr(itemData, "target")));
                        break;
                    case "snd":
                        arguments.Add(string.Format("sound(\"{0}\", {1}, {2})", JStr(itemData, "sound"), JStr(itemData, "pitch"), JStr(itemData, "vol")));
                        break;
                    case "pot":
                        arguments.Add(string.Format("pot(\"{0}\", {1}, {2})", JStr(itemData, "pot"), JStr(itemData, "dur"), JStr(itemData, "amp")));
                        break;
                    case "vec":
                        arguments.Add(string.Format("vec({0}, {1}, {2})", JStr(itemData, "x"), JStr(itemData, "y"), JStr(itemData, "z")));
                        break;
                    case "bl_tag":
                        arguments.Add(string.Format("tag(\"{0}\", \"{1}\")", JStr(itemData, "tag"), JStr(itemData, "option")));
                        break;
                    case "item":
                        arguments.Add(string.Format("item({0})", JStr(itemData, "item")));
                        break;
                    default:
                        arguments.Add("!~! " + itemId);
                        break;
                }
            }
            return arguments;
        }

    }
}

