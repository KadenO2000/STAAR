using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace STAAR.StateManagement
{
    public class InputState
    {
        public KeyboardState CurrentKeyboardState;
        private KeyboardState _lastKeyboardState;

        public InputState()
        {
            CurrentKeyboardState = new KeyboardState();
            _lastKeyboardState = new KeyboardState();
        }

        public void Update()
        {
                _lastKeyboardState = CurrentKeyboardState;
                CurrentKeyboardState = Keyboard.GetState();
        }

        public bool IsKeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }
    }
}
