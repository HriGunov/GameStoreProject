using System;

namespace GameStore.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}