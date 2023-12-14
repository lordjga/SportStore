using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.ViewModels;
using Catalog.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductViewModel>> GetProductById(string id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("{productName}")]
        [ProducesResponseType(typeof(IList<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductViewModel>>> GetProductByProductName(string productName)
        {
            var result = await _mediator.Send(new GetProductByNameQuery(productName));
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductViewModel>>> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagination<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Pagination<ProductViewModel>>> GetProductsWithFilter([FromQuery] CatalogSpecsParams catalogSpecsParams)
        {
            var result = await _mediator.Send(new GetProductsWithFilterQuery(catalogSpecsParams));
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<ProductBrandViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductBrandViewModel>>> GetAllBrands()
        {
            var result = await _mediator.Send(new GetAllBrandsQuery());
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<ProductTypeViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductTypeViewModel>>> GetAllTypes()
        {
            var result = await _mediator.Send(new GetAllTypesQuery());
            return Ok(result);
        }

        [HttpGet("{brand}")]
        [ProducesResponseType(typeof(IList<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductViewModel>>> GetProductsByBrandName(string brand)
        {
            var result = await _mediator.Send(new GetProductByBrandQuery(brand));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)] 
        public async Task<ActionResult<ProductViewModel>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _mediator.Send(new DeleteProductByIdCommand(id));
            return Ok(result);
        }
    }
}
