using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }

        public bool IsDeleted { get; set; }
    }
}
