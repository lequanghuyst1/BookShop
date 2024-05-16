using BookShopOnline.Core.Interfaces.Infrastructures;

namespace BookShopOnline.Api.Middleware
{
    public class JWTValidationMiddleware
    {

        #region Field
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        public JWTValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion
        public async Task InvokeAsync(HttpContext httpContext, ICacheRepository cacheRepository)
        {
            // Lấy giá trị của tiêu đề "Authorization" từ yêu cầu HTTP
            string authorizationHeader = httpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer"))
            {
                string jwtToken = authorizationHeader.Substring(7);
                Console.WriteLine(jwtToken);
                // Kiểm tra chuỗi JWT hết hạn trong cache chưa
                if (cacheRepository.IsCacheExist(jwtToken) == false)
                {
                    // Trả về lỗi 401  Unauthorized nếu chuỗi JWT không hợp lệ
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }

            }
            // CHuyến tiếp yêu cầu cho middleware tiếp theo
            await _next(httpContext);
        }
    }
}
