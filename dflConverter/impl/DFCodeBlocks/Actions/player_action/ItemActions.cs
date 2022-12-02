using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dflConverter.impl.DFCodeBlocks.Actions.player_action
{
    public class SetHotbar : Action {}
    public class SetInventory : Action {}
    public class SetSlotItem : Action {}
    public class SetEquipment : Action
    {
        protected override void SetParams()
        {
            tags.Add(new Tag("Equipment Slot", "Main hand", "Off hand", "Head", "Chest", "Legs", "Feet"));
        }
    }
    public class SetArmor : Action {}
    public class ReplaceItems : Action {}
    public class RemoveItems : Action {}
    public class ClearItems : Action {}
    public class ClearInv : Action
    {
        protected override void SetParams()
        {
            tags.Add(new Tag("Clear Crafting and Cursor", "True", "False"));
            tags.Add(new Tag("Clear Mode", "Entire inventory", "Main inventory", "Upper inventory", "Hotbar", "Armor"));
        }
    }
    public class SetCursorItem : Action {}
    public class SaveInv : Action {}
    public class LoadInv : Action {}
    public class SetItemCooldown : Action {}
}
