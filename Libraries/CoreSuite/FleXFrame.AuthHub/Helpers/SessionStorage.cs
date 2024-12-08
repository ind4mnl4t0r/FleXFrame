using FleXFrame.AuthHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Helpers
{
    public class SessionStorage
    {
        public static Session? CurrentSession { get; private set; }

        public static void StartSession(Session session)
        {
            CurrentSession = session;
        }

        public static void EndSession()
        {
            CurrentSession = null;
        }

        public static bool IsActive => CurrentSession != null && CurrentSession.SessionStatus == Session.SessionStatuses.Active;
    }
}
