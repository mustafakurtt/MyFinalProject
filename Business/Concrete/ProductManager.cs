using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private IProductDal _productDal;

    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;
    }

    public IDataResult<List<Product>> GetAll()
    {
       
        List<Product> data = _productDal.GetAll();
        return new SuccessDataResult<List<Product>>(data);
    }

    public IDataResult<List<Product>> GetAllByCategoryId(int id)
    {
        List<Product> data = _productDal.GetAll(p => p.CategoryId == id);
        return new SuccessDataResult<List<Product>>(data);
    }

    public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
    {
        List<Product> data = _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        return new SuccessDataResult<List<Product>>(data);
    }

    public IDataResult<List<ProductDetailDto>> GetProductDetails()
    {
        if (DateTime.Now.Hour == 11)
        {
            return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
        }
        List<ProductDetailDto> data = _productDal.GetProductDetails();
        return new SuccessDataResult<List<ProductDetailDto>>(data);
    }

    public IDataResult<Product> GetById(int productId)
    {
        Product data = _productDal.Get(p => p.ProductId == productId);
        return new SuccessDataResult<Product>(data);
    }

    public IResult Add(Product product)
    {
        if (product.ProductName.Length < 2)
        {
            return new ErrorResult("Ürün ismi en az 2 karakter olmalıdır");
        }

        _productDal.Add(product);
        return new SuccessResult("Ürün Eklendi");
    }

    public IResult Update(Product product)
    {
        _productDal.Update(product);
        return new SuccessResult("Product Updated");
    }

    public IResult Delete(Product product)
    {
        _productDal.Delete(product);
        return new SuccessResult("Product Deleted");
    }
}