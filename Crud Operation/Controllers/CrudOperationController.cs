using Crud_Operation.CommonLayer.Model;
using Crud_Operation.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_Operation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudOperationController : ControllerBase
    {

        public readonly ICrudOperationSL _crudOperationSL;
        public CrudOperationController(ICrudOperationSL crudOperationSL)
        {
            _crudOperationSL = crudOperationSL;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecord(CreateRecordRequest request)
        {
            CreateRecordResponse response = null;

            try
            {
                response = await _crudOperationSL.CreateRecord(request);
            } 
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
            
        }
    }
}
