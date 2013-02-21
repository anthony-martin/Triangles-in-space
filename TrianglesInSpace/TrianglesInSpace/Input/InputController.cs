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
            state.height = 800;
            state.width = 800;

            m_Disposer  = new Disposer();

            m_Bus.Subscribe<TimeUpdateMessage>(OnTimeUpdate).AddTo(m_Disposer);

        }

        public void Capture()
        {
            m_Mouse.Capture();
        }

        private void OnTimeUpdate(TimeUpdateMessage message)
        {
            m_Time = message.Time;
        }

        private bool MousePressed(MouseEvent mouseEvent, MouseButtonID id)
        {
            if (mouseEvent.state.ButtonDown(MouseButtonID.MB_Right))
            {
                NavigateToTarget(mouseEvent);
            }
            if (mouseEvent.state.ButtonDown(MouseButtonID.MB_Left))
            {
                SelectObject(mouseEvent);
            }
            return true;
        }

        private Vector FromScreenToWorldPosition(double mouseX, double mouseY)
        {
            var windowHeight = m_Camera.OrthoWindowHeight;
            var windowWidth = m_Camera.OrthoWindowWidth;
            Vector3 cameraPosition = m_Camera.Position;
            Vector3 cornerPosition = cameraPosition - new Vector3(windowWidth / 2, 0, windowHeight / 2);
            cornerPosition.y = 0;
            var mouseOffset = new Vector3(windowHeight * (1.0 - mouseY / 800.0),
                                          0,
                                          windowWidth * mouseX / 800.0);
            var desiredPosition = cornerPosition + mouseOffset;

            return new Vector(desiredPosition.x, desiredPosition.z);
        }

        private void NavigateToTarget(MouseEvent mouseEvent)
        {
            var worldPosition = FromScreenToWorldPosition(mouseEvent.state.X.abs, mouseEvent.state.Y.abs);

            m_Bus.Send(new SetPathToTargetMessage(worldPosition, m_Time));
        }

        private void SelectObject(MouseEvent mouseEvent)
        {
            var worldPosition = FromScreenToWorldPosition(mouseEvent.state.X.abs, mouseEvent.state.Y.abs);

            m_Bus.Send(new SelectObjectAtMessage(worldPosition));
        }

        public void Dispose()
        {
            m_Mouse.Dispose();
            m_InputManager.Dispose();
            m_Disposer.Dispose();
        }
    }
}
