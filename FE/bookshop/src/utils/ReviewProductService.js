import BaseService from "./BaseService";
class ReviewProductService extends BaseService {
  constructor() {
    super("ReviewProducts");
  }
  async getByBookId(bookId) {
    const res = await this.baseAxios.get(`${this.baseUrl}/GetByBookId`, bookId);
    return res;
  }
}
const reviewProductService = new ReviewProductService();
export default reviewProductService;
