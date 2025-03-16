using EventSystem;
using EventSystem.Signals;
using UnityEngine;
using Zenject;

namespace Logic.Core
{
    public class InputManager : ITickable
    {
        private readonly EventBus _eventBus;

        public InputManager(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _eventBus.Invoke(new EscapeClickedSignal());
            if (Input.GetKeyDown(KeyCode.Alpha1)) _eventBus.Invoke(new Alpha1ClickedSignal());
            if (Input.GetKeyDown(KeyCode.Alpha2)) _eventBus.Invoke(new Alpha2ClickedSignal());
        }
    }
}