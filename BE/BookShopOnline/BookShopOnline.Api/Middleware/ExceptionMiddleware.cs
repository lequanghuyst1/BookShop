using BookShopOnline.Core.Dto;
using BookShopOnline.Core.Exceptions;
using System.Net;
using BookShopOnline.Core.Resources;
using System.Security.Authentication;

namespace BookShopOnline.Api.Middleware
{
    public class ExceptionMiddleware
    {
        #region Field
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xử lý các yêu cầu HTTP 
        /// </summary>
        /// <param name="context">thông tin của yêu cầu và phản hồi</param>
        /// <returns></returns>
        /// Created by: LQHUY(25/12/2023)
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        /// <summary>
        /// Xử lý các Exception xảy ra
        /// </summary>
        /// <param name="context">thông tin của yêu cầu và phản hồi.</param>
        /// <param name="ex">đối tượng ngoại lệ</param>
        /// <returns>
        /// Status code: 404 và thông tin chi tiết về lỗi xảy ra
        /// Status code: 409 và thông tin chi tiết về lỗi xảy ra
        /// Status code: 500 và thông tin chi tiết về lỗi xảy ra
        /// </returns>
        /// Created by: LQHUY(25/12/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            Console.WriteLine(ex);
            context.Response.ContentType = "apptication/json";
            if (ex is ConnectDbException connectDbException)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(text: new ServiceResult()
                {
                    status = HttpStatusCode.BadRequest,
                    userMsg = ResourceVN.UserMessage,
                    devMsg = ex.Message,
                    errors = connectDbException.errors,
                    traceId = context.TraceIdentifier,
                    moreInfo = ex.HelpLink
                }.ToString() ?? ""
                );
            }

            else if (ex is ValidateException validateException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(text: new ServiceResult()
                {
                    status = HttpStatusCode.BadRequest,
                    userMsg = ResourceVN.UserMessage,
                    devMsg = ex.Message,
                    errors = validateException.errors,
                    traceId = context.TraceIdentifier,
                    moreInfo = ex.HelpLink,
                }.ToString() ?? ""
                );
            }

            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(text: new ServiceResult()
                {
                    status = HttpStatusCode.InternalServerError,
                    userMsg = ResourceVN.UserMessage,
                    devMsg = ex.Message,
                    traceId = context.TraceIdentifier,
                    moreInfo = ex.HelpLink
                }.ToString() ?? ""
                );
            }
            #endregion
        }
    }
}
