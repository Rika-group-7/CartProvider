using CartProvider.DataContext;
using CartProvider.Models;
using CartProvider.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace CartProvider.Test;

public class UnitTest1
{
    private static ApiContext GetInMemoryDataContext()
    {
        var option = new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        return new ApiContext(option);

    }

    [Fact]
    public async Task AddToCartAsync_AddNewItemToCart()
    {
        //Arrange
        var context = GetInMemoryDataContext();
        var repository = new CartRepository(context);
        var cartItem = new CartItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            Quantity = 3,
            Price = 10.99m
        };
        await context.SaveChangesAsync();

        //Act
        await repository.AddCart(cartItem);

        //Assert
        var result = await context.DbCartItems.FirstOrDefaultAsync(e => e.ProductId == cartItem.ProductId);
        Assert.NotNull(result);
        Assert.Equal(cartItem.ProductName, result.ProductName);
        Assert.Equal(cartItem.Quantity, result.Quantity);
        Assert.Equal(cartItem.Price, result.Price);

    }

    [Fact]
    public async Task UppdateQuantityAsync_UppdateItemQuantity()
    {
        //Arrange
        var context = GetInMemoryDataContext();
        var repository = new CartRepository(context);
        var productId = Guid.NewGuid();
        var cartItem = new CartItem
        {
            ProductId = productId,
            ProductName = "Test Product",
            Quantity = 6,
            Price = 100.49m
        };
        await context.DbCartItems.AddAsync(cartItem);
        await context.SaveChangesAsync();

        //Act
        var newQuantity = 5;
        var update = await repository.UpdateQuantity(productId, newQuantity);

        //Assert
        var result = await context.DbCartItems.FirstOrDefaultAsync(e => e.ProductId == cartItem.ProductId);
        Assert.True(update);
        Assert.Equal(newQuantity, result!.Quantity);
    }

    [Fact]

    public async Task GetCartAsync_ReturnCardWithTotalCost()
    {
        //Arrange
        var context = GetInMemoryDataContext();
        var repository = new CartRepository(context);
        var item1 = new CartItem { ProductId = Guid.NewGuid(), ProductName = "Product 1", Quantity = 2, Price = 20.00m };
        var item2 = new CartItem { ProductId = Guid.NewGuid(), ProductName = "Product 2", Quantity = 1, Price = 10.00m };
        await context.DbCartItems.AddRangeAsync(item1, item2);
        await context.SaveChangesAsync();


        //Act
        var cart = await repository.GetCart();

        //Assert
        decimal expectedTotalCost = item1.Price * item1.Quantity + item2.Price * item2.Quantity;
        Assert.Equal(2, cart.Items.Count);
        Assert.Equal(expectedTotalCost, cart.TotalCost);
    }

    [Fact]
    public async Task DeleteCartAsync_DeleteItemFromCart()
    {
        //Arrange
        var context = GetInMemoryDataContext();
        var repository = new CartRepository(context);
        var productId = Guid.NewGuid();
        var cartItem = new CartItem 
        {
            ProductId = productId,
            ProductName = "Test Product",
            Quantity = 1,
            Price = 10.00m
        };
        await context.DbCartItems.AddAsync(cartItem);
        await context.SaveChangesAsync();

        //Act
        var removed = await repository.DeleteQuantity(productId);

        //Assert
        Assert.True(removed);
        
    }
    
}