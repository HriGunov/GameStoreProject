using Autofac;
using GameStore.Services.Abstract;

namespace GameStore.Services.Injections
{
    public class GameStoreServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountsService>().As<IAccountsService>().SingleInstance();
            builder.RegisterType<ProductsService>().As<IProductsService>().SingleInstance();
            builder.RegisterType<CryptographicService>().As<ICryptographicService>().SingleInstance();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();
            builder.RegisterType<CommentService>().As<ICommentService>().SingleInstance();
            builder.RegisterType<ShoppingCartsService>().As<IShoppingCartsService>().SingleInstance();
            builder.RegisterType<OrderService>().As<IOrderService>().SingleInstance();
            builder.RegisterType<SaveContextService>().As<ISaveContextService>().SingleInstance();
        }
    }
}