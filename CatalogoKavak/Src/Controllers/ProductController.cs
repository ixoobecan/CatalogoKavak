using CatalogoKavak.Src.Models;
using CatalogoKavak.Src.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CatalogoKavak.Src.Controllers
{
    [ApiController]
    [Route("api/Product")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        #region Attribute
        private readonly IProduct _repository;
        #endregion

        #region Controllers
        public ProductController(IProduct repository)
        {
            _repository = repository;
        }
        #endregion

        [HttpGet]
        #region Metodos
        [HttpGet("allProducts")]
        public async Task<ActionResult> TakeAllProductAsync()
        {
            var lista = await _repository.TakeAllProductAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }
        /// <summary>
        /// Pegar produto pelo Id
        /// </summary>
        /// <param name="idProduct">Id do produto(carro)</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Id encontrado</response> 
        /// <response code="404">Id não existente</response>
        [HttpGet("idProduct/{idProduct}")]
        public async Task<ActionResult> TakeProductByIdAsync([FromRoute] int idProduct)
        {
            try
            {
                return Ok(await _repository.TakeProductByIdAsync(idProduct));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
        /// <summary>
        /// Criar novo produto
        /// </summary> 
        /// <param name="product">Contrutor para criar produto</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/Product/newProduct
        ///     { 
        ///         "Nome": "Nome do anuncio",
        ///         "Descricao": "descricao do carro",
        ///         "Marca": "Chevrolet",
        ///         "Modelo": "Onix",
        ///         "Foto": "URL_Onix",
        ///         "Preco": "45.522",
        ///  
        ///     } 
        ///     
        /// </remarks> 
        /// <response code="201">Retorna Anuncio criado</response> 
        /// <response code="422">Anuncio ja cadastrado</response>
        [HttpPost("newProduct")]
        public async Task<ActionResult> NewProductAsync([FromBody] Product product)
        {
            await _repository.NewProductAsync(product);
            return Created($"api/Product", product);
        }
        /// <summary>
        /// Atualizar produto
        /// </summary> 
        /// <param name="product">Construtor para atualizar produto</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     PUT /api/Product/UpdateProduct 
        ///     { 
        ///         "id": 0,
        ///         "Nome": "Nome do anuncio",
        ///         "Descricao": "descricao do carro",
        ///         "Marca": "Chevrolet",
        ///         "Modelo": "Onix",
        ///         "Foto": "URL_Onix",
        ///         "Preco": "45.522",
        ///     }
        ///     
        /// </remarks> 
        /// <response code="200">Anunio atualizado</response> 
        /// <response code="400">Erro na requisição</response>
        [HttpPut("UpdateProduct")]
        public async Task<ActionResult> UpdateProductAsync([FromBody] Product product)
        {
            try
            {
                await _repository.UpdateProductAsync(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }
        /// <summary>
        /// Deletar produto
        /// <para>Função assíncrona para deletar produto pelo Id</para>
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Anuncio deletado</response>
        /// <response code="404">Id do produto não existe</response>
        [HttpDelete("deleteProduct/{idProduct}")]
        public async Task<ActionResult> DeleteProductAsync([FromRoute] int idProduct)
        {
            try
            {
                await _repository.DeleteProductAsync(idProduct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
        #endregion
 
    }
}
