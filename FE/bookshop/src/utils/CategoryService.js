import BaseService from "./BaseService";
class CategoryService extends BaseService{
    constructor(){
        super("Categories")
    }
}
const categoryService = new CategoryService();
export default categoryService;