using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class ShieldTool : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        public Animator Animator => animator;

        private string stateName = "State";

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void ShieldOpen()
        {
            animator.SetInteger(stateName, 0);
        }

        public void ShieldClose()
        {
            animator.SetInteger(stateName, -1);
        }
    }
}
