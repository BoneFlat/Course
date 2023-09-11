using UnityEngine;

public class HwDelegate : MonoBehaviour
{
    delegate int MyDelegate(int x, int y);

    int AddNumbers(int x, int y)
    {
        return x + y;
    }

    int MultiplyNumbers(int x, int y)
    {
        return x * y;
    }

    int DivideNumbers(int x, int y)
    {
        return x / y;
    }

    private void Start()
    {
        MyDelegate delegateAdd = AddNumbers;
        MyDelegate delegateMultiply = MultiplyNumbers;
        MyDelegate delegateDivide = DivideNumbers;

        int addResult = delegateAdd(1, 2);
        int multiplyResult = delegateMultiply(3, 4);
        int divideResult = delegateDivide(7, 3);
        Debug.LogError("Add result: " + addResult);
        Debug.LogError("Multiply result: " + multiplyResult);
        Debug.LogError("Divide result: " + divideResult);
    }
}
