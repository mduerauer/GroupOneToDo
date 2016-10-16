using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GroupOneToDo.Commons;
using System.Threading.Tasks;

namespace GroupOneToDo.WebService.Controllers.Api
{
    public abstract class RestControllerBase<TAggregateType, TIdType> : ApiController where TAggregateType : IAggregateRoot<TIdType>
    {

        public abstract IAsyncRepository<TAggregateType, TIdType> Repository { get; }

        [HttpGet]
        [Route()]
        public virtual async Task<HttpResponseMessage> GetAll()
        {
            HttpResponseMessage response;

            try
            {
                IEnumerable<TAggregateType> data = await Repository.FindAll();
                response = Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
            return response;
        }


        [HttpGet]
        [Route("{id}")]
        public virtual async Task<HttpResponseMessage> GetById(TIdType id)
        {
            HttpResponseMessage response;

            try
            {
                TAggregateType data = await Repository.GetById(id);
                response = Request.CreateResponse<object>(HttpStatusCode.OK, data);

            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }


            return response;
        }

        public virtual void AfterInsert(TAggregateType entity) { }

        public virtual bool BeforeInsert(TAggregateType entity)
        {
            return true;
        }

        public virtual void AfterUpdate(TAggregateType entity) { }

        public virtual bool BeforeUpdate(TAggregateType entity)
        {
            return true;
        }

        public virtual void AfterDelete(TAggregateType entity) { }

        [HttpPost]
        [Route()]
        public virtual async Task<HttpResponseMessage> Post([FromBody] TAggregateType entity)
        {
            HttpResponseMessage response;

            try
            {
                if (!BeforeInsert(entity)) throw new Exception("BeforeInsert failed.");

                var result = await Repository.Save(entity);

                AfterInsert(result);

                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }

            return response;
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<HttpResponseMessage> Update(TIdType id, [FromBody] TAggregateType entity)
        {
            HttpResponseMessage response;

            try
            {

                if (!BeforeUpdate(entity)) throw new Exception("BeforeUpdate failed.");
                var result = await Repository.Save(entity);

                AfterUpdate(result);

                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }

            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<HttpResponseMessage> Delete(TIdType id)
        {
            HttpResponseMessage response;

            try
            {
                TAggregateType data = await Repository.DeleteById(id);

                AfterDelete(data);

                response = Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
            return response;
        }
    }
}