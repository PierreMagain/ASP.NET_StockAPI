using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.BLL.Interfaces;
using Stock.Domain.Entities;
using Stock.API.Models;
using Stock.API.Mappers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductShortDTO>))]
        public ActionResult<IEnumerable<ProductShortDTO>> GetAll()
        {
            return Ok(_productService.GetAll().Select(p => p.ToShortDTO()));
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ProductDetailsDTO> GetById([FromRoute] int id)
        {
            Claim? claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            int userId = int.Parse(claimId.Value);

            if(userId != 1)
            {
                return Unauthorized("Seul l'admin peut voir les détails des produits");
            }

            if (id <= 0)
                return BadRequest("L'ID du produit doit être positif");

            try
            {
                return Ok(_productService.GetById(id).ToDetailsDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> Create([FromBody] ProductFormDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int productId = _productService.Create(product.ToProduct());
                return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("L'ID du produit doit être positif");

            try
            {
                bool isDeleted = _productService.Delete(id);

                if (isDeleted)
                {
                    return NoContent();
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([FromRoute] int id, [FromBody] ProductFormDTO product)
        {
            if (id <= 0)
                return BadRequest("L'ID du produit doit être positif");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool isUpdated = _productService.Update(id, product.ToProduct());
                if (isUpdated)
                {
                    return NoContent();
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return BadRequest();
        }
    }
}
