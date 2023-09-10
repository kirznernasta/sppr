using System;
using System.Text.Json.Serialization;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Extensions;

namespace WEB_153502_KIRZNER.Services.CartService
{
	public class SessionCart : Cart
	{
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
            .HttpContext?.Session;
            SessionCart cart = session?.Get<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }

        public override void AddToCart(Product product)
        {
            base.AddToCart(product);
            Session?.Set("Cart", this);
        }

        public override void RemoveItems(int id)
        {
            base.RemoveItems(id);
            Session?.Set<SessionCart>("Cart", this);
        }

        public override void ClearAll()
        {
            base.ClearAll();
            Session?.Remove("Cart");
        }
    }
}

