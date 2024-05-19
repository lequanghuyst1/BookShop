import BaseService from "./BaseService";
class VoucherService extends BaseService {
  constructor() {
    super("Vouchers");
  }
  async applyVoucher(data) {
    const res = await this.baseAxios.get(`${this.baseUrl}/ApplyVoucher/`, data);
    return res;
  }
}
const voucherService = new VoucherService();
export default voucherService;
