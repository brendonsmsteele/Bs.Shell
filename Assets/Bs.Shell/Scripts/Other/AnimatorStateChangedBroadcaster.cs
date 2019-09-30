﻿using UnityEngine;

namespace Bs.Shell
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorStateChangedBroadcaster : MonoBehaviour
    {
        public delegate void OnStateChangedDelegate(int currentStateHash);
        public event OnStateChangedDelegate OnStateChanged;

        Attached<Animator> animator;

        int _currentStateInfo;
        int currentStateInfo
        {
            set
            {
                if(_currentStateInfo != value)
                {
                    _currentStateInfo = value;
                    OnStateChanged?.Invoke(_currentStateInfo);
                }
            }
        }

        private void Update()
        {
            currentStateInfo = animator.Value.GetCurrentAnimatorStateInfo(0).fullPathHash;
        }
    }
}