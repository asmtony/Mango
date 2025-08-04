using Mago.Services.CouponAPI.Models.Dto;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class CouponController : BaseController
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }
    public async Task<IActionResult> CouponIndex()
    {
        List<CouponDto> couponList = new();
        ResponseDto? response = await _couponService.GetAllCouponsAsync();
        if (response != null && response.IsSuccess)
        {
            var resultString = response.Result?.ToString();
            if (!string.IsNullOrEmpty(resultString))
            {
                var deserializedList = JsonConvert.DeserializeObject<List<CouponDto>>(resultString);
                if (deserializedList != null)
                {
                    couponList = deserializedList;
                }
            }
            return View(couponList);
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
                SetTempDataMessage("Error retrieving coupons.", ApiStaticUtility.TempDataTypes.Error);
                //TempData["error"] = "Error retrieving coupons.";
            }

        }

        return View();
    }

    public async Task<IActionResult> CreateCoupon()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCoupon(CouponDto coupon)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            //coupon. = User.Identity.Name;
            //coupon.UpdatedBy = User.Identity.Name;
        }
        else
        {
            SetTempDataMessage("You dont have permissions to create a coupon.", ApiStaticUtility.TempDataTypes.Error);
            return RedirectToAction(nameof(CouponIndex));
        }
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _couponService.CreateCouponAsync(coupon);
            if (response != null && response.IsSuccess)
            {
                SetTempDataMessage("Coupon created successfully.", ApiStaticUtility.TempDataTypes.Success);
                //TempData["success"] = "Coupon created successfully.";
                return RedirectToAction(nameof(CouponIndex));
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

    public async Task<IActionResult> DeleteCoupon(int couponId)
    {
        ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);
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
    public async Task<IActionResult> DeleteCoupon(CouponDto coupon)
    {
        ResponseDto? response = await _couponService.DeleteCouponAsync(coupon.CouponId);
        if (response != null && response.IsSuccess)
        {
            SetTempDataMessage($"Deleted record {coupon.CouponId}.", ApiStaticUtility.TempDataTypes.Success);
            //TempData["success"] = $"Deleted record {coupon.CouponId}.";
            return RedirectToAction(nameof(CouponIndex));
        }
        else
        {
            SetTempDataMessage(response.Message, ApiStaticUtility.TempDataTypes.Error);
            //TempData["error"] = response.Message;
        }
        return View(coupon);
    }
}
