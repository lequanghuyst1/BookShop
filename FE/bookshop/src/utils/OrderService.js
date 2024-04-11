import BaseService from "./BaseService";
class OrderService extends BaseService {
  constructor() {
    super("Orders");
  }

  async checkout(data) {
    const res = await this.baseAxios.post(`${this.baseUrl}/Checkout`, data);
    return res;
  }
}
const orderService = new OrderService();
export default orderService;
