import axios from "axios";
import localStorageService from "@/js/storage/LocalStorageService";
import { setInfoTokensToStorage } from "@/js/token/TokenService";
/**
 * Khởi tạo instance axios
 * author : LQHUY(05/12/2023)
 */
const httpRequest = axios.create({
  baseURL: "https://localhost:7015/api/v1/",
  headers: { Accept: "application/json", "Content-Type": "application/json" },
});

//  thêm token JWT
httpRequest.interceptors.request.use(
  async (config) => {
    // Định nghĩa URL làm mới token
    const tokenRenewalUrl = "Users/RenewToken";

    // Bỏ qua logic interceptor cho yêu cầu làm mới token
    if (config.url.includes(tokenRenewalUrl)) {
      return config;
    }

    // Lấy token JWT từ local storage
    const token = localStorageService.getItemFromLocalStorage("jwtToken");
    // Nếu tồn tại token, thêm vào header Authorization
    if (token) {
      // Lấy thời gian hết hạn của token từ local storage
      const expiration =
        localStorageService.getItemFromLocalStorage("expiration");

      // Kiểm tra nếu token đã hết hạn
      if (config.headers.Authorization && new Date() > new Date(expiration)) {
        try {
          // Tạo model token
          const tokenModel = {
            AccessToken: token,
            RefreshToken:
              localStorageService.getItemFromLocalStorage("refreshToken"),
          };

          // Gửi yêu cầu làm mới token bằng refresh token
          const res = await httpRequest.post(tokenRenewalUrl, tokenModel);

          if (res.status === 201) {
            setInfoTokensToStorage(
              res.data.AccessToken,
              res.data.RefreshToken,
              res.data.UserDto
            );
            localStorageService.setItemToLocalStorage(
              "expiration",
              res.data.Expiration
            );

            // Đặt token truy cập mới vào header của yêu cầu
            config.headers["Authorization"] = `Bearer ${res.data.AccessToken}`;
          }
        } catch (error) {
          console.error("Làm mới token thất bại", error);
          // Xử lý lỗi làm mới token (ví dụ: đăng xuất người dùng hoặc hiển thị thông báo lỗi)
        }
      } else {
        // Nếu token chưa hết hạn, thêm nó vào header của yêu cầu
        config.headers["Authorization"] = `Bearer ${token}`;
      }
    }

    return config;
  },
  (error) => {
    // Do something with request error
    return Promise.reject(error);
  }
);

// Thêm interceptor cho phản hồi
httpRequest.interceptors.response.use(
  function (response) {
    return response;
  },
  async function (error) {
    return Promise.reject(error);
  }
);

export default httpRequest;
