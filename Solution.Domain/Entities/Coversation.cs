using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Conversation
    {
        public Conversation()
        {
            status = messageStatus.Sent;
        }

        public enum messageStatus
        {
            Sent,
            Delivered
        }

        public int id { get; set; }
        public int sender_id { get; set; }
        public int receiver_id { get; set; }
        public string message { get; set; }
        public messageStatus status { get; set; }

        public string created_at { get; set; } = DateTime.Now.ToString();
    }
}

