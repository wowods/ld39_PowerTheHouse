using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ld39.Entities;
using System.Linq;

namespace ld39
{
    static class InputManager
    {
        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;
        private static bool isUsingMouse;
        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }

        public static void Update()
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            // if arrow keys is used, disabled control with mouse
            // if moving with mouse, enable aim with mouse
            if (new[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down }.Any(x => keyboardState.IsKeyDown(x)))
                isUsingMouse = false;
            else if (MousePosition != new Vector2(lastMouseState.X, lastMouseState.Y))
                isUsingMouse = true;

            Point currentPosition = GetCursorPositionByMouse();
            // Controlling cursor with mouse
            if (isUsingMouse && GameMain.State.Equals(GameMain.GAMESTATE.Playing))
            {
                Cursor.Instance.Position = currentPosition;
                if (LeftClickWasPressed())// && EntityManager.CheckSelectedEntity(currentPosition.X, currentPosition.Y))
                    if (EntityManager.CheckSelectedEntity(currentPosition.X, currentPosition.Y))
                        Cursor.Instance.ChangeSelectedState();
            }
            // Contolling cursor with keyboard
            else
            {
                if (GameMain.State.Equals(GameMain.GAMESTATE.Playing))
                {
                    if (WasKeyPressed(Keys.A) || WasKeyPressed(Keys.Left))
                        Cursor.Instance.Position += new Point(-1, 0);
                    if (WasKeyPressed(Keys.D) || WasKeyPressed(Keys.Right))
                        Cursor.Instance.Position += new Point(1, 0);
                    if (WasKeyPressed(Keys.W) || WasKeyPressed(Keys.Up))
                        Cursor.Instance.Position += new Point(0, -1);
                    if (WasKeyPressed(Keys.S) || WasKeyPressed(Keys.Down))
                        Cursor.Instance.Position += new Point(0, 1);
                    if (WasKeyPressed(Keys.Space) && EntityManager.CheckSelectedEntity(currentPosition.X, currentPosition.Y))
                        Cursor.Instance.ChangeSelectedState();
                }
                if (WasKeyPressed(Keys.Enter) && GameMain.State.Equals(GameMain.GAMESTATE.Winning))
                {
                    if (GameMain.CurrentLevel < 9)
                    {
                        EntityManager.ClearAll();
                        LevelManager.ClearAll();
                        GameMain.CurrentLevel++;
                        LevelManager.LoadLevel(GameMain.CurrentLevel);
                    }
                }
            }
        }

        private static bool LeftClickWasPressed()
        {
            return (lastMouseState.LeftButton == ButtonState.Released) && (mouseState.LeftButton == ButtonState.Pressed);
        }

        private static bool WasKeyPressed(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }

        private static Point GetCursorPositionByMouse()
        {
            return MousePosition.GetIndexPosition();
        }
    }
}
