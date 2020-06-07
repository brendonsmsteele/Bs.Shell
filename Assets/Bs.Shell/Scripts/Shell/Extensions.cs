using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

namespace Nc.Shell
{
    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Toggle<T>(this List<T> list, T item)
        {
            if (list.Contains(item))
                list.Remove(item);
            else
                list.Add(item);
        }
        
        public static bool HasParameterOfType(this Animator self, string name, AnimatorControllerParameterType type)
        {
            var parameters = self.parameters;
            foreach (var currParam in parameters)
            {
                if (currParam.type == type && currParam.name == name)
                {
                    return true;
                }
            }
            return false;
        }


        public static void GetTriggers(this Animator animator, List<string> triggers)
        {
            var parameters = animator.parameters;
            foreach (var currParam in parameters)
            {
                if (currParam.type == AnimatorControllerParameterType.Trigger)
                {
                    triggers.Add(currParam.name);
                }
            }
        }

        public static void SetTriggerSingle(this Animator self, string trigger)
        {
            foreach (var param in self.parameters)
            {
                if (param.name != trigger)
                    self.ResetTrigger(param.name);
                else
                    self.SetTrigger(trigger);
            }
        }

        public static RaycastHit RaycastReturnNearestHit(this Camera camera, Vector3 screenPoint)
        {
            var ray = camera.ScreenPointToRay(screenPoint);
            var hits = Physics.RaycastAll(ray, Mathf.Infinity);
            var orderedHits = hits.OrderBy((h)=> { return Vector3.Distance(camera.transform.position, h.point); });
            return orderedHits.FirstOrDefault();
        }

        public static void Show(this CanvasGroup canvasGroup, bool show)
        {
            canvasGroup.alpha = show ? 1f : 0f;
            canvasGroup.blocksRaycasts = show;
            canvasGroup.interactable = show;
        }

        public static GameObject InstantiateChildFromResources(this Transform parent, string path)
        {
            var o = Resources.Load(path);
            if (o == null)
                return null;

            GameObject go = GameObject.Instantiate(o) as GameObject;
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            return go;
        }


    }
}