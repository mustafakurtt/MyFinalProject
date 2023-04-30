using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class CategoryManager : ICategoryService
{
    private ICategoryDal _categoryDal;

    public CategoryManager(ICategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }


    public IDataResult<List<Category>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IDataResult<Category> GetById(int id)
    {
        throw new NotImplementedException();
    }
}