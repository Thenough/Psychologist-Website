using Core.DTOs;
using Core.Models.Abstarct;
using Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PsikoWebApi.Filter
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);
            if(anyEntity)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDTO<NoContent>.Fail(404,$"{typeof(T).Name}({id}) Not Found"));
        }
    }
}
