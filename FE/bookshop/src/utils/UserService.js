import BaseService from "./BaseService";
class UserService extends BaseService{
    constructor(){
        super("Users")
    }
    async RegisterUser(data){
        var res = await this.baseAxios.post(`${this.baseUrl}/RegisterUser`,data);
        return res;
    } 
}
const userService = new UserService();
export default userService;