import BaseService from "./BaseService";
class VnPayService extends BaseService {
  constructor() {
    super("VnPays");
  }
  async CreatePaymentUrl(data) {
    var res = await this.baseAxios.post(
      `${this.baseUrl}/CreatePaymentUrl`,
      data
    );
    return res;
  }
  async PaymentCallback(data) {
    const res = await this.baseAxios.post(
      `${this.baseUrl}/PaymentCallback}`,
      data
    );
    return res;
  }
}
const vnPayService = new VnPayService();
export default vnPayService;
