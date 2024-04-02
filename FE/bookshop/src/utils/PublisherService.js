import BaseService from "./BaseService";
class PublisherService extends BaseService{
    constructor(){
        super("Publishers")
    }
}
const publisherService = new PublisherService();
export default publisherService;