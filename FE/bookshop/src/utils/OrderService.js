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
    const res = await this.baseAxios.put(`${this.baseUrl}/CancelOrder`, data);
    return res;
  }
  async ConfirmAll() {
    const res = await this.baseAxios.put(`${this.baseUrl}/ConfirmAll`);
    return res;
  }
  async CalculateTotalSalesPerMonth(data) {
    const res = await this.baseAxios.get(
      `${this.baseUrl}/CalculateTotalSalesPerMonth`,
      data
    );
    return res;
  }
  async getTotalOrderByConditionIn24Hour(data) {
    const res = await this.baseAxios.get(
      `${this.baseUrl}/TotalOrderBy24Hour`,
      data
    );
    return res;
  }
  async calculateTotalAmountByTypeOfTime(data) {
    const res = await this.baseAxios.get(
      `${this.baseUrl}/Chart/CalculateTotalAmountByTypeOfTime`,
      data
    );
    return res;
  }
  async getTotalRevenvue() {
    const res = await this.baseAxios.get(
      `${this.baseUrl}/Chart/GetTotalRevenvue`
    );
    return res;
  }

  async getOrderByTypeOfTime(data) {
    const res = await this.baseAxios.get(
      `${this.baseUrl}/GetByTypeOfTime`,
      data
    );
    return res;
  }

  async exportRevenue(data) {
    const res = await this.baseAxios.post(
      `${this.baseUrl}/Export/Revenue`,
      data,
      {
        responseType: "blob",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    return res;
  }
}
const orderService = new OrderService();
export default orderService;
