using System.ServiceModel;
using Microsoft.AspNetCore.Mvc;

namespace AuthZ.BackgroundTask.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class InicioController : ControllerBase
    {        
        [HttpGet]        
        //[OperationContract]
        public ActionResult Get()
        {            
            return Ok("AuthZ BackgroundTask");
        }                       
        
    }
}
