using System.Collections;
using UnityEngine;
using Bs.Shell.ScriptableObjects;

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
            {   //  Load a UI additvely in code.
                secondUIRoutine = StartCoroutine(
                                        LoadSecondUIRoutine(
                                            App.Instance.LoadUIAsync<SecondUIData, SecondUI>("SecondUI", ScriptableObject.CreateInstance<SecondUIDataEvent>(), null)
                                        )
                                    );
            }
            else
            {   //  Unload a UI...  Implement the Dispose function to do things before Unloading the scene.
                App.Instance.UnloadUI(cachedUIToken);
            }
        }

        public IEnumerator LoadSecondUIRoutine(WaitForUITokenYieldInstruction<SecondUIData, UIBase<SecondUIData>> waitForUIToken)
        {
            //  This is how you wait for a UIToken to load.
            yield return waitForUIToken;

            //  You can hold onto the token for later.
            cachedUIToken = waitForUIToken.uiToken;
            //  You can create ScriptableObjects in code.
            SecondUIData data = ScriptableObject.CreateInstance<SecondUIData>();
            data.message = "Hello World.  You loaded me from FirstUI!";
            data.prefab = Resources.Load("Prefabs/itemA") as GameObject;
            data.interpolateRefSet = ScriptableObject.CreateInstance<InterpolateReferenceSet>();
            for (int i = 0; i < 4; i++)
            {
                InterpolateReference interpolateReference = new InterpolateReference();
                interpolateReference.Variable = ScriptableObject.CreateInstance<InterpolateVariable>();
                data.interpolateRefSet.Add(interpolateReference);
            }
            //  The UIToken holds onto the Event that the target UI listens to. 
            waitForUIToken.uiToken.uiDataEvent.Raise(data);

            //  If you chose to drag and drop ScriptableObjects that you create in editor, that is fine.
            //  Do it when the editor is NOT playing
            //  To do it when the editor is playing...  drag and drop... then Disable/Enable the gameObject that listens to the event.
            //  If you do, they act more like global events.

            secondUIRoutine = null;
        }
	}
}