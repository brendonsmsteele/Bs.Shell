using UnityEngine;
using UnityEngine.UI;

namespace Bs.Shell.Examples
{
    public class Item : MonoBehaviour
    {
        public Image image;
        public Text text;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Bind(TestData data)
        {
            image.color = data.color;
            text.text = data.id.ToString();
        }
    }
}
