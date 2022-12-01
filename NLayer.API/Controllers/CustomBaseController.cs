using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    //Bunu burada belirttik birdaha yazmamak için.
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {

        //Burası bir endpoint değil swagger bunu endpoint olarak algılamaması için NoAction yazıyoruz.
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };


            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
