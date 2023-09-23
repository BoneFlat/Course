using System.Collections.Generic;
using JackieSoft;
using UnityEngine;

public class ListViewInit : MonoBehaviour
{
    [SerializeField] private ListView listView;

    private void Start()
    {
        listView.data = new List<Cell.IData>();
        for (var i = 0; i < 500; i++)
        {
            listView.data.Add(new CellData { index = i });
        }

        listView.Initialize();
    }
}