using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GroupOneToDo.Commons;

namespace GroupOneToDo.WebService.Controllers.Api
{
    public abstract class RestControllerBase<TAggregateType, TIdType> : ApiController where TAggregateType : IAggregateRoot<TIdType>
    {

        public abstract IRepository<TAggregateType, TIdType> Repository { get; }

        [HttpGet]
        [Route()]
        public virtual HttpResponseMessage GetAll()
        {
            HttpResponseMessage response;

            try
            {
                IEnumerable<TAggregateType> data = Repository.FindAll();
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
        public virtual HttpResponseMessage GetById(TIdType id)
        {
            HttpResponseMessage response;

            try
            {
                TAggregateType data = Repository.GetById(id);
                response = Request.CreateResponse<object>(HttpStatusCode.OK, data);

            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }


            return response;
        }

        public virtual bool BeforeInsert(TAggregateType entity)
        {
            return true;
        }

        public virtual bool BeforeUpdate(TAggregateType entity)
        {
            return true;
        }

        [HttpPost]
        [Route()]
        public virtual HttpResponseMessage Post([FromBody] TAggregateType entity)
        {
            HttpResponseMessage response;

            try
            {
                if (!BeforeInsert(entity)) throw new Exception("BeforeInsert failed.");

                Repository.Save(entity);
                response = Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }

            return response;
        }

        [HttpPut]
        [Route("{id}")]
        public virtual HttpResponseMessage Update(TIdType id, [FromBody] TAggregateType entity)
        {
            HttpResponseMessage response;

            try
            {

                if (!BeforeUpdate(entity)) throw new Exception("BeforeUpdate failed.");
                Repository.Save(entity);
                response = Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }

            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual HttpResponseMessage Delete(TIdType id)
        {
            HttpResponseMessage response;

            try
            {
                TAggregateType data = Repository.DeleteById(id);
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