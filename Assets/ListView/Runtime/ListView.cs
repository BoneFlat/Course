using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JackieSoft
{
    [AddComponentMenu("Jackie Soft/List View")]
    [RequireComponent(typeof(ScrollRect), typeof(CellCreator))]
    public class ListView : MonoBehaviour
    {
        public List<Cell.IData> data;
        [SerializeReference, SubclassSelector] private Layout layout = new Vertical();
        [SerializeReference, SubclassSelector] private Order order = new Ascending();
        [SerializeField] public RectOffset padding;
        [SerializeField] public float spacing;

        private ScrollRect _scrollRect;
        private CellCreator _cellCreator;
        private RectTransform _tContent;
        private LayoutElement _leHeader, _leFooter;
        private Cell[] _cells;
        
        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _cellCreator = GetComponent<CellCreator>();

            _scrollRect.viewport = GetComponent<RectTransform>();
            _tContent = new GameObject(">-------content---------<", typeof(RectTransform)).GetComponent<RectTransform>();
            _tContent.SetParent(_scrollRect.viewport, false);
            _scrollRect.content = _tContent;

            _leHeader = new GameObject(">--------Header--------<", typeof(RectTransform), typeof(LayoutElement)).GetComponent<LayoutElement>();
            _leFooter = new GameObject(">--------Footer--------<", typeof(RectTransform), typeof(LayoutElement)).GetComponent<LayoutElement>();

            _leHeader.transform.SetParent(_tContent, false);
            _leFooter.transform.SetParent(_tContent, false);

            layout.Set(_scrollRect);
            layout.Awake(_tContent, padding, _scrollRect.viewport.rect, order, spacing);
            order.Awake(_leHeader.GetComponent<RectTransform>(), _leFooter.GetComponent<RectTransform>());
        }

        private void OnEnable()
        {
            _scrollRect.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _scrollRect.onValueChanged.RemoveListener(OnValueChanged);
        }

        private int _top, _bottom;

        private void OnValueChanged(Vector2 val)
        {
            var pointStart = layout.CalculatePoint(val);
 
            var pointEnd = pointStart + layout.listViewSize;
            var start = pointStart < _cells[0].top ? 0 : CellAt(0, _cells.Length - 1, pointStart);
            var end = pointEnd > _cells[_cells.Length - 1].down ? (_cells.Length - 1) : CellAt(0, _cells.Length - 1, pointEnd);

            var dirty = false;
            if (start != _top)
            {
                if (start < _top)
                {
                    Active(start, _top - 1);
                }
                else
                {
                    DeActive(_top, start - 1);
                }
                
                dirty = true;
                _top = start;
            }

            if (end != _bottom)
            {
                if (end < _bottom)
                {
                    DeActive(end + 1, _bottom);
                }
                else
                {
                    Active(_bottom + 1, end);
                }
                
                dirty = true;
                _bottom = end;
            }

            if (dirty)
            {
                CorrectSibling();
                CorrectHeader();
                CorrectFooter();
            }
        }

        private void CorrectSibling()
        {
            var count = _tContent.childCount;
            for (var i = _top; i <= _bottom; i++)
                order.SetSibling(((Component)_cells[i].view).transform, i - _top, count);
        }

        private int CellAt(int top, int down, float point)
        {
            while (top != down)
            {
                var middle = (top + down) / 2;
                if (point < _cells[middle].top)
                {
                    down = middle - 1;
                }
                else if (point > _cells[middle].down)
                {
                    top = middle + 1;
                }
                else
                {
                    return middle;
                }
            }

            return top;
        }

        private void DeActive(int begin, int end)
        {
            for (var i = begin; i <= end; i++)
            {
                if (_cells[i].view != null)
                {
                    _cellCreator.Release(_cells[i].data, _cells[i].view);
                    _cells[i].view = null;
                }
            }
        }

        private void Active(int begin, int end)
        {
            for (var i = begin; i <= end; i++)
            {
                if (_cells[i].view == null)
                {
                    _cells[i].view = _cellCreator.Get(_cells[i].data);
                    _cells[i].data.Craw(_cells[i].view);
                    ((Component)_cells[i].view).transform.SetParent(_tContent, false);
                }
            }
        }

        public void Initialize()
        {
            var listViewSize = layout.listViewSize;
            _cells = new Cell[data.Count];

            var contentSize = layout.firstPadding;

            var cell0size = layout.CellSize(_cellCreator.CellSize(data[0]));
            // cell 0;
            _cells[0] = new Cell
            {
                data = data[0],
                point = contentSize,
                size = cell0size,
                top = 0,
                down = contentSize + cell0size + 0.5f * spacing,
            };

            contentSize = contentSize + cell0size + spacing;

            // cell 1 - cell n-2
            for (var i = 1; i < data.Count - 1; i++)
            {
                var cellData = data[i];
                var cellSize = layout.CellSize(_cellCreator.CellSize(cellData));

                _cells[i] = new Cell
                {
                    data = data[i],
                    point = contentSize,
                    size = cellSize,
                    top = contentSize - 0.5f * spacing,
                    down = contentSize + cellSize + 0.5f * spacing,
                };

                contentSize = contentSize + cellSize + spacing;
            }

            // cell n - 1
            var cellLastSize = layout.CellSize(_cellCreator.CellSize(data[data.Count - 1]));
            _cells[_cells.Length -1] = new Cell
            {
                data = data[data.Count -1],
                point = contentSize,
                size = cellLastSize,
                top = contentSize - 0.5f * spacing,
                down = contentSize + cellLastSize + layout.lastPadding,
            };

            contentSize = contentSize + cellLastSize + layout.lastPadding;

            _top = 0;
            _bottom = 0;

            while (_bottom < _cells.Length)
            {
                if (_cells[_bottom].point < listViewSize)
                {
                    _bottom++;
                }
                else
                {
                    _bottom--;
                    break;
                }
            }

            layout.SetContent(contentSize);

            Active(_top, _bottom);

            CorrectSibling();
            CorrectHeader();
            CorrectFooter();
        }

        private void CorrectHeader()
        {
            if (_top <= 0)
            {
                _leHeader.gameObject.SetActive(false);
            }
            else
            {
                _leHeader.gameObject.SetActive(true);

                layout.SetElement(_leHeader, _cells[_top].point - _cells[0].point - spacing);
                order.SetHeaderSibling();
            }
        }

        private void CorrectFooter()
        {
            if (_bottom >= _cells.Length - 1)
            {
                _leFooter.gameObject.SetActive(false);
            }
            else
            {
                _leFooter.gameObject.SetActive(true);
                layout.SetElement(_leFooter, _cells[_cells.Length - 1].point - _cells[_bottom].point - spacing - _cells[_bottom].size + _cells[_cells.Length - 1].size);
                order.SetFooterSibling();
            }
        }

        public abstract class Layout
        {
            protected RectTransform content;
            protected RectOffset padding;
            protected Rect viewport;
            protected float spacing;
            protected Order order;

            protected float contentSize;

            public float listViewSize { get; protected set; }
            public float firstPadding { get; protected set; }
            public float lastPadding { get; protected set; }

            public abstract float CellSize(Vector2 size);
            public abstract void Awake();
            public abstract float CalculatePoint(Vector2 val);
            public abstract void SetElement(LayoutElement layoutElement, float size);

            public virtual void SetContent(float size)
            {
                contentSize = size;
            }

            public void Awake(RectTransform content, RectOffset padding, Rect viewport, Order order, float spacing)
            {
                this.content = content;
                this.padding = padding;
                this.viewport = viewport;
                this.order = order;
                this.spacing = spacing;

                Awake();
            }

            public abstract void Set(ScrollRect scrollRect);
        }

        [Serializable]
        public abstract class Order
        {
            protected RectTransform header, footer;

            public void Awake(RectTransform header, RectTransform footer)
            {
                this.header = header;
                this.footer = footer;
            }

            public abstract void SetHeaderSibling();
            public abstract void SetFooterSibling();
            public abstract void SetSibling(Transform transform, int index, int count);

            public abstract float CalculateVal(float val);
        }
    }


    public class Cell
    {
        public IData data;
        public IView view;
        public float point;
        public float size;
        public float top;
        public float down;

        public interface IData
        {
            void Craw(IView cellView);
            Type ViewType { get; }
        }
        
        public interface IView
        {
        }

        public abstract class Data<T> : IData where T : MonoBehaviour, IView
        {
            void IData.Craw(IView cellView) => SetUp((T)cellView);
            Type IData.ViewType => typeof(T);
            protected abstract void SetUp(T cellView);
        }
    }
}