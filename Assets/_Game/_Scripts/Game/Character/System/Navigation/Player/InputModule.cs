using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Character
{
    using UnityEngine.InputSystem;
    using Utilities;
    using Utilities.Core.Character.NavigationSystem;

    public class InputModule : AbstractNavigationModule<NavigationData, NavigationParameter>
    {

        PlayerInputActions playerInputActions;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Jump.performed += OnJump;
            playerInputActions.Player.Fire.performed += OnFire;
        }
        public override void StartNavigation()
        {

        }

        public override void StopNavigation()
        {

        }

        public override void UpdateData()
        {
            Data.MoveDirection = playerInputActions.Player.Movement.ReadValue<Vector2>();
        }

        protected void OnJump(InputAction.CallbackContext callback)
        {
            Data.Jump.Value = true;
        }

        protected void OnFire(InputAction.CallbackContext callback) 
        { 
            Data.Fire.Value = true;
        }
    }
}