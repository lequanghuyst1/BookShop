import axios from "axios";
import localStorageService from "@/js/storage/LocalStorageService";

/**
 * Khởi tạo instance axiois
 * author : LQHUY(05/12/2023)
 */
const httpRequest = axios.create({
  baseURL: "https://localhost:7015/api/v1/",
  headers: { Accept: "application/json", "Content-Type": "application/json" },
});

//  thêm token JWT
httpRequest.interceptors.request.use(
  (config) => {
    // Lấy token JWT từ local storage
    const token = localStorageService.getItemFromLocalStorage("jwtToken");
    // Nếu tồn tại token, thêm vào header Authorization
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
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
    // const originalRequest = error.config;
    // console.log(error);
    // if (error.response?.status === 401 && checkInfoTokensInStorage() &&!originalRequest._retry ) {
    //   originalRequest._retry = true;
    //   try {
    //     let tokens = {
    //       AccessToken: localStorageService.getItemFromLocalStorage("jwtToken"),
    //       RefreshToken:
    //         localStorageService.getItemFromLocalStorage("refreshToken"),
    //     };
    //     const res = await httpRequest.post("Users/RenewToken", tokens);
    //     if (res.status === 201) {
    //       setInfoTokensToStorage(
    //         res.data.AccessToken,
    //         res.data.RefreshToken,
    //         res.data.UserDto
    //       );
    //     }
    //     const accessToken = res.data.AccessToken;
    //     httpRequest.defaults.headers.common[
    //       "Authorization"
    //     ] = `Bearer ${accessToken}`;
    //     return httpRequest(originalRequest);
    //   } catch (error) {
    //     return Promise.reject(error);
    //   }
    // }
    return Promise.reject(error);
  }
);

export default httpRequest;
