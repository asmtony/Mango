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

    public async Task<IActionResult> EditProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(ProductDto product)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            //product. = User.Identity.Name;
            //product.UpdatedBy = User.Identity.Name;
        }
        else
        {
            SetTempDataMessage("You dont have permissions to edit a product.", ApiStaticUtility.TempDataTypes.Error);
            return RedirectToAction(nameof(ProductIndex));
        }
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _productService.CreateProductAsync(product);
            if (response != null && response.IsSuccess)
            {
                SetTempDataMessage("product updated successfully.", ApiStaticUtility.TempDataTypes.Success);
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
                    SetTempDataMessage("Error editing product.", ApiStaticUtility.TempDataTypes.Error);
                    //TempData["error"] = "Error creating product.";
                }
            }
        }
        return View();
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
            //product.UpdatedBy = User.Identity.Name;
        }
        else
        {
            SetTempDataMessage("You dont have permissions to create a product.", ApiStaticUtility.TempDataTypes.Error);
            return RedirectToAction(nameof(ProductIndex));
        }
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _productService.CreateProductAsync(product);
            if (response != null && response.IsSuccess)
            {
                SetTempDataMessage("product created successfully.", ApiStaticUtility.TempDataTypes.Success);
                //TempData["success"] = "product created successfully.";
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
                    SetTempDataMessage("Error creating product.", ApiStaticUtility.TempDataTypes.Error);
                    //TempData["error"] = "Error creating product.";
                }
            }
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        ResponseDto? response = await _productService.GetProductByIdAsync(productId);
        if (response != null && response.IsSuccess)
        {
            var resultString = response.Result?.ToString();
            if (!string.IsNullOrEmpty(resultString))
            {
                var deserializedProduct = JsonConvert.DeserializeObject<ProductDto>(resultString);
                if (deserializedProduct != null && deserializedProduct.ProductId > 0)
                {
                    return View(deserializedProduct);
                }
            }
            else
            {
                SetTempDataMessage($"Error deleteing product - {response.Message}", ApiStaticUtility.TempDataTypes.Error);
                //TempData["error"] = $"Error deleteing product - {response.Message}";
            }
        }
        return View(response.Result);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(ProductDto product)
    {
        ResponseDto? response = await _productService.DeleteProductAsync(product.ProductId);
        if (response != null && response.IsSuccess)
        {
            SetTempDataMessage($"Deleted record {product.ProductId}.", ApiStaticUtility.TempDataTypes.Success);
            //TempData["success"] = $"Deleted record {product.productId}.";
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
