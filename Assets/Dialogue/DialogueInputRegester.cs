using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;

public class DialogueInputRegester : MonoBehaviour
{

#if USE_NEW_INPUT

        private TheSpiritAlchemist controls;

        // Track which instance of this script registered the inputs, to prevent
        // another instance from accidentally unregistering them.
        protected static bool isRegistered = false;
        private bool didIRegister = false;

        void Awake()
        {
            controls = new TheSpiritAlchemist();
        }

        void OnEnable()
        {
            if (!isRegistered)
            {
                isRegistered = true;
                didIRegister = true;
                controls.Enable();
                InputDeviceManager.RegisterInputAction("Interact", controls.Player.Interact);
                InputDeviceManager.RegisterInputAction("Horizontal", controls.Dialogue.Horizontal);
                InputDeviceManager.RegisterInputAction("Vertical", controls.Dialogue.Vertical);
                InputDeviceManager.RegisterInputAction("Select", controls.Dialogue.Select);
                InputDeviceManager.RegisterInputAction("Cancel", controls.Dialogue.Cancel);
            }
        }

        void OnDisable()
        {
            if (didIRegister)
            {
                isRegistered = false;
                didIRegister = false;
                controls.Disable();
                InputDeviceManager.UnregisterInputAction("Interact");
                InputDeviceManager.UnregisterInputAction("Horizontal");
                InputDeviceManager.UnregisterInputAction("Vertical");
                InputDeviceManager.UnregisterInputAction("Select");
                InputDeviceManager.UnregisterInputAction("Cancel");
            }
        }

#endif

}
