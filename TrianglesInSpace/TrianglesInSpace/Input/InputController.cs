using System;
using MOIS;
using Mogre;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;
using Vector3 = Mogre.Vector3;

namespace TrianglesInSpace.Input
{
    public class InputController : IDisposable
    {
        private readonly Camera m_Camera;
        private readonly IBus m_Bus;
        private InputManager m_InputManager;
        //private Keyboard m_Keyboard;
        private Mouse m_Mouse;

        private Disposer m_Disposer;

        private ulong m_Time;

        public InputController(string windowHandle, Camera camera, IBus bus)
        {
            m_Camera = camera;
            m_Bus = bus;

            ParamList pl = new ParamList();
            pl.Insert("WINDOW", windowHandle);
            pl.Insert("w32_mouse", "DISCL_FOREGROUND");
            pl.Insert("w32_mouse", "DISCL_NONEXCLUSIVE");
            pl.Insert("w32_keyboard", "DISCL_FOREGROUND");
            pl.Insert("w32_keyboard", "DISCL_NONEXCLUSIVE");

            m_InputManager = MOIS.InputManager.CreateInputSystem(pl);

            //m_Keyboard = (MOIS.Keyboard)m_InputManager.CreateInputObject(MOIS.Type.OISKeyboard, false);
            m_Mouse = (MOIS.Mouse)m_InputManager.CreateInputObject(MOIS.Type.OISMouse, true);
            m_Mouse.MousePressed += MousePressed;
            var state = m_Mouse.MouseState;
            state.height = 600;
            state.width = 800;

            m_Disposer  = new Disposer();

            m_Bus.Subscribe<TimeUpdateMessage>(OnTimeUpdate).AddTo(m_Disposer);

        }

        private void OnTimeUpdate(TimeUpdateMessage message)
        {
            m_Time = message.Time;
        }

        private bool MousePressed(MouseEvent mouseEvent, MouseButtonID id)
        {
            if (mouseEvent.state.ButtonDown(MouseButtonID.MB_Left))
            {
                var windowHeight = m_Camera.OrthoWindowHeight;
                var windowWidth = m_Camera.OrthoWindowWidth;
                Vector3 cameraPosition = m_Camera.Position;
                Vector3 cornerPosition = cameraPosition - new Vector3(windowWidth / 2, 0, windowHeight / 2);
                cornerPosition.y = 0;
                var mouseOffset = new Vector3(windowHeight * (1.0 - (double)mouseEvent.state.Y.abs / 600.0),
                                              0,
                                              windowWidth * (double)mouseEvent.state.X.abs / 800.0);
                var desiredPosition = cornerPosition + mouseOffset;
                //m_ClickNode.SetPosition(desiredPosition.x, 0, desiredPosition.z);

                //m_Path.MoveToDestination(new Vector2(desiredPosition.x, desiredPosition.z), m_time);
                m_Bus.Send(new SetPathToTarget(new Vector(desiredPosition.x, desiredPosition.z), m_Time));
            }
            return true;
        }

        public void Dispose()
        {
            m_Mouse.Dispose();
            m_InputManager.Dispose();
            m_Disposer.Dispose();
        }
    }
}
