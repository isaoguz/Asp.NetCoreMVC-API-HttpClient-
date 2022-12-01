using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute:ActionFilterAttribute
    {
        //Biz burada çalışırken, çalışmadan önce, çalıştıkdan sonra gibi noktaları yönetebiliyoruz.
        //Buraya yazdığın her şey service katmanında tanımladığın validasyonlar ile entegreli geliyor.
        //Bu kodu sisteme entegre etmek için Class isimini controllerda ki classların üstüne yazmamız gerekir. ancak global olduğu için program.cs de tanımlarız.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Bir hata varsa
            if (!context.ModelState.IsValid)
            {
                //Burada hataları listeledik.
                var errors = context.ModelState.Values.SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage).ToList();

                //Burada eğer client eksik bir yer girerse onun karşısına 400 hatasını ve erroru çıkarıyoruz.
                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors)); 

            }
        }
    }
}
