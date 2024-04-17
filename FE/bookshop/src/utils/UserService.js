import BaseService from "./BaseService";
class UserService extends BaseService {
  constructor() {
    super("Users");
  }
  /**
   * Thực hiện đăng ký người dùng
   * @param {object} data
   *  @returns {Promise} Promise chứa kết quả từ việc gửi yêu cầu POST
   * @throws {Error} Nếu có lỗi xảy ra trong quá trình gửi yêu cầu POST
   * @author LQHUY(06/04/2024)
   */
  async RegisterUser(data) {
    var res = await this.baseAxios.post(`${this.baseUrl}/RegisterUser`, data);
    return res;
  }
  /**
   * Thực hiên đăng nhập
   * @param {object} data dữ liệu của bản ghi
   *  @returns {Promise} Promise chứa kết quả từ việc gửi yêu cầu POST
   * @throws {Error} Nếu có lỗi xảy ra trong quá trình gửi yêu cầu POST
   * @author LQHUY(06/04/2024)
   */
  async Login(data) {
    var res = await this.baseAxios.post(`${this.baseUrl}/Login`, data);
    return res;
  }
  /**
   * Thực hiên đăng xuất
   * @param {string} data Email người dùng
   * @returns {Promise} Promise chứa kết quả từ việc gửi yêu cầu POST
   * @throws {Error} Nếu có lỗi xảy ra trong quá trình gửi yêu cầu POST
   * @author LQHUY(06/04/2024)
   */
  async Logout(data) {
    var res = await this.baseAxios.post(`${this.baseUrl}/Logout`, data);
    return res;
  }
}
const userService = new UserService();
export default userService;
