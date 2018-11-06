using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Web.Models.ProductsViewModels
{
    public class AddCommentViewModel
    {
        public AddCommentViewModel()
        {

        }
        public AddCommentViewModel(int productId, string text)
        {
            ProductId = productId;
            Text = text;
        }

        public int ProductId { get; set; }

        public string Text { get; set; }
        public string AccountAvatar { get; set; }
    }
}
