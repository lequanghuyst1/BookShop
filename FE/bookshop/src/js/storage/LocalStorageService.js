class LocalstorageService {
    /**
     * Thực hiện lấy ra giá trị lưu trữ local theo tên
     * @param key tên storage
     * @return {undefined|any}
     * @author LQHUY (06/04/2024)
     */
    getItemFromLocalStorage(key) {
      try {
        const serializedData = localStorage.getItem(key);
        if (serializedData == null) {
          return undefined;
        }
        return JSON.parse(serializedData);
      } catch (error) {
        console.log("Error getting data from Local Storage:", error);
        return undefined;
      }
    }
  
    /**
     * Thực hiện tạo mới/ sửa 1 giá trị storage
     * @param {String} key Tên Lưu trữ
     * @param {JSON} value Giá trị lưu trữ
     * @author LQHUY (06/04/2024)
     */
    setItemToLocalStorage(key, value) {
      try {
        const serializeData = JSON.stringify(value);
        localStorage.setItem(key, serializeData);
      } catch (error) {
        console.log("Error set data to Local Storage:", error);
      }
    }
  
    /**
     * Thực hiện xóa 1 lưu trữ trên local storage theo tên
     * @param {String} key tên của giá trị lưu trữ
     * @author LQHUY (06/04/2024)
     */
    removeItemLocalStorage(key) {
      try {
        localStorage.removeItem(key);
      } catch (e) {
        console.log("Error remove data from Local Storage:", e);
      }
    }
  }
  const localStorageService = new LocalstorageService();
  export default localStorageService;
  