using System.ComponentModel.DataAnnotations;

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

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(250, MinimumLength = 2)]
        public string Text { get; set; }
    }
}