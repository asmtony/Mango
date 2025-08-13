using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.Web.Controllers;

public class HomeController : BaseController
{
    private readonly IProductService _productService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IProductService productService, ILogger<HomeController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
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

    [Authorize]
    public async Task<IActionResult> ProductDetails(int productId)
    {
        ProductDto productList = new();
        ResponseDto? response = await _productService.GetProductByIdAsync(productId);
        if (response != null && response.IsSuccess)
        {
            var resultString = response.Result?.ToString();
            if (!string.IsNullOrEmpty(resultString))
            {
                var deserializedList = JsonConvert.DeserializeObject<ProductDto>(resultString);
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
            }
            else
            {
                SetTempDataMessage("Error retrieving product.", ApiStaticUtility.TempDataTypes.Error);
            }
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
