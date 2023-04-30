using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;

public class OrderDal : EntityRepositoryBase<Order,NorthwindContext>, IOrderDal
{
    
}