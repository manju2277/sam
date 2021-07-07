﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Smartstore.ComponentModel;
using Smartstore.Core;
using Smartstore.Core.Catalog;
using Smartstore.Core.Catalog.Attributes;
using Smartstore.Core.Checkout.Cart;
using Smartstore.Core.Content.Media;
using Smartstore.Core.Localization;
using Smartstore.Core.Security;
using Cart = Smartstore.Core.Checkout.Cart;

namespace Smartstore.Web.Models.ShoppingCart
{
    public static partial class ShoppingCartMappingExtensions
    {
        public static async Task MapAsync(this Cart.ShoppingCart cart,
            WishlistModel model,
            bool isEditable = true,
            bool isOffcanvas = false)
        {
            dynamic parameters = new ExpandoObject();
            parameters.IsEditable = isEditable;
            parameters.IsOffcanvas = isOffcanvas;

            await MapperFactory.MapAsync(cart, model, parameters);
        }
    }

    public class WishlistModelMapper : CartMapperBase<WishlistModel>
    {
        private readonly IShoppingCartValidator _shoppingCartValidator;
        private readonly IProductAttributeFormatter _productAttributeFormatter;

        public WishlistModelMapper(
            ICommonServices services,
            IShoppingCartValidator shoppingCartValidator,
            IProductAttributeFormatter productAttributeFormatter,
            ShoppingCartSettings shoppingCartSettings,
            CatalogSettings catalogSettings,
            MediaSettings mediaSettings,
            Localizer T)
            : base(services, shoppingCartSettings, catalogSettings, mediaSettings, T)
        {
            _shoppingCartValidator = shoppingCartValidator;
            _productAttributeFormatter = productAttributeFormatter;
        }

        protected override void Map(Cart.ShoppingCart from, WishlistModel to, dynamic parameters = null)
            => throw new NotImplementedException();

        public override async Task MapAsync(Cart.ShoppingCart from, WishlistModel to, dynamic parameters = null)
        {
            Guard.NotNull(from, nameof(from));
            Guard.NotNull(to, nameof(to));

            if (!from.Items.Any())
            {
                return;
            }

            await base.MapAsync(from, to, null);

            to.IsEditable = parameters?.IsEditable == true;
            to.EmailWishlistEnabled = _shoppingCartSettings.EmailWishlistEnabled;
            to.DisplayAddToCart = await _services.Permissions.AuthorizeAsync(Permissions.Cart.AccessShoppingCart);

            to.CustomerGuid = from.Customer.CustomerGuid;
            to.CustomerFullname = from.Customer.GetFullName();
            to.ShowItemsFromWishlistToCartButton = _shoppingCartSettings.ShowItemsFromWishlistToCartButton;

            // Cart warnings.
            var warnings = new List<string>();
            if (!await _shoppingCartValidator.ValidateCartAsync(from, warnings))
            {
                to.Warnings.AddRange(warnings);
            }

            foreach (var cartItem in from.Items)
            {
                var model = new WishlistModel.WishlistItemModel
                {
                    DisableBuyButton = cartItem.Item.Product.DisableBuyButton,
                };

                await cartItem.MapAsync(model);

                if (parameters?.IsOffcanvas == true)
                {
                    model.QuantityUnitName = null;

                    var item = from.Items.Where(c => c.Item.Id == model.Id).FirstOrDefault();
                    if (item != null)
                    {
                        model.AttributeInfo = await _productAttributeFormatter.FormatAttributesAsync(
                            item.Item.AttributeSelection,
                            item.Item.Product,
                            null,
                            htmlEncode: false,
                            separator: ", ",
                            includePrices: false,
                            includeGiftCardAttributes: false,
                            includeHyperlinks: false);
                    }
                }

                to.AddItems(model);
            }
        }
    }
}
