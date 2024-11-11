using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Exceptions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ExceptionController : BaseApiControllers
    {
        private readonly StoreContext context;
        public ExceptionController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var id = context.products.Find(65);

            if(id == null){
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequestByID(int id)
        {
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var id = context.products.Find(65);
            var result = id.ToString();

            return Ok();
        }

        [HttpGet("endpointnotexists")]
        public ActionResult GetEndPointNotExists()
        {
            var id = context.products.Find(65);

            if(id == null){
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }
    }
}