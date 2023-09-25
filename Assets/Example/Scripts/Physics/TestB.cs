using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestB : MonoBehaviour
{
    void Start()
    {
        Test();
    }
    void Test()
    {
        int[] numbers = new int[1000000];

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i;
        }

        // Sử dụng boxing
        Stopwatch boxingStopwatch = new Stopwatch();
        boxingStopwatch.Start();

        int sumWithBoxing = 0;
        foreach (int num in numbers)
        {
            sumWithBoxing += num;
        }

        boxingStopwatch.Stop();

        // Không sử dụng boxing
        Stopwatch noBoxingStopwatch = new Stopwatch();
        noBoxingStopwatch.Start();

        int sumWithoutBoxing = 0;
        foreach (int num in numbers)
        {
            sumWithoutBoxing += num;
        }

        noBoxingStopwatch.Stop();

        Debug.LogError("Tổng với boxing: " + sumWithBoxing);
        Debug.LogError("Thời gian với boxing: " + boxingStopwatch.ElapsedMilliseconds + " ms");

        Debug.LogError("Tổng không boxing: " + sumWithoutBoxing);
        Debug.LogError("Thời gian không boxing: " + noBoxingStopwatch.ElapsedMilliseconds + " ms");
    }
    
    void ListViewTest()
    {
        // Khởi tạo ListView
        ListView listView = new ListView();
        listView.View = View.Details;
        listView.Columns.Add("Name", 120);
        listView.Columns.Add("Age", 60);

        // Thêm dữ liệu vào ListView
        AddStudent(listView, "Sang", 25);
        AddStudent(listView,"Dung", 22);
        AddStudent(listView,"Huy", 28);
    }

    // Phương thức để thêm một học viên vào ListView
    private void AddStudent(ListView listView, string name, int age)
    {
        ListViewItem item = new ListViewItem(name);
        item.SubItems.Add(age.ToString());
        listView.Items.Add(item);
    }

    // Xử lý sự kiện khi người dùng nhấp vào nút
    private void button1_Click(ListView listView, object sender, EventArgs e)
    {
        // Lấy dữ liệu từ mục đã chọn (nếu có)
        if (listView.SelectedItems.Count > 0)
        {
            ListViewItem selectedItem = listView.SelectedItems[0];
            string name = selectedItem.Text;
            string age = selectedItem.SubItems[1].Text;

            MessageBox.Show($"Tên: {name}, Tuổi: {age}");
        }
        else
        {
            MessageBox.Show("Vui lòng chọn một học viên.");
        }
    }
}


