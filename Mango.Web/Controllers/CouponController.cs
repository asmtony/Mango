using Mago.Services.CouponAPI.Models.Dto;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
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
                TempData["error"] = "Error retrieving coupons.";
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
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(coupon);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon created successfully.";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = "Error creating coupon.";
                }
            }
            return View();
        }

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
                    TempData["error"] = $"Error deleteing coupon - {response.Message}";
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
                TempData["success"] = $"Deleted record {coupon.CouponId}.";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return View(coupon);
        }
    }
}
