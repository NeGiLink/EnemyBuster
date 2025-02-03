using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyAssets
{
    public enum InputType
    {
        MouseLeft,
        MouseRight
    }
    public class InputUIController : MonoBehaviour
    {
        [SerializeField]
        private InputButtonUI[] inputButtonUIs;
        [SerializeField]
        private InputActionReference[] inputActions;

        private void Awake()
        {
            inputButtonUIs = GetComponentsInChildren<InputButtonUI>();
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        private void OnEnable()
        {

            inputActions[(int)InputType.MouseLeft].action.performed += inputButtonUIs[(int)InputType.MouseLeft].PressDown;
            inputActions[(int)InputType.MouseLeft].action.Enable();

        }

        private void OnDisable()
        {
            inputActions[(int)InputType.MouseLeft].action.performed -= inputButtonUIs[(int)InputType.MouseLeft].PressDown;
            inputActions[(int)InputType.MouseLeft].action.Disable();
        }

        // Update is called once per frame
        void Update()
        {
            if(inputActions[(int)InputType.MouseRight] == null) { return; }
            if(inputActions[(int)InputType.MouseRight].action.ReadValue<float>() > 0)
            {
                inputButtonUIs[(int)InputType.MouseRight].Press();
            }
            else
            {
                inputButtonUIs[(int)InputType.MouseRight].PressEnd();
            }
        }
    }
}
