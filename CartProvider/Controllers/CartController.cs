using CartProvider.Models;
using CartProvider.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CartProvider.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController(ICartRepository repository) : ControllerBase
{
    private readonly ICartRepository _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var cart = await _repository.GetCart();
        return Ok(new
        {
            Items = cart.Items,
            TotalCost = cart.TotalCost
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddCart([FromBody] CartItem cartItem)
    {
        if (cartItem == null || cartItem.Quantity <= 0) 
           return BadRequest("Invalid product");

        await _repository.AddCart(cartItem);
        return Ok("The product has been added to the shopping cart");
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateQuantity(Guid productId, [FromBody] int newQuantity)
    {
        if(newQuantity <= 0)
           BadRequest("You have to add products");

        bool updated = await _repository.UpdateQuantity(productId, newQuantity);
        if (!updated) 
           return BadRequest("The product could not be found");

        return Ok("Qunatity has been added");
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteQuantity(Guid productId)
    {
        bool removed = await _repository.DeleteQuantity(productId);
        if (!removed)
           return BadRequest("The product could not be found");

        return Ok("The product has been successfully removed");
    }
}
