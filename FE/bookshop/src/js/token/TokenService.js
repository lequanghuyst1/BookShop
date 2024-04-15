import localStorageService from "../storage/LocalStorageService";

/**
 * Kiểm tra jwtToken và refreshToken đã được thêm vào localStorage chưa
 * @returns true - rồi, false - chưa
 * @author LQHUY(06/04/2024)
 */
export const checkInfoTokensInStorage = () => {
  try {
    const jwtToken = localStorage.getItem("jwtToken");
    const refreshToken = localStorage.getItem("refreshToken");
    if (jwtToken && refreshToken) {
      return true;
    } else {
      return false;
    }
  } catch (e) {
    console.log(e);
  }
};

/**
 * Thực hiện thêm mới/sửa các thông tin liên quan tới token
 * @param {string} accessToken
 * @param {string} refreshToken
 * @param {object} userInfo
 * @author LQHUY(06/04/2024)
 */
export const setInfoTokensToStorage = (accessToken, refreshToken, userInfo) => {
  try {
    localStorageService.setItemToLocalStorage("jwtToken", accessToken);
    localStorageService.setItemToLocalStorage("refreshToken", refreshToken);
    localStorageService.setItemToLocalStorage("userInfo", userInfo);
  } catch (error) {
    console.log(error);
  }
};
/**
 * Xóa bỏ mọi thông tin của token ra khỏi local storage
 * @author LQHUY(06/04/2024)
 */
export const removeAllInfoTokenToStorage = () => {
  try {
    localStorageService.removeItemLocalStorage("jwtToken");
    localStorageService.removeItemLocalStorage("refreshToken");
    localStorageService.removeItemLocalStorage("userInfo");
  } catch (error) {
    console.log(error);
  }
};
