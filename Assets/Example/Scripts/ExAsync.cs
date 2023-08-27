using System.Threading;
using System.Threading.Tasks;
using System.IO;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Example.Scripts
{
    public class ExAsync : MonoBehaviour
    {
        private void Start()
        {
            
        }

        public async Task Counting()
        {
            await Task.Delay()
        }

        public void LoadAsset()
        {
            var asset = Resources.LoadAsync("path-to-assets");
            StreamWriter writer;
            // writer.WriteAsync();
            
        }
    }
}