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
                if (_instance == null)
                {
                    var go = new GameObject("RunCoroutine");
                    go.hideFlags = HideFlags.DontSave;
                    _instance = go.AddComponent<RunCoroutine>();
                }

                return _instance;
            }
        }

	    void Awake ()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
	    }
	
        public new Coroutine StartCoroutine(IEnumerator ienumerator)
        {
            return base.StartCoroutine(ienumerator);
        }
    }
}