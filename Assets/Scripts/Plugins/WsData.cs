using System;

namespace UnityWSControl
{
    public class WsData
    {
        // reserved values
        public String name { get; set; }
        public String sender_id { get; set; }
        public String message { get; set; }
        public String rooms { get; set; }
        public String room { get; set; }
        public String command { get; set; }

        // specific to the app
        public String text { get; set; }
        public String jt_pos { get; set; }
    }
}