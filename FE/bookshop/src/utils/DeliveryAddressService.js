import BaseService from "./BaseService";
class DeliveryAddress extends BaseService{
    constructor(){
        super("DeliveryAddresss")
    }
    async getAllByUserId (id){
        const res = await this.baseAxios(`${this.baseUrl}/GetByUserId/${id}`);
        return res;
    }
}
const deliveryAddressService = new DeliveryAddress();
export default deliveryAddressService;