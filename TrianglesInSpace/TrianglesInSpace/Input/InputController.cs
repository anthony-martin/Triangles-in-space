using System;
using MOIS;
using Mogre;
using TrianglesInSpace.Disposers;
using TrianglesInSpace.Messages;
using TrianglesInSpace.Messaging;
using TrianglesInSpace.Primitives;
using TrianglesInSpace.Time;
using Vector3 = Mogre.Vector3;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using TrianglesInSpace.Objects;

namespace TrianglesInSpace.Input
{

    public class InputController : IDisposable
    {
        private readonly Camera m_Camera;
        private readonly IBus m_Bus;
        private readonly IClock m_Clock;

        private readonly IPlayerId m_PlayerId;
        private readonly Disposer m_Disposer;

        private InputMode m_Mode = InputMode.Standard;
        private int count = 0;

        public InputController(string windowHandle, 
                               Camera camera, 
                               IBus bus, 
                               IClock clock,
                               IPlayerId id)
        {
            m_Camera = camera;
            m_Bus = bus;
            m_Clock = clock;
            m_PlayerId = id;

            ParamList pl = new ParamList();
            pl.Insert("WINDOW", windowHandle);
            pl.Insert("w32_mouse", "DISCL_FOREGROUND");
            pl.Insert("w32_mouse", "DISCL_NONEXCLUSIVE");
            pl.Insert("w32_keyboard", "DISCL_FOREGROUND");
            pl.Insert("w32_keyboard", "DISCL_NONEXCLUSIVE");

            m_Disposer  = new Disposer();

            bus.Subscribe<ChangeInputModeMessage>(OnModeChanged).AddTo(m_Disposer);
        }

        private void OnModeChanged(ChangeInputModeMessage message)
        {
            m_Mode = message.Mode;
        }

        public void Capture()
        {
        }

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr SetFocus(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch(m_Mode)
            {
                case InputMode.Placement:
                {
                    PlacementMode(hwnd, msg, wParam, lParam, ref handled);
                    break;
                }
                default:
                {
                    StandardMode(hwnd, msg, wParam, lParam, ref handled);
                    break;
                }
            }

            return IntPtr.Zero;
        }

        private IntPtr StandardMode(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WindowsConstants.WM_MOUSEACTIVATE)
            {
                SetFocus(hwnd);
                handled = true;
            }
            if (msg == WindowsConstants.WM_RBUTTONDOWN)
            {
                m_Bus.SendLocal(new SetPathToTargetMessage(FromWinScreenToWorldPosition(hwnd, lParam), m_Clock.Time));
                handled = true;
            }
            if (msg == WindowsConstants.WM_LBUTTONDOWN)
            {
                //SelectObject(mouseEvent);
                //x and y are in screen coordinates need to convert those to scene coordinates.
                //should be a case of converting to relative positions -1, 1 or 0,1 then using camera values
                m_Bus.SendLocal(new SelectObjectAtMessage(FromWinScreenToWorldPosition(hwnd, lParam), m_Clock.Time));
                handled = true;
            }

            return IntPtr.Zero;
        }

        private IntPtr PlacementMode(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WindowsConstants.WM_RBUTTONDOWN)
            {
                m_Mode = InputMode.Standard;
                handled = true;
            }
            if (msg == WindowsConstants.WM_LBUTTONDOWN)
            {
                //SelectObject(mouseEvent);
                //x and y are in screen coordinates need to convert those to scene coordinates.
                //should be a case of converting to relative positions -1, 1 or 0,1 then using camera values
                //m_Bus.Send(new SelectObjectAtMessage(FromWinScreenToWorldPosition(hwnd, lParam), m_Clock.Time));
                m_Bus.Send(new AddObjectMessage(m_PlayerId.Id, count.ToString()));
                m_Mode = InputMode.Standard;
                count++;

                handled = true;
            }
            return IntPtr.Zero;
        }

        private Vector FromWinScreenToWorldPosition(IntPtr hwnd, IntPtr param)
        {
            RECT rect;

            // unpack x and y from the param
            double x = unchecked((short)(long)param);
            double y = unchecked((short)((long)param >> 16));

            Vector worldPos = Vector.Zero;

            // get the size of the screen so we can tell how big we are.
            // this could be cahced in the future if we wanted to 
            // should be done if we need the performance
            if(GetWindowRect( hwnd, out rect))
            {
                double width = rect.Right - rect.Left;
                double height = rect.Bottom - rect.Top;

                //convert the positions to relative to top left corner. top left 0,0 bottom right 1,1
                double relx = x/ width;
                double rely = y / height;

                double cameraWidth = m_Camera.OrthoWindowWidth;
                double cameraHeight = m_Camera.OrthoWindowHeight;

                //calc a corner for the camera.
                //should be top left corner
                Vector3 cornerPosition = m_Camera.Position - new Vector3(cameraWidth / 2.0, 0, cameraHeight / 2.0);

                //camera coordinates decrease as the go down so subtract the z value
                Vector3 inputPosition = new Vector3( relx * cameraWidth, 0 ,(1 -  rely) * cameraHeight);

                Vector3 worldVector = cornerPosition + inputPosition;

                worldPos = new Vector(worldVector.z, worldVector.x);
            }

            return worldPos;

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

        public void Dispose()
        {
            //m_Mouse.Dispose();
            //m_InputManager.Dispose();
            m_Disposer.Dispose();
        }
    }
}
