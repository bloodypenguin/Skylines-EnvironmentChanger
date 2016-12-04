using ColossalFramework.UI;
using UnityEngine;

namespace EnvironmentChanger
{
    public static class UIUtils
    {
        public static UIDropDown CreateDropDown(UIComponent parent)
        {
            UIDropDown dropDown = parent.AddUIComponent<UIDropDown>();
            dropDown.size = new Vector2(90f, 27f);
            dropDown.listBackground = "StylesDropboxListbox";
            dropDown.itemHeight = 22;
            dropDown.itemHover = "ListItemHover";
            dropDown.itemHighlight = "ListItemHighlight";
            dropDown.normalBgSprite = "StylesDropbox";
            dropDown.disabledBgSprite = "";
            dropDown.hoveredBgSprite = "";
            dropDown.focusedBgSprite = "";
            dropDown.listWidth = 90;
            dropDown.listHeight = 200;
            dropDown.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            dropDown.popupColor = new Color32(45, 52, 61, 255);
            dropDown.popupTextColor = new Color32(170, 170, 170, 255);
            dropDown.zOrder = 1;
            dropDown.verticalAlignment = UIVerticalAlignment.Middle;
            dropDown.horizontalAlignment = UIHorizontalAlignment.Left;
            dropDown.selectedIndex = 0;
            dropDown.textFieldPadding = new RectOffset(7, 28, 4, 0);
            dropDown.itemPadding = new RectOffset(4, 4, 4, 4);
            dropDown.listPadding = new RectOffset(4, 4, 4, 4);
            dropDown.listPosition = UIDropDown.PopupListPosition.Automatic;
            dropDown.triggerButton = dropDown;

            dropDown.eventSizeChanged += (c, t) =>
            {
                dropDown.listWidth = (int)t.x;
            };

            return dropDown;
        }
    }
}