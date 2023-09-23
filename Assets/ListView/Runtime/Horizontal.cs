using System;
using UnityEngine;
using UnityEngine.UI;

namespace JackieSoft
{
    [Serializable]
    public class Horizontal : ListView.Layout
    {
        public override float CellSize(Vector2 size) => size.x;

        public override void Awake()
        {
            // set content
            var layoutGroup = content.gameObject.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childForceExpandWidth = false;

            if (ascendingOrder is Descending)
            {
                layoutGroup.padding = padding;
                layoutGroup.spacing = spacing;
                layoutGroup.childAlignment = TextAnchor.MiddleRight;

                content.anchorMax = new Vector2(1, 1);
                content.anchorMin = new Vector2(1, 0);

                content.pivot = new Vector2(1, 0.5f);

                firstPadding = padding.right;
                lastPadding = padding.left;
            }
            // default is ascending
            else
            {
                layoutGroup.padding = padding;
                layoutGroup.spacing = spacing;
                layoutGroup.childAlignment = TextAnchor.MiddleLeft;

                content.anchorMax = new Vector2(0, 1);
                content.anchorMin = new Vector2(0, 0);

                content.pivot = new Vector2(0, 0.5f);

                firstPadding = padding.left;
                lastPadding = padding.right;
            }

            content.offsetMin = new Vector2(content.offsetMin.x, 0);
            content.offsetMax = new Vector2(content.offsetMax.x, 0);
            listViewSize = viewport.width;
        }

        public override float CalculatePoint(Vector2 val) => (contentSize - listViewSize) * (1 - ascendingOrder.CalculateVal(val.x));

        public override void SetElement(LayoutElement layoutElement, float size) => layoutElement.minWidth = size;

        public override void SetContent(float size)
        {
            base.SetContent(size);
            var sizeDelta = content.sizeDelta;
            sizeDelta.x = size;
            content.sizeDelta = sizeDelta;
        }

        public override void Set(ScrollRect scrollRect)
        {
            scrollRect.horizontal = true;
            scrollRect.vertical = false;
        }
    }
}