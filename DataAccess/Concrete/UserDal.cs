﻿using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete;

public class UserDal : EntityRepositoryBase<User, NorthwindContext>, IUserDal
{
    public List<OperationClaim> GetClaims(User user)
    {
        using (var context = new NorthwindContext())
        {
            var result = from operationClaim in context.OperationClaims
                join userOperationClaim in context.UserOperationClaims
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                where userOperationClaim.UserId == user.Id
                select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();

        }
    }
}