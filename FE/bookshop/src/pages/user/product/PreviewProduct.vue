<template>
  <div class="m-dialog" id="write-review">
    <div class="m-dialog__overlay"></div>
    <div class="m-dialog__container" style="width: 40%">
      <div class="m-dialog__header">
        <h3 class="m-dialog__header-title">Đánh giá & nhận xét</h3>

        <div class="m-dialog__header-action">
          <div
            v-tippy="{
              content: 'Thoát',
              placement: 'bottom',
            }"
            class="m-dialog__header-close"
            @click="this.$emit('onCloseForm')"
          >
            <i class="fa-solid fa-xmark"></i>
          </div>
        </div>
      </div>
      <div class="m-dialog__content" style="margin: 12px 0 0 0">
        <form action="" class="w-100">
          <div class="tilte-preview-star">Đánh giá chung</div>
          <div class="modal-review-star">
            <div class="star-all">
              <div
                class="modal__button"
                v-for="(item, index) in starItems"
                :key="index"
              >
                <div
                  class="icon"
                  :class="{ 'is-active': index <= previewProduct.Rating }"
                  @click="rateProduct(index)"
                >
                  <i class="fa-solid fa-star"></i>
                </div>
                <p>{{ item }}</p>
              </div>
            </div>
            <div class="tilte-preview-star mb-4">Viết đánh giá</div>
            <m-textarea
              :rows="4"
              placeholder="Xin mời chia sẻ một số cảm nhận về sản phẩm"
              v-model="previewProduct.Content"
            ></m-textarea>
          </div>
        </form>
      </div>
      <div class="m-dialog__footer">
        <div @click="this.$emit('onCloseForm')" class="m-dialog__footer-left">
          <MButton class="m-button--sub" text="Hủy"></MButton>
        </div>
        <div class="m-dialog__footer-right">
          <MButton
            @click="hanldeSave"
            style="background-color: #c92127"
            text="Gửi nhận xét"
          ></MButton>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import MTextarea from "@/components/base/textarea/MTextarea.vue";
import reviewProductService from "@/utils/ReviewProductService";
export default {
  components: { MTextarea },
  props: {
    bookId: {
      type: String,
      required: true,
    },
    userId: {
      type: String,
      required: true,
    },
    reviewProductIdEdit: {
      type: String,
      required: true,
    },
  },
  emits: ["onCloseForm", "loadData"],
  created() {
    this.handleCheckModeForm();
  },
  data() {
    return {
      starItems: ["Rất tệ", "Tệ", "Bình thường", "Tốt", "Tuyệt vời"],
      previewProduct: {
        Rating: 4,
      },
    };
  },
  computed: {
    formMode: function () {
      if (this.$props.reviewProductIdEdit) {
        return this.$Enum.FormMode.Edit;
      } else {
        return this.$Enum.FormMode.Add;
      }
    },
  },
  methods: {
    rateProduct(rating) {
      this.previewProduct.Rating = rating;
    },
    handleCheckModeForm() {
      if (this.formMode === this.$Enum.FormMode.Edit) {
        this.getReviewProductDetail();
      }
    },

    /**
     * Thực hiện save khi click btn Gửi nhận xét
     * @author LQHUY(18/04/2024)
     */
    hanldeSave() {
      if (this.formMode === this.$Enum.FormMode.Edit) {
        this.hanldeOnEdit();
      } else {
        this.handleOnAdd();
      }
    },

    /**
     * Thực hiện thêm mới đánh giá và nhận xét
     * @author LQHUY(18/04/2024)
     */
    async handleOnAdd() {
      try {
        this.previewProduct.UserId = this.$props.userId;
        this.previewProduct.BookId = this.$props.bookId;
        this.previewProduct.Rating += 1;
        const formData = new FormData();
        formData.append("dataJson", JSON.stringify(this.previewProduct));
        const res = await reviewProductService.post(formData);
        if (res.status === 201) {
          this.$emit("onCloseForm");
          this.$emit("loadData");
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện chỉnh sửa đánh giá và nhận xét
     * @author LQHUY(18/04/2024)
     */
    async hanldeOnEdit() {
      try {
        const formData = new FormData();
        this.previewProduct.Rating += 1;
        formData.append("dataJson", JSON.stringify(this.previewProduct));
        const res = await reviewProductService.put(
          this.$props.reviewProductIdEdit,
          formData
        );
        if (res.status === 200) {
          this.$emit("onCloseForm");
          this.$emit("loadData");
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện lấy thông tin chi tiết đánh giá
     * @author LQHUY(18/04/2024)
     */
    async getReviewProductDetail() {
      try {
        const res = await reviewProductService.getById(
          this.$props.reviewProductIdEdit
        );
        if (res.status === 200) {
          this.previewProduct = res.data;
          this.previewProduct.Rating -= 1;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style scoped>
.star-all {
  display: flex;
  justify-content: space-around;
  margin: 20px 0;
}
.star-all .modal__button {
  text-align: center;
}
.star-all .modal__button .icon {
  color: rgba(145, 158, 171, 0.522);
  font-size: 20px;
}
.star-all .modal__button p {
  font-size: 14px;
  font-weight: 400;
  color: #4a4a4a;
}
.tilte-preview-star {
  font-size: 16px;
  color: #333;
  font-weight: 600;
}
.is-active {
  color: #ffbf00 !important;
}
</style>
