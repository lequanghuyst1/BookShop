import BaseService from "./BaseService";
class DeliveryAddress extends BaseService {
  constructor() {
    super("DeliveryAddresss");
  }
  async getAllByUserId(id) {
    const res = await this.baseAxios.get(`${this.baseUrl}/GetByUserId/${id}`);
    return res;
  }
  async updateAdressDefault(id, data) {
    const res = await this.baseAxios.put(
      `${this.baseUrl}/SetAddresDefault/${id}`,
      data
    );
    return res;
  }
}
const deliveryAddressService = new DeliveryAddress();
export default deliveryAddressService;
