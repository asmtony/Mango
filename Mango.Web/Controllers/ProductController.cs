using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class ProductController : BaseController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto> productList = new();
        ResponseDto? response = await _productService.GetAllProductsAsync();
        if (response != null && response.IsSuccess)
        {
            var resultString = response.Result?.ToString();
            if (!string.IsNullOrEmpty(resultString))
            {
                var deserializedList = JsonConvert.DeserializeObject<List<ProductDto>>(resultString);
                if (deserializedList != null)
                {
                    productList = deserializedList;
                }
            }
            return View(productList);
        }
        else
        {
            if (response != null)
            {
                SetTempDataMessage(response.Message, ApiStaticUtility.TempDataTypes.Error);
                //TempData["error"] = response.Message;
            }
            else
            {
                SetTempDataMessage("Error retrieving products.", ApiStaticUtility.TempDataTypes.Error);
                //TempData["error"] = "Error retrieving products.";
            }

        }

        return View();
    }

    public async Task<IActionResult> CreateProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductDto product)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            //product. = User.Identity.Name;
            //coupon.UpdatedBy = User.Identity.Name;
        }
        else
        {
            SetTempDataMessage("You dont have permissions to create a coupon.", ApiStaticUtility.TempDataTypes.Error);
            return RedirectToAction(nameof(ProductIndex));
        }
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _productService.CreateProductAsync(product);
            if (response != null && response.IsSuccess)
            {
                SetTempDataMessage("Coupon created successfully.", ApiStaticUtility.TempDataTypes.Success);
                //TempData["success"] = "Coupon created successfully.";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                if (response != null)
                {
                    SetTempDataMessage(response.Message, ApiStaticUtility.TempDataTypes.Error);
                    //TempData["error"] = response.Message;
                }
                else
                {
                    SetTempDataMessage("Error creating coupon.", ApiStaticUtility.TempDataTypes.Error);
                    //TempData["error"] = "Error creating coupon.";
                }
            }
        }
        return View();
    }

    //private void SetTempDataMessage(ResponseDto? response, ApiStaticUtility.TempDataTypes tempDataTypes)
    //{
    //    if (response != null && !string.IsNullOrEmpty(response.Message))
    //    {
    //        TempData[responseType] = response.Message;
    //    }
    //    else
    //    {
    //        TempData["error"] = "An error occurred while processing your request.";
    //    }
    //}

    public async Task<IActionResult> DeleteProduct(int productId)
    {
        ResponseDto? response = await _productService.GetProductByIdAsync(productId);
        if (response != null && response.IsSuccess)
        {
            var resultString = response.Result?.ToString();
            if (!string.IsNullOrEmpty(resultString))
            {
                var deserializedCoupon = JsonConvert.DeserializeObject<CouponDto>(resultString);
                if (deserializedCoupon != null && deserializedCoupon.CouponId > 0)
                {
                    return View(deserializedCoupon);
                }
            }
            else
            {
                SetTempDataMessage($"Error deleteing coupon - {response.Message}", ApiStaticUtility.TempDataTypes.Error);
                //TempData["error"] = $"Error deleteing coupon - {response.Message}";
            }
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(ProductDto product)
    {
        ResponseDto? response = await _productService.DeleteProductAsync(product.ProductId);
        if (response != null && response.IsSuccess)
        {
            SetTempDataMessage($"Deleted record {product.ProductId}.", ApiStaticUtility.TempDataTypes.Success);
            //TempData["success"] = $"Deleted record {coupon.CouponId}.";
            return RedirectToAction(nameof(ProductIndex));
        }
        else
        {
            SetTempDataMessage(response.Message, ApiStaticUtility.TempDataTypes.Error);
            //TempData["error"] = response.Message;
        }
        return View(product);
    }
}
