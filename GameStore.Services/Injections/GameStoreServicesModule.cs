using Autofac;
using GameStore.Services.Abstract;

namespace GameStore.Services.Injections
{
    public class GameStoreServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountsService>().As<IAccountsService>();
            builder.RegisterType<ProductsService>().As<IProductsService>();
            builder.RegisterType<CryptographicService>().As<ICryptographicService>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<CommentService>().As<ICommentService>();
            builder.RegisterType<ShoppingCartsService>().As<IShoppingCartsService>();
        }
    }
}