using System.Collections;
using UnityEngine;

namespace Bs.Shell.UI
{
    public class FirstUI : UIBase<FirstUIData>
	{
        Coroutine secondUIRoutine;
        UIToken cachedUIToken;

		public override void Bind(FirstUIData data)
		{
            Debug.Log(data.message);

            if (secondUIRoutine != null)
                StopCoroutine(secondUIRoutine);
            
            if (data.loadSecondUI)
            {   //  Load
                secondUIRoutine = StartCoroutine(
                                        LoadSecondUIRoutine(
                                            App.Instance.LoadUIAsync<SecondUIData, SecondUI>("SecondUI", ScriptableObject.CreateInstance<SecondUIDataEvent>(), null)
                                        )
                                    );
            }
            else
            {   //  Unload
                App.Instance.UnloadUI(cachedUIToken);
            }
        }

        public IEnumerator LoadSecondUIRoutine(WaitForUITokenYieldInstruction<SecondUIData, UIBase<SecondUIData>> waitForUIToken)
        {
            yield return waitForUIToken;
            cachedUIToken = waitForUIToken.uiToken;

            SecondUIData data = ScriptableObject.CreateInstance<SecondUIData>();
            data.message = "Hello World.  You loaded me from FirstUI!";
            waitForUIToken.uiToken.uiDataEvent.Raise(data);

            secondUIRoutine = null;
        }
	}
}