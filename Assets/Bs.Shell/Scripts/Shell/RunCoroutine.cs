using System.Collections;
using UnityEngine;

namespace Bs.Shell
{
    public class RunCoroutine : MonoBehaviour
    {
        static RunCoroutine _instance;
        public static RunCoroutine Instance
        {
            get
            {
                return _instance;
            }
        }

	    void Awake () {
            _instance = this;
	    }
	
        public new Coroutine StartCoroutine(IEnumerator ienumerator)
        {
            return base.StartCoroutine(ienumerator);
        }
    }
}