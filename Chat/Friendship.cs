using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat
{
    public class Friendship
    {
        public Friendship(string my_id,string friend_id, List<(Message message,Source source)> messages)
        {
            this.friend_id = friend_id;
            this.my_id = my_id;
            this.messages = messages;
        }

        public string friend_id { get; }
        public string my_id { get; }
        public enum Source { me, friend }
        public List<(Message message, Source source)> messages { get; }
        
    }
}