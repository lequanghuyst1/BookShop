import BaseService from "./BaseService";
class BookService extends BaseService{
    constructor(){
        super("Books")
    }
}
const bookService = new BookService();
export default bookService;