using Basket.Business.Services;
using Basket.Common.Exceptions;
using Basket.Common.Surrogates;
using Basket.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : Controller
    {
        readonly IBasketService _basketService;
        readonly ILogger<BasketController> _logger;

        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> AddToBasket(AddProductDto productInfo)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Validation failed!");

                return BadRequest();
            }

            try
            {
                return Ok(await _basketService.AddToBasket(productInfo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (ex is BusinessException)
                {
                    return StatusCode(422);
                }

                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetBasket(int userId)
        {
            if (userId == 0)
            {
                _logger.LogError("Validation failed!");

                return BadRequest();
            }

            try
            {
                var result = await _basketService.GetBasket(userId);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (ex is BusinessException)
                {
                    return StatusCode(422);
                }

                return StatusCode(500);
            }
        }
    }
}
