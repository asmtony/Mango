using AutoMapper;
using Mago.Services.CouponAPI.Data;
using Mago.Services.CouponAPI.Models.DbModels;
using Mago.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mago.Services.CouponAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponApiController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CouponApiController(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCoupons()
    {
        ResponseDto<IEnumerable<CouponDto>> responseDto = new();
        //IEnumerable<CouponDto> couponsDto;
        try
        {
            IEnumerable<Coupon> coupons = _appDbContext.Coupons.ToList();

            if (coupons == null || !coupons.Any())
            {
                return NotFound("No coupons found.");
            }
            responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);

        }
        catch (Exception)
        {
            throw;
        }

        return Ok(responseDto);
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetCouponById(int id)
    {
        ResponseDto<CouponDto> responseDto = new();

        try
        {
            Coupon coupon = _appDbContext.Coupons.FirstOrDefault(u => u.CouponId == id) ?? new Coupon();

            if (coupon == null || coupon.CouponId <= 0)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ($"No coupon found for Id {id}.");
            }
            else
            {
                CouponDto couponDto = _mapper.Map<CouponDto>(coupon);
                responseDto.Result = couponDto;
            }
        }
        catch (Exception)
        {
            throw;
        }

        return Ok(responseDto);
    }

    [HttpGet]
    [Route("GetCouponByCode/{couponCode}")]
    public IActionResult GetCouponByCode(string couponCode)
    {
        ResponseDto<CouponDto> responseDto = new();

        try
        {
            Coupon coupon = _appDbContext.Coupons.FirstOrDefault(u => u.CouponCode == couponCode) ?? new Coupon();

            if (coupon == null || coupon.CouponId <= 0)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ($"No coupon found for coupon code {couponCode}.");
            }
            else
            {
                CouponDto couponDto = _mapper.Map<CouponDto>(coupon);
                responseDto.Result = couponDto;
            }
        }
        catch (Exception)
        {
            throw;
        }

        return Ok(responseDto);
    }

    [HttpPost]
    //[Route("GetCouponByCode/{couponCode}")]
    public IActionResult Post([FromBody] CouponDto couponDto)
    {
        ResponseDto<CouponDto> responseDto = new();

        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _appDbContext.Add(coupon);
            _appDbContext.SaveChanges();

            CouponDto createdCouponDto = _mapper.Map<CouponDto>(coupon);
            responseDto.Result = createdCouponDto;
        }
        catch (Exception)
        {
            throw;
        }

        return Ok(responseDto);
    }

    [HttpPut]
    //[Route("GetCouponByCode/{couponCode}")]
    public IActionResult Put([FromBody] CouponDto couponDto)
    {
        ResponseDto<CouponDto> responseDto = new();

        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _appDbContext.Update(coupon);
            _appDbContext.SaveChanges();

            CouponDto createdCouponDto = _mapper.Map<CouponDto>(coupon);
            responseDto.Result = createdCouponDto;
        }
        catch (Exception)
        {
            throw;
        }

        return Ok(responseDto);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete(int id)
    {
        ResponseDto<CouponDto> responseDto = new();

        try
        {
            Coupon coupon = _appDbContext.Coupons.FirstOrDefault(u => u.CouponId == id) ?? new Coupon();
            _appDbContext.Remove(coupon);
            _appDbContext.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }

        return Ok(responseDto);
    }
}
