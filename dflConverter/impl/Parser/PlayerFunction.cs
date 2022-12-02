using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dflConverter.impl.Parser
{
    public class PlayerAction : DFLBlock
    {
        public static class FUNCTION
        {
            public readonly static string sendMessage = "SendMessage";
            public readonly static string sendTitle = "SendTitle";
            public readonly static string sendActionbar = "SendActionbar";
        }

        public PlayerAction(string action = "", string data = "") : base("block", "player_action", action, data)
        {
        }

        public static PlayerAction Parse(string text)
        {
            PlayerAction action = new PlayerAction();

            /*
            FUNCTION func = (FUNCTION) Enum.Parse(typeof(FUNCTION), text);
            switch (func)
            {
                case FUNCTION.sendMessage:
                    return new PlayerAction("block", "player_action");
                case FUNCTION.sendActionbar:
                    return new PlayerAction("block", "player_action");
            }*/

            return action;
        }

    }
}
