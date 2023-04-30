// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using DataAccess.Concrete;
using Entities.DTOs;


ProductManager manager = new ProductManager(new ProductDal());
var data = manager.GetProductDetails();
if (data.Success)
{
    foreach (ProductDetailDto dto in data.Data)
    {
        Console.WriteLine(dto.ProductId);
        Console.WriteLine(dto.ProductName);
        Console.WriteLine(dto.CategoryName);
        Console.WriteLine(dto.UnitsInStock);
        Console.WriteLine("-*-");

    }
}
else
{
    Console.WriteLine(data.Message);
}