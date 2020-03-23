using System;
using AutoMapper;
using Infrastructure.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Web.Attributes;
using Web.Extensions;

namespace Web.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route( "v{version:apiVersion}/[controller]" )]
    [GenericController]
    public class GenericController<TEntity, TKey> : ODataController
    where TEntity : class
    {
        readonly IRepository<TEntity, TKey> _repository;
        readonly ODataValidationSettings _validationSettings;
        readonly ODataQuerySettings _querySettings;
        readonly IMapper _mapper;

        public GenericController(
            IRepository<TEntity, TKey> repository,
            IMapper mapper)
        //ODataValidationSettings validationSettings,
        //ODataQuerySettings querySettings)
        //ILogger<CrudController<TEntity, TKey>> logger = null)
        {
            //_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_validationSettings = validationSettings ?? throw new ArgumentNullException(nameof(validationSettings));
            //_querySettings = querySettings ?? throw new ArgumentNullException(nameof(querySettings));
            //_logger = logger;// ?? throw new ArgumentNullException(nameof(logger));
        }

        //[Authorize(Roles = "guest")]
        [HttpGet]
        public virtual IActionResult Get(ODataQueryOptions<TEntity> queryOptions)
        {
            if (queryOptions == null)
                throw new ArgumentNullException(nameof(queryOptions));

            var data = _repository.Find<TEntity, TKey>(queryOptions, _querySettings, _validationSettings);
            //_mapper.Map<TEntity>()

            return Ok(data);
        }

        /*[HttpGet]
        public virtual TEntity GetById([FromODataUri] TKey key)//, ODataQueryOptions<TEntity> queryOptions)
		{
            return _repository.Get(key);
        }

        [HttpDelete]
        public virtual void Delete([FromODataUri] TKey key)
		{
            _repository.Delete(key);
		}

        public virtual ObjectResult Put([FromODataUri] TKey key, [FromBody] TEntity update)
		{
			if(update == null)
                 throw new ArgumentNullException(nameof(update));

			TEntity updatedEntity = UpdateEntity(key, update);

			return Updated(updatedEntity);
		}

        public virtual ObjectResult Patch([FromODataUri] TKey key, Delta<TEntity> patch)
		{
			if(patch == null)
                 throw new ArgumentNullException(nameof(patch));

			TEntity patchedEntity = PatchEntity(key, patch);

			return Updated(patchedEntity);
		}*/
    }
}