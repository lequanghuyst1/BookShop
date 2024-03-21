import BaseService from "./BaseService";
class BoolService extends BaseService{
    constructor(){
        super("Books")
    }
}
const bookService = new BoolService();
export default bookService;