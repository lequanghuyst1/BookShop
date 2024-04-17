import BaseService from "./BaseService";
class BookService extends BaseService {
  constructor() {
    super("Books");
  }
  async getByCategorySlug(slug) {
    const res = await this.baseAxios.get(
      `${this.baseUrl}/GetByCategorySlug/${slug}`
    );
    return res;
  }
  async getFilter(data) {
    const res = await this.baseAxios.post(`${this.baseUrl}/Filter`, data);
    return res;
  }
  async getAuthor(data) {
    const res = await this.baseAxios.get(`${this.baseUrl}/GetAuthor`, data);
    return res;
  }
  async getPublisher(data) {
    const res = await this.baseAxios.get(`${this.baseUrl}/GetPublisher`, data);
    return res;
  }
}
const bookService = new BookService();
export default bookService;
