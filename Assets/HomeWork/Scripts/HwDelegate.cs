using UnityEngine;

public class HwDelegate : MonoBehaviour
{
    // Khai báo delegate với input là hai số nguyên
    public delegate void MathOperationDelegate(int x, int y);

    private MathOperationDelegate mathOperationDelegate;
    [SerializeField] private int x = 10;
    [SerializeField] private int y = 5;
    private void Start()
    {
        // Gán các method vào delegate
        mathOperationDelegate += AddNumbers;
        mathOperationDelegate += DivideNumbers;
        mathOperationDelegate += MultiplyNumbers;

        // Gọi delegate với các giá trị x và y

        mathOperationDelegate(x, y);
    }

    private void AddNumbers(int x, int y)
    {
        int result = x + y;
        Debug.Log("Addition Result: " + result);
    }

    private void DivideNumbers(int x, int y)
    {
        if (y != 0)
        {
            int result = x / y;
            Debug.Log("Division Result: " + result);
        }
        else
        {
            Debug.LogError("Cannot divide by zero.");
        }
    }

    private void MultiplyNumbers(int x, int y)
    {
        int result = x * y;
        Debug.Log("Multiplication Result: " + result);
    }
}