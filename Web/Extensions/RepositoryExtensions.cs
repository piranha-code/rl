using Domain.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNet.OData.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Web.Extensions
{
    public static class RepositoryExtensions
    {   
        public static IQueryable Find<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,              
            ODataQueryOptions queryOptions,
            ODataQuerySettings querySettings,
            ODataValidationSettings validationSettings)
            where TEntity : class 
        {           
            /*if(queryOptions == null)
                 throw new ArgumentNullException(nameof(queryOptions));
            if(querySettings == null)
                 throw new ArgumentNullException(nameof(querySettings));
            if(validationSettings == null)
                 throw new ArgumentNullException(nameof(validationSettings));*/            

            //queryOptions.Validate(validationSettings);
            
            if(repository is EfRepository<TEntity, TKey> efRepository)
                return queryOptions.ApplyTo(efRepository.AsQueryable());//querySettings)

            throw new NotImplementedException();
        }
    }
}