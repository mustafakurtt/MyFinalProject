using System.Linq.Expressions;
using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;

public class CategoryDal : EntityRepositoryBase<Category,NorthwindContext>, ICategoryDal
{
    
}