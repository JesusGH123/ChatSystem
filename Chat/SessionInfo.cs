using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Chat
{
    public static class SessionInfo
    {
        public static User getLoggedInUser(HttpSessionState session) => (User)session["logged_user"];
    }
}