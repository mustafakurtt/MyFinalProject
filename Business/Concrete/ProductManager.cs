using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
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

    [SecuredOperation("admin")]
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

    [SecuredOperation("product.add,admin")]
    [ValidationAspect(typeof(ProductValidator))]
    public IResult Add(Product product)
    {
        IResult result = BusinessRules.Run(
            CheckIfProductCountOfCategoryCorrect(product.CategoryId), 
            CheckIfProductNameExists(product.ProductName));
        
        if (result != null)
        {
            return result;
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


    private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
    {
        var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
        if (result >= 15)
        {
            return new ErrorResult(Messages.ProductCountOfCategoryError);
        }

        return new SuccessResult();
    }

    private IResult CheckIfProductNameExists(string productName)
    {

        var result = _productDal.GetAll(p => p.ProductName == productName).Any();
        if (result)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }

        return new SuccessResult();
    }
}