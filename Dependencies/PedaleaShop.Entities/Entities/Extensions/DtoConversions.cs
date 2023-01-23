using PedaleaShop.Entities.Dtos;
using System.Data;

namespace PedaleaShop.Entities.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductCategoryDto> ConvertToProductCategoryDto(this DataTable Categories)
        {
            return Categories.AsEnumerable().Select(row => new ProductCategoryDto
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = Convert.ToString(row["Name"]),
                IconCSS = Convert.ToString(row["IconCSS"]),
            });
        }
        public static IEnumerable<ProductsColorDto> ConvertToProductColorDto(this DataTable Colors)
        {
            return Colors.AsEnumerable().Select(row => new ProductsColorDto
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = Convert.ToString(row["Name"]),
                IconCSS = Convert.ToString(row["IconCSS"]),
            });
        }
        public static IEnumerable<ProductsSizeDto> ConvertToProductSizeDto(this DataTable Sizes)
        {
            return Sizes.AsEnumerable().Select(row => new ProductsSizeDto
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = Convert.ToString(row["Name"]),
                IconCSS = Convert.ToString(row["IconCSS"]),
            });
        }
        public static IEnumerable<ProductsDto> ConvertToProductDto(this DataTable products)
            {
                return products.AsEnumerable().Select(row => new ProductsDto
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"]),
                    Description = Convert.ToString(row["Description"]),
                    ImageURL = Convert.ToString(row["ImageURL"]),
                    Price = Convert.ToDecimal(row["Price"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    ColorId = Convert.ToInt32(row["ColorId"]),
                    SizeId = Convert.ToInt32(row["SizeId"]),
                    CategoryName = Convert.ToString(row["CategoryName"]),
                    ColorName = Convert.ToString(row["ColorName"]),
                    SizeName = Convert.ToString(row["SizeName"])

                });
            }
        public static IEnumerable<ShoppingCartItemDto>? ConvertToShoppingCartItemDto(this DataTable shoppingCartItem)
        {
            DataColumnCollection columns = shoppingCartItem.Columns;
            if (shoppingCartItem.Columns.Contains("Result"))
            {
                if (Convert.ToInt32(shoppingCartItem.Rows[0]["Result"]) == 0) return null;
            }
            return shoppingCartItem.AsEnumerable().Select(row => new ShoppingCartItemDto
            {
                Id=Convert.ToInt32(row["Id"]),
                ProductId=Convert.ToInt32(row["ProductId"]),
                CartId=Convert.ToInt32(row["CartId"]),
                Separated = Convert.ToBoolean(row["Separated"]),
                Paid = Convert.ToBoolean(row["Paid"]),
                // separatePlane=Convert.ToInt32(row["separatePlane"]),
                ProductName =Convert.ToString(row["ProductName"]),
                ProductDescription=Convert.ToString(row["ProductDescription"]),
                ProductImageURL=Convert.ToString(row["ProductImageURL"]),
                Price=Convert.ToDecimal(row["Price"]),
                TotalPrice=Convert.ToDecimal(row["TotalPrice"]),
                Quantity=Convert.ToInt32(row["Quantity"]),

            });
        }

    }
}
