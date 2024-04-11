import BaseService from "./BaseService";
class CartItemService extends BaseService{
    constructor(){
        super("CartItems")
    }
    async getByCartId (id){
        const res = await this.baseAxios.get(`${this.baseUrl}/GetByCartId/${id}`);
        return res;
    }
}
const cartItemService = new CartItemService();
export default cartItemService;