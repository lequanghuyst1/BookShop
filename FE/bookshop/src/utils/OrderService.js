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
    console.log(data);
    const res = await this.baseAxios.get(
      `${this.baseUrl}/Chart/CalculateTotalAmountByTypeOfTime`,
      data
    );
    return res;
  }
  async getRevenueByProduct(data) {
    const res = await this.baseAxios.get(
      `${this.baseUrl}/Chart/GetRevenueByProduct`,
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

  async exportRecord(ids) {
    const res = await this.baseAxios.post(`${this.baseUrl}/Export`, ids, {
      responseType: "blob",
      headers: {
        "Content-Type": "application/json",
      },
    });
    return res;
  }
  
  async exportRevenueByTime(data) {
    const res = await this.baseAxios.post(
      `${this.baseUrl}/Export/ExportRevenueByTime`,
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
  async exportRevenueByProduct(data) {
    const res = await this.baseAxios.post(
      `${this.baseUrl}/Export/ExportRevenueByProduct`,
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
  async filter(data) {
    const res = await this.baseAxios.post(`${this.baseUrl}/Filter`, data);
    return res;
  }
}
const orderService = new OrderService();
export default orderService;
