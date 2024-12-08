using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleXFrame.UtilityHub.WinForms.Helpers
{
    public class InteractionTracker : NativeWindow, IDisposable
    {
        private readonly System.Windows.Forms.Timer _inactivityTimer;
        private readonly int _timeoutSeconds;
        private readonly Action _onTimeout;

        public InteractionTracker(Form targetForm, int timeoutSeconds, Action onTimeout)
        {
            _timeoutSeconds = timeoutSeconds;
            _onTimeout = onTimeout;

            // Attach to the target form
            targetForm.HandleCreated += (s, e) => AssignHandle(targetForm.Handle);
            targetForm.HandleDestroyed += (s, e) => ReleaseHandle();

            // Timer to track inactivity
            _inactivityTimer = new System.Windows.Forms.Timer { Interval = timeoutSeconds * 1000 };
            _inactivityTimer.Tick += (s, e) => OnInactivityTimeout();
            _inactivityTimer.Start();
        }

        private void ResetTimer()
        {
            _inactivityTimer.Stop();
            _inactivityTimer.Start();
        }

        private void OnInactivityTimeout()
        {
            _onTimeout.Invoke();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEMOVE = 0x0200;
            const int WM_KEYDOWN = 0x0100;

            // Reset the timer on user interaction
            if (m.Msg == WM_MOUSEMOVE || m.Msg == WM_KEYDOWN)
            {
                ResetTimer();
            }

            base.WndProc(ref m);
        }

        public void Dispose()
        {
            _inactivityTimer.Dispose();
        }
    }
}
