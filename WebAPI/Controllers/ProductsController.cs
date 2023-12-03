﻿using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // => Attribute
    public class ProductsController : ControllerBase
    {
        // Loosely coupled
        //IoC Container => Inversion of control
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //Dependency chain

            var result = _productService.GetAll();
            if (result.Success)
            {   //200 => Ok Request
                return Ok(result);
            }
            // 400 => Bad Request
            return BadRequest(result);
        }

        [HttpGet("getbyid")] // => getbyid diyerek alias olarak belirledik.

        public IActionResult GetById(int id) 
        {
            var result = _productService.GetById(id);
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        
        }

        [HttpPost("add")]

        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
