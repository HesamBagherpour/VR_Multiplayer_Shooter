using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using VR_PROJECT.General;

namespace VR_PROJECT.Inputs
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        #region Fields

        [SerializeField] private InputActionReference _sprintAction;

        private bool _sprint;

        #endregion

        #region Properties

        public bool Sprint => _sprint;

        #endregion

        #region Public Methods

        public UniTask<Result<bool>> Init()
        {
            AsignActions();
            
            DontDestroyOnLoad(this);

            return new UniTask<Result<bool>>();
        }

        #endregion

        #region Private Methods

        private void AsignActions()
        {
            var sprintModeAction = _sprintAction;
            _sprintAction.action.started += SprintStarted;
            _sprintAction.action.canceled += SprintCanceled;
        }

        private void SprintStarted(InputAction.CallbackContext context)
        {
            _sprint = true;
        }
        private void SprintCanceled(InputAction.CallbackContext obj)
        {
            _sprint = false;
        }

        #endregion

    }
}

