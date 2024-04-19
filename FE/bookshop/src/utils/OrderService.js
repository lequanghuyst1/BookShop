import BaseService from "./BaseService";
class OrderService extends BaseService {
  constructor() {
    super("Orders");
  }

  async checkout(data) {
    const res = await this.baseAxios.post(`${this.baseUrl}/Checkout`, data);
    return res;
  }
  async GetByUserId(id) {
    const res = await this.baseAxios.get(`${this.baseUrl}/GetByUserId/${id}`);
    return res;
  }
  async CancelOrder(data) {
    const res = await this.baseAxios.put(`${this.baseUrl}/CancelOrder`,data);
    return res;
  }
  async ConfirmAll(){
    const res = await this.baseAxios.put(`${this.baseUrl}/ConfirmAll`);
    return res;
  }
}
const orderService = new OrderService();
export default orderService;
