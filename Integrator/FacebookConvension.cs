using Integrator.Models;
using System.Security.Cryptography;
using System.Text;

namespace Integrator
{
    public class FacebookConvension
    {
        private GeneralInformation _info;

        public FacebookConvension(GeneralInformation info)
        {
            _info= info;
        }


        #region  Utilities
        public string Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData?.ToLowerInvariant() ?? String.Empty));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        #endregion

        #region  Data Preparition
        protected CustomerInfoParams PrepareUserData()
        {
            var ipAddress = _info.IP;
            var userAgent = _info.UserAgent; 


            return new CustomerInfoParams
            {
                EmailAddress = new List<string> { Hash(_info.UserMail) },
                FirstName = new List<string> { Hash(_info.UserName) },
                LastName = new List<string> { Hash(_info.UserSurname) },
                PhoneNumber = new List<string> { Hash(_info.UserPhoneNumber) },
                ExternalId = new List<string> { Hash(_info.UserId.ToString()) },
                City = new List<string> { Hash(_info.UserCity) },
                State = new List<string> { Hash(_info.UserState) },
                Country = new List<string> { Hash(_info.UserCountry) },
                ClientIpAddress = ipAddress?.ToLowerInvariant(),
                ClientUserAgent = userAgent?.ToLowerInvariant(),
            };
        }
        private MainBodyParams PrepareAddToCartEventModel(CartProduct item, string cartType)
        {

            var eventObject = new CustomDataParams
            {
                ContentIds = new List<string> { item.Product.Code },
                ContentName = item.Product.Name,
                ContentType = "product",
                ContentCategory = item.Product.MainCategory?.Name,
                Contents = new List<object>
                {
                    new
                    {
                        id = item.Product.Id,
                        quantity = item.Quantity,
                        item_price = item.TotalPrice
                    }
                },
                Currency = item.Product.Currency.ShortName,
                Value = Convert.ToSingle(item.TotalPriceWithDiscount)
            };

            return new MainBodyParams
            {
                Data = new List<ServerEventParams>
                {
                    new ServerEventParams
                    {
                        EventId = "1",
                        EventName =  cartType == "Cart" ? "AddToCart" :
                                        cartType == "Favorite" ? "AddToWishlist" :
                                            cartType == "Discount" ? "AddToWishlist" : "",
                        EventTime = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds(),
                        EventSourceUrl = $"{_httpContextAccessor.HttpContext.Request.Path}{_httpContextAccessor.HttpContext.Request.QueryString}",
                        ActionSource = "website",
                        UserData = PrepareUserData(),
                        CustomData = eventObject
                    }
                }
            };
        }
        private MainBodyParams PrepareSearchModel(string searchTerm)
        {
            var eventObject = new CustomDataParams
            {
                SearchString = JavaScriptEncoder.Default.Encode(searchTerm)
            };

            return new MainBodyParams
            {
                Data = new List<ServerEventParams>
                {
                    new ServerEventParams
                    {
                        EventId = "2",
                        EventName = "Search",
                        EventTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
                        EventSourceUrl = $"{_httpContextAccessor.HttpContext.Request.Path}{_httpContextAccessor.HttpContext.Request.QueryString}",
                        ActionSource = "website",
                        UserData = PrepareUserData(),
                        CustomData = eventObject
                    }
                }
            };
        }
        private MainBodyParams PrepareCompleteRegistrationModel()
        {
            var eventObject = new CustomDataParams
            {
                Status = true.ToString()
            };
            return new MainBodyParams
            {
                Data = new List<ServerEventParams>
                {
                    new ServerEventParams
                    {
                        EventId= "3",
                        EventName = "CompleteRegistration",
                        EventTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
                        EventSourceUrl = $"{_httpContextAccessor.HttpContext.Request.Path}{_httpContextAccessor.HttpContext.Request.QueryString}",
                        ActionSource = "website",
                        UserData = PrepareUserData(),
                        CustomData = eventObject
                    }
                }
            };
        }
        private MainBodyParams PrepareViewContentModel(ProductViewModel productDetails)
        {
            var eventObject = new CustomDataParams
            {
                ContentCategory = productDetails.MainCategory?.Name,
                ContentIds = new List<string> { productDetails.Code },
                ContentName = productDetails?.Name,
                ContentType = "product",
                Currency = productDetails.Currency?.ShortName ?? "try",
                Value = Convert.ToSingle(productDetails.PriceWithDiscount)
            };

            return new MainBodyParams
            {
                Data = new List<ServerEventParams>
                {
                    new ServerEventParams
                    {
                        EventId = "4",
                        EventName = "ViewContent",
                        EventTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
                        EventSourceUrl = $"{_httpContextAccessor.HttpContext.Request.Path}{_httpContextAccessor.HttpContext.Request.QueryString}",
                        ActionSource = "website",
                        UserData = PrepareUserData(),
                        CustomData = eventObject
                    }
                }
            };
        }
        private MainBodyParams PreparePurchaseModel(SalesOrderViewModel order)
        {
            var contentsProperties = (order.OrderProduct.Select(
            item =>
            {
                var content_ids = item?.Code ?? string.Empty;
                var num_items = item.Quantity.ToString() ?? "1";

                return new { id = content_ids, quantity = num_items };
            }).Cast<object>().ToList());


            var eventObject = new CustomDataParams
            {
                ContentType = contentsProperties.Count > 1 ? "product_group" : "product",
                Contents = contentsProperties,
                Currency = order.Currency?.ShortName ?? "try",
                Value = order.Payment.Sum(x => Convert.ToSingle(x.ExpectedTotal))
            };

            return new MainBodyParams
            {
                Data = new List<ServerEventParams>
                {
                    new ServerEventParams
                    {
                        EventId = "5",
                        EventName = "Purchase",
                        EventTime = new DateTimeOffset(order.OrderDate).ToUnixTimeSeconds(),
                        EventSourceUrl = $"{_httpContextAccessor.HttpContext.Request.Path}{_httpContextAccessor.HttpContext.Request.QueryString}",
                        ActionSource = "website",
                        UserData = PrepareUserData(),
                        CustomData = eventObject,
                    }
                }
            };
        }
        private MainBodyParams PrepareInitiateCheckoutModel(CartViewModel cart)
        {
            var contentsProperties = cart.CartProduct.Select(item =>
            {
                var product = item.Product;
                var sku = product != null ? product.Code : string.Empty;
                var quantity = product != null ? (int?)item.Quantity : null;
                return new { id = product.Code, quantity = item.Quantity };
            }).Cast<object>().ToList();

            var eventObject = new CustomDataParams
            {
                ContentType = "product",
                Contents = contentsProperties,
                Currency = cart.Currency?.ShortName ?? "try",
                Value = Convert.ToSingle(cart.TotalWithDiscount)
            };

            return new MainBodyParams
            {
                Data = new List<ServerEventParams>
                {
                    new ServerEventParams
                    {
                        EventName = "InitiateCheckout",
                        EventTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
                        EventSourceUrl = $"{_httpContextAccessor.HttpContext.Request.Path}{_httpContextAccessor.HttpContext.Request.QueryString}",
                        ActionSource = "website",
                        UserData = PrepareUserData(),
                        CustomData = eventObject
                    }
                }
            };
        }

        #endregion

        #region Send Events

        public async Task SendAddToCartEventAsync(CartProduct product, string cartType)
        {
            var addToCartData = PrepareAddToCartEventModel(product, cartType);
            await SendEventAsync(addToCartData).ConfigureAwait(false);
        }
        public async Task SendSearchEventAsync(string searchTerm)
        {
            var searchData = PrepareSearchModel(searchTerm);
            await SendEventAsync(searchData).ConfigureAwait(false);
        }
        public async Task SendCompleteRegistrationEventAsync()
        {
            var completeRegistrationData = PrepareCompleteRegistrationModel();
            await SendEventAsync(completeRegistrationData).ConfigureAwait(false);
        }
        public async Task SendViewContentEventAsync(ProductViewModel productDetailsModel)
        {
            var productViewData = PrepareViewContentModel(productDetailsModel);
            await SendEventAsync(productViewData).ConfigureAwait(false);
        }
        public async Task SendPurchaseEventAsync(SalesOrderViewModel order)
        {
            var purchaseData = PreparePurchaseModel(order);
            await SendEventAsync(purchaseData).ConfigureAwait(false);
        }
        public async Task SendInitiateCheckoutEventAsync(CartViewModel cart)
        {
            var pageData = PrepareInitiateCheckoutModel(cart);
            await SendEventAsync(pageData).ConfigureAwait(false);
        }


        #endregion

        private async Task<string> SendEventAsync(MainBodyParams conversionsEvent)
        {
            _settings.FacebookConversionToken = "EAAGNX2wXpn4BAHZBwoAHZBYEjhGsgToOybVWyddPZC2ZCOA532cZBI1kdtWtAWEE4ghkdUQZCunjhPrZCpX56VQfhCFGUMlml67MP4G4YHWJvTxTOKy4rwgpmS3gSjwqQZAtlGl28Ism5MEnwk4ALDemyCPeBCtkJWzZAtkHn0fbZABWhJC4d7WpqT";
            _settings.FacebookPixelCode = "746412076482654";
            conversionsEvent.TestCode = "TEST26804";
            var apiBase = "https://graph.facebook.com";
            var version = "v14.0";

            var urlString = string.Join($"/", new string[] { apiBase, version, _settings.FacebookPixelCode })
                + $"/events/?access_token=" + _settings.FacebookConversionToken;


            var jsonString = JsonConvert.SerializeObject(conversionsEvent);


            HttpClient _httpClient = new();
            var requestContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(urlString, requestContent);
            var response = result.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }
    }
}