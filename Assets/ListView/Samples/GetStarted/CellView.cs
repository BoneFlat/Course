using JackieSoft;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour, Cell.IView
{
    public Text text;

}

public class CellData : Cell.Data<CellView>
{
    public int index;
    
    protected override void SetUp(CellView cellView)
    {
        cellView.gameObject.name = "element" + index;
        cellView.text.text = $"{index}";
    }
} 
