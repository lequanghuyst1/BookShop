<template>
  <div class="main-body-order-detail">
    <div class="order-view-content-info">
      <div class="order-view-title">Chi tiết đơn hàng</div>

      <div>
        <div
          class="order-view-status"
          :style="{
            color:
              this.order.OrderStatus ===
              this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
                ? '#DEB85B'
                : this.order.OrderStatus === this.$Enum.ORDER_STATUS.CANCELLED
                ? '#a90000'
                : '#a0dbb3',
            borderColor:
              this.order.OrderStatus ===
              this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
                ? '#DEB85B'
                : this.order.OrderStatus === this.$Enum.ORDER_STATUS.CANCELLED
                ? '#a90000'
                : '#a0dbb3',
            backgroundColor:
              this.order.OrderStatus ===
              this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
                ? '#fdfde8'
                : this.order.OrderStatus === this.$Enum.ORDER_STATUS.CANCELLED
                ? '#f3b4af'
                : '#f1fcf5',
          }"
        >
          {{ "Đơn hàng " + orderStatusString(order.OrderStatus) }}
        </div>

        <div class="order-view-id">
          <span>Mã đơn hàng: </span><span>{{ order.OrderCode }}</span>
        </div>
        <div class="order-view-date">
          <span>Ngày mua: </span
          ><span>{{ this.$helper.formatDate(order.OrderDate) }}</span>
        </div>
        <div class="order-view-total">
          <span>Tổng Tiền: </span
          ><span
            ><span style="font-size: 14px !important" class="price">{{
              this.$helper.formatMoney(order.TotalAmount)
            }}</span
            ><span style="font-size: 14px !important" class="sym-totals"
              >đ</span
            ></span
          >
        </div>
        <div class="order-view-vat">
          <span style="flex: 1">Thông tin xuất hóa đơn GTGT: </span>
          <span class="dont-have-info" style="flex: 5; font-weight: 500"
            >(Không có)</span
          >
        </div>
        <div class="order-view-note">
          <span style="flex: 1">Ghi chú: </span>
          <span class="dont-have-info" style="flex: 16; font-weight: 500">{{
            order.Note ? order.Note : "(Không có)"
          }}</span>
        </div>
      </div>

      <div style="text-align: center" class="order-view-buttons-color">
        <!--<input type="button" class="order-view-buy-again" value="Mua Lai">-->
        <!--<span class="back-link"><a href=""><small>&laquo; </small></a></span>-->
        <!--<a href="" class="link-rss f-none"></a>-->
        <div
          style="cursor: pointer"
          @click="handleReOder"
          class="order-view-buttons-color-child"
        >
          <a class="link-reorder order-view-buy-again-btn">Đặt hàng lại</a>
        </div>
        <div
          style="cursor: pointer"
          @click="handleCancelOrder"
          v-show="
            order.OrderStatus === this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
          "
          class="button-cancel-order order-view-buttons-color-child"
        >
          <a class="link-reorder order-view-buy-again-btn">Hủy đơn hàng</a>
        </div>
      </div>
      <div id="cancel-order-cover"></div>
      <div id="popup-cancel-order">
        <!--    <button id = "coupon-message-close">X</button>-->
        <div class="row">
          <div class="col-12 popup-cancel-order popup-cancel-order-title">
            Lý do huỷ đơn hàng
          </div>
        </div>
        <div
          class="col-12 form-group popup-cancel-order popup-cancel-order-content"
        >
          <div class="form-group">
            <form action="" method="POST">
              <select
                id="reason"
                name="reason_cancel_order"
                class="form-control"
              >
                <option value="0">----- Chọn lý do muốn huỷ đơn hàng</option>
                <option value="1">Không còn nhu cầu mua hàng</option>
                <option value="2">Đặt nhầm/trùng</option>
                <option value="3">Thêm/bớt sản phẩm</option>
                <option value="4">Quên nhập mã giảm giá</option>
                <option value="5">Không áp dụng được mã giảm giá</option>
                <option value="6">
                  Đơn hàng bị tách ra quá nhiều lần giao
                </option>
                <option value="7">Thời gian giao hàng quá chậm</option>
                <option value="8">Thay đổi địa chỉ nhận hàng</option>
                <option value="9">Khác</option>
              </select>
            </form>
          </div>

          <div id="other-cancel-reason" style="text-align: left">
            <p>Nhập vào lý do khác</p>
            <textarea
              id="other-reason-input"
              cols="5"
              rows="2"
              name="other-reason"
              maxlength="100"
            ></textarea>
            <h6 class="pull-right" id="count-message">0 ký tự</h6>
          </div>
        </div>

        <div
          class="col-sm-12 col-md-12 col-sms-12 text-result-cancel"
          style="color: red; display: none"
        >
          Lỗi xảy ra. Vui lòng thử lại.
        </div>
        <div
          class="col-sm-12 col-md-12 col-sms-12 confirm-cancel-order"
          style="padding: 20px"
        >
          <button
            id="cancel"
            type="button"
            onclick="closeCancelOrderBox()"
            class="cancel-order-confirm cancel"
          >
            <span> Đóng lại </span>
          </button>
          <button
            id="confirm"
            type="submit"
            onclick="reasoncancel()"
            class="cancel-order-confirm"
            disabled="true"
          >
            <span> Đồng ý </span>
          </button>
        </div>
      </div>

      <div id="cancel-order-cover"></div>

      <div id="popup-review-order">
        <div class="popup-review-order-header">Ý KIẾN KHÁCH HÀNG</div>
        <div class="popup-review-order-body">
          <div id="order-info" class="row" style="padding: 0 20px 20px">
            <div class="col-6" style="text-align: left">
              <label style="font-weight: bold">Đơn Hàng</label>
              <input
                type="text"
                class="form-control review-order-textbox"
                id="order_id"
                name="order_id"
                readonly=""
                value="103431888"
              />
            </div>
            <div class="col-6" style="text-align: left; padding-left: 10px">
              <label style="font-weight: bold">Email</label>
              <input
                type="email"
                class="form-control review-order-textbox"
                id="email"
                readonly=""
                name="email"
                value=""
              />
            </div>
          </div>
          <div
            id="ykienkhachhang"
            class="col-lg-12 col-md-12 col-sm-12 col-xs-12 popup-review-order-content"
          >
            <form
              class="form-horizontal"
              id="ykienkhachhang_form"
              method="post"
              action="https://www.fahasa.com/ykienkhachhang/index/post/"
              enctype="multipart/form-data"
            >
              <!-- <div class="row err_message">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                  <i class="fa fa-exclamation" aria-hidden="true"></i>
                  <span></span>
                </div>
              </div> -->
              <div class="phan1">
                <div
                  class="row"
                  style="padding-left: 30px; padding-right: 10px"
                >
                  <div class="row">
                    <div class="col-4">
                      <div class="row">
                        <div class="col-3">
                          <center>
                            <img
                              src="https://cdn0.fahasa.com/skin/frontend/ma_vanese/fahasa/images/ykienkhachhang/Book-icon.png"
                              style="width: 40px"
                            />
                          </center>
                        </div>
                        <div class="col-9 review-order-body-icon-text">
                          <center>CHẤT LƯỢNG SẢN PHẨM</center>
                        </div>
                      </div>
                    </div>
                    <div class="col-8">
                      <div class="container" style="text-align: left">
                        <p><b>1. Chất Lượng Sản Phẩm</b></p>

                        <select
                          id="product"
                          class="form-control review-order-combo"
                        >
                          <option value="0">
                            Đánh giá chất lượng sản phẩm
                          </option>
                          <option value="4">Tốt</option>
                          <option value="1">Rất kém</option>
                          <option value="3">Trung bình</option>
                          <option value="2">Kém</option>
                          <option value="5">Rất tốt</option>
                        </select>

                        <!-- <div>
                          <div class="sanpham1-note input-note">
                            <label for="sanpham1-note"
                              >Fahasa.com chân thành xin lỗi vì những trải
                              nghiệm không tốt của quý khách. Quý khách vui lòng
                              chia sẻ những trải nghiệm không tốt về chất lượng
                              sản phẩm trong đơn hàng vừa qua, để Fahasa.com có
                              thể cải thiện chất lượng sản phẩm lần sau.</label
                            >
                            <textarea
                              cols="4"
                              id="sanpham1-note"
                              name="sanpham1-note"
                              class="form-control"
                            ></textarea>
                          </div>
                        </div> -->
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </form>
            <div class="phan2">
              <div class="row" style="padding-left: 30px; padding-right: 10px">
                <div class="row flex-center">
                  <div class="col-4">
                    <div class="row">
                      <div class="col-3">
                        <center>
                          <img
                            src="https://cdn0.fahasa.com/skin/frontend/ma_vanese/fahasa/images/ykienkhachhang/Shipping-icon.png"
                            style="width: 40px"
                          />
                        </center>
                      </div>
                      <div class="col-9 review-order-body-icon-text">
                        <center>CHẤT LƯỢNG GIAO HÀNG</center>
                      </div>
                    </div>
                  </div>
                  <div class="col-8">
                    <div class="">
                      <div class="container" style="text-align: left">
                        <p><b>1. Thời Gian Giao Hàng</b></p>
                        <form action="" method="POST">
                          <select
                            id="delivery-time"
                            class="form-control review-order-combo"
                          >
                            <option value="0">
                              Đánh giá thời gian giao hàng
                            </option>
                            <option value="2">Chậm</option>
                            <option value="4">Nhanh</option>
                            <option value="1">Rất Chậm</option>
                            <option value="3">Bình Thường</option>
                            <option value="5">Rất nhanh</option>
                          </select>
                        </form>

                        <!-- <div>
                          <div class="giaohang1-note input-note">
                            <textarea
                              id="giaohang1-note"
                              name="giaohang1-note"
                              class="form-control"
                              rows="2"
                            ></textarea>
                          </div>
                        </div> -->

                        <p><b>2. Thái Độ Nhân Viên Giao Hàng</b></p>
                        <form action="" method="POST">
                          <select
                            id="delivery-men"
                            class="form-control review-order-combo"
                          >
                            <option value="0">
                              Đánh giá thái độ nhân viên giao hàng
                            </option>
                            <option value="3">Vui vẻ, dễ chịu</option>
                            <option value="2">Bình Thường</option>
                            <option value="1">Khó chịu, cáu gắt</option>
                          </select>
                        </form>

                        <!-- <div>
                          <div class="giaohang2-note input-note">
                            <textarea
                              id="giaohang2-note"
                              name="giaohang2-note"
                              class="form-control"
                              rows="2"
                            ></textarea>
                          </div>
                        </div> -->
                        <p><b>3. Nhân Viên Giao Hàng Liên Hệ Trước?</b></p>
                        <form action="" method="POST">
                          <select
                            id="delivery-contact"
                            name="reason_cancel_order"
                            class="form-control review-order-combo"
                          >
                            <option value="2">Có</option>
                            <option value="1">Không</option>
                          </select>
                        </form>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="phan3">
              <div class="row" style="padding-left: 30px; padding-right: 10px">
                <div class="row flex-center">
                  <div class="col-4">
                    <div class="row">
                      <div class="col-3">
                        <center>
                          <img
                            src="https://cdn0.fahasa.com/skin/frontend/ma_vanese/fahasa/images/ykienkhachhang/Call-icon.png"
                            style="width: 40px"
                          />
                        </center>
                      </div>
                      <div class="col-9 review-order-body-icon-text">
                        <center>CHĂM SÓC KHÁCH HÀNG</center>
                      </div>
                    </div>
                  </div>
                  <div class="col-8">
                    <div class="container" style="text-align: left">
                      <p><b>1. Chất Lượng Chăm Sóc Khách Hàng FAHASA.COM</b></p>
                      <form action="" method="POST">
                        <select
                          id="customer-service"
                          class="form-control review-order-combo"
                        >
                          <option value="-1">
                            Đánh giá chất lượng chăm sóc khách hàng Fahasa.com
                          </option>
                          <option value="1">Không hài lòng</option>
                          <option value="3">Rất hài lòng</option>
                          <option value="0">
                            Tôi không liên lạc với tổng đài Fahasa.com
                          </option>
                          <option value="2">Hài lòng</option>
                        </select>
                      </form>

                      <!-- <div>
                        <div class="cskh1-note input-note">
                          <label for="cskh1-note"
                            >Fahasa.com chân thành xin lỗi vì những trải nghiệm
                            không tốt của quý khách. Quý khách vui lòng đóng góp
                            ý kiến để Fahasa.com hoàn thiện hơn.</label
                          >
                          <textarea
                            id="cskh1-note"
                            name="cskh1-note"
                            class="form-control"
                          ></textarea>
                        </div>
                      </div> -->
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="clear"></div>
          <div class="popup-review-order-footer btn-submit">
            <button
              id="cancel"
              type="button"
              onclick="closeReviewOrderBox()"
              class="review-order-confirm cancel"
            >
              <span> Đóng lại </span>
            </button>
            <button
              id="singlebutton"
              type="submit"
              onclick="revieworder()"
              name="singlebutton"
              class="review-order-confirm confirm"
            >
              <span> Đánh Giá </span>
            </button>
          </div>
        </div>
      </div>

      <!-- <div id="noti-success">
        <div class="noti-success icon">
          <i class="fa fa-thumbs-o-up fa-8x" aria-hidden="true"></i>
        </div>
        <div class="noti-success top">
          Cảm ơn bạn đã gửi nhận xét cho đơn hàng #103431888
        </div>
        <button
          id="cancel"
          type="button"
          onclick="closeNotiBox()"
          class="noti-success cancel"
        >
          <span> Đóng lại </span>
        </button>
      </div> -->
    </div>
    <div class="order-view-content-details">
      <div class="order-view-content-box1">
        <div class="order-view-box">
          <div class="order-box-title">
            <div class="order-view-title">Thông tin người nhận</div>
          </div>
          <div class="order-box-info">
            <address>
              {{ order.Fullname }}
              <br />

              {{ order.Address }}
              <br />

              Tel: {{ order.PhoneNumber }}
            </address>
          </div>
        </div>
        <div class="order-view-box">
          <div class="order-info-shipping-description">
            <div class="order-box-title">
              <div class="order-view-title">Phương thức vận chuyển</div>
            </div>
            <div class="order-box-info">Giao hàng tận nơi</div>
          </div>
        </div>
        <div class="order-view-box">
          <div class="order-box-title">
            <div class="order-view-title">Phương thức thanh toán</div>
          </div>

          <div class="order-box-info">
            <div class="order-box-info-historypayment">
              <div>
                <p>
                  {{
                    this.$helper.hanldeValueTypeEnum(
                      "PAYMENT_METHOD",
                      order.PaymentMethod
                    )
                  }}
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="border-block-mobile"></div>
      <div class="order-view-status-container">
        <div class="order-view-status-new-order">
          <div class="order-view-icon-container">
            <div style="height: 60px; width: 60px; align-self: center">
              <div
                class="order-view-icon-img"
                style="
                  background: url(https://cdn0.fahasa.com/skin/frontend/ma_vanese/fahasa/images/order/ico_donhangmoi_red.svg)
                    no-repeat center;
                  border-color: #fa0001;
                "
              ></div>
            </div>
            <div class="order-view-icon-content">
              <p>Đơn hàng mới</p>
              <p>{{ this.$helper.formatOrderDate(order.OrderDate) }}</p>
            </div>
          </div>
          <div
            class="order-view-progress-bar"
            style="background: #fa0001"
          ></div>
        </div>
        <div class="order-view-status-new-order">
          <div class="order-view-icon-container">
            <div style="height: 60px; width: 60px; align-self: center">
              <div
                class="order-view-icon-img"
                style="
                  background: url(https://cdn0.fahasa.com/skin/frontend/ma_vanese/fahasa/images/order/ico_dangxuly_red.svg)
                    no-repeat center;
                  border-color: #fa0001;
                "
              ></div>
            </div>
            <div class="order-view-icon-content">
              <p>Đang xử lý</p>
              <p></p>
            </div>
          </div>
          <div
            class="order-view-progress-bar"
            style="background: #fa0001"
          ></div>
        </div>
        <div class="order-view-status-new-order">
          <div class="order-view-icon-container">
            <div style="height: 60px; width: 60px; align-self: center">
              <div
                class="order-view-icon-img"
                style="
                  background: url(https://cdn0.fahasa.com/skin/frontend/ma_vanese/fahasa/images/order/ico_huy_red.svg)
                    no-repeat center;
                  border-color: #fa0001;
                "
              ></div>
            </div>
            <div class="order-view-icon-content">
              <p>{{ orderStatusString(order.OrderStatus) }}</p>
              <p>10/04/2024 - 13:26</p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="order-subOrder-container">
      <div class="order-subOrder-items">
        <div class="order-subOrder-info-status">
          <div
            class="order-subOrder-info"
            id="103431888"
            onclick="showOnProductDetails(this.id);"
          >
            <div>
              <span>Đơn hàng:</span><span>{{ order.OrderCode }}</span>
            </div>

            <div
              class="order-view-status"
              :style="{
                color:
                  this.order.OrderStatus ===
                  this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
                    ? '#DEB85B'
                    : this.order.OrderStatus ===
                      this.$Enum.ORDER_STATUS.CANCELLED
                    ? '#a90000'
                    : '#a0dbb3',
                borderColor:
                  this.order.OrderStatus ===
                  this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
                    ? '#DEB85B'
                    : this.order.OrderStatus ===
                      this.$Enum.ORDER_STATUS.CANCELLED
                    ? '#a90000'
                    : '#a0dbb3',
                backgroundColor:
                  this.order.OrderStatus ===
                  this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION
                    ? '#fdfde8'
                    : this.order.OrderStatus ===
                      this.$Enum.ORDER_STATUS.CANCELLED
                    ? '#f3b4af'
                    : '#f1fcf5',
              }"
            >
              {{ "Đơn hàng " + orderStatusString(order.OrderStatus) }}
            </div>
            <div>
              <span>Tổng tiền:</span
              ><span
                ><span class="price"
                  >{{ this.$helper.formatMoney(order.TotalAmount) }}đ</span
                ></span
              >
            </div>
            <div class="order-subOrder-quantity">
              <span>Số lượng:</span><span>{{ this.totalQuantity }}</span>
            </div>
            <div class="order-subOrder-arrow">
              <i class="fa fa-chevron-right" aria-hidden="true"></i>
            </div>
          </div>
        </div>
      </div>
      <div class="order-subOrder-products id-list-products-103431888">
        <div class="table-subOrder-container">
          <div class="table-subOrder-header-and-img">
            <div class="table-subOrder-title-product order-view-title">
              Sản phẩm
            </div>
            <div class="table-subOrder-row table-subOrder-header">
              <div class="table-subOrder-cell" style="width: 120px">
                Hình ảnh
              </div>
              <div class="table-subOrder-cell">Tên sản phẩm</div>
              <div class="table-subOrder-cell">Sku</div>
              <div class="table-subOrder-cell">Giá bán</div>
              <div class="table-subOrder-cell">SL</div>
              <div class="table-subOrder-cell">Thành tiền</div>
            </div>
          </div>
          <!-- Check if Buffet Combo Event is Active -->
          <div class="table-subOrder-parent-img-and-cell">
            <div
              v-for="orderDetail in orderDetails"
              :key="orderDetail.OrderDetailId"
              class="table-subOrder-row"
            >
              <div
                style="width: 120px; text-align: left"
                class="table-subOrder-cell table-subOrder-img-web"
              >
                <img :src="orderDetail.ImagePath" />
              </div>
              <div class="table-subOrder-cell table-subOrder-name-product">
                <div class="table-subOrder-name-tag-a">
                  <a
                    href="https://www.fahasa.com/montessori-phuong-phap-giao-duc-toan-dien-cho-tre-0-6-tuoi.html"
                    style="height: auto"
                  >
                    {{ orderDetail.BookName }}
                  </a>
                </div>
              </div>
              <div class="table-subOrder-cell table-subOrder-sku">
                8936066688703
              </div>
              <div class="table-subOrder-cell">
                <span class="table-subOrder-hidden-desktop"
                  >Giá bán:&nbsp;</span
                >
                <span>
                  <!--<span class="price-incl-tax">-->
                  <span class="cart-price">
                    <div class="cart-orderHs-price">
                      <div>
                        <span class="price">{{
                          this.$helper.formatMoney(orderDetail.Price)
                        }}</span>
                        đ
                      </div>
                      <div>
                        <span class="orderHs-price-old"
                          ><span class="price">{{
                            this.$helper.formatMoney(orderDetail.OriginalPrice)
                          }}</span>
                          đ</span
                        >
                      </div>
                    </div>
                    <!--</span>-->
                  </span>
                </span>
              </div>
              <div class="table-subOrder-cell">
                <span class="table-subOrder-hidden-desktop"
                  >{{ orderDetail.Quantity }}:&nbsp;</span
                >
                <span>
                  <strong>{{ orderDetail.Quantity }}</strong
                  ><br />
                </span>
              </div>
              <div class="table-subOrder-cell">
                <span class="table-subOrder-hidden-desktop"
                  >Thành tiền:&nbsp;</span
                >
                <span>
                  <!--<span class="price-incl-tax">-->
                  <span class="cart-price">
                    <span class="price">{{
                      this.$helper.formatMoney(
                        orderDetail.Quantity * orderDetail.Price
                      )
                    }}</span>
                    đ
                    <!--</span>-->
                  </span>
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="order-subOrder-total id-total-103431888">
        <div class="order-subOrder-total-mobile">
          <div>
            <span>Thành tiền: </span>
            <span class="order-totals-price"
              ><span class="price">{{
                this.$helper.formatMoney(order.TotalAmount)
              }}</span
              >&nbsp;<span class="sym-totals">đ</span></span
            >
          </div>

          <div>
            <span>Phí vận chuyển: </span>
            <span class="order-totals-price"
              ><span class="price">{{
                this.$helper.formatMoney(order.ShippingFee)
              }}</span
              >&nbsp;<span class="sym-totals">đ</span></span
            >
          </div>

          <div>
            <span>Tổng Số Tiền (gồm VAT): </span>
            <span class="order-totals-price"
              ><span class="price">105.870</span>&nbsp;<span class="sym-totals"
                >đ</span
              ></span
            >
          </div>
        </div>
        <div class="order-subOrder-total-desktop">
          <div>
            <p><span>Thành tiền: </span></p>

            <p><span>Phí vận chuyển: </span></p>
            <p><span>Tổng Số Tiền (gồm VAT): </span></p>
          </div>
          <div>
            <p class="order-totals-price">
              <span class="price">{{
                this.$helper.formatMoney(order.TotalAmount)
              }}</span
              >&nbsp;<span class="sym-totals">đ</span>
            </p>

            <p class="order-totals-price">
              <span class="price">19.000</span>&nbsp;<span class="sym-totals"
                >đ</span
              >
            </p>
            <p class="order-totals-price">
              <span class="price">{{
                this.$helper.formatMoney(order.TotalAmount)
              }}</span
              >&nbsp;<span class="sym-totals">đ</span>
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import cartLocalStorageService from "@/js/storage/CartLocalStorage";
import localStorageService from "@/js/storage/LocalStorageService";
import cartItemService from "@/utils/CartItemService";
import orderService from "@/utils/OrderService";
export default {
  name: "OrderDetail",
  created() {
    this.getOrderDetailsData();
    this.getOrderData();
  },
  computed: {
    resource: function () {
      return this.$Resource[this.$languageCode];
    },
    userInfo: function () {
      return localStorageService.getItemFromLocalStorage("userInfo");
    },
  },
  data() {
    return {
      //Lưu danh sách chi tiết từng đơn hàng
      orderDetails: [],
      //Lưu thông tin chi tiết đơn hàng
      order: [],
      //Lưu tổng số lượng của đơn hàng
      totalQuantity: 0,
      orderIdReOrder: [],
    };
  },
  methods: {
    /**
     * Hàm thực hiện gán giá trị cho orderDetails
     * @author LQHUY(12/04/2024)
     */
    async getOrderDetailsData() {
      try {
        const res = await this.$httpRequest.get(
          `OrderDetails/GetByOrderId/${this.$route.params.id}`
        );
        if (res.status === 200) {
          this.orderDetails = res.data;
          this.totalQuantity = this.orderDetails.reduce(
            (previousValue, item) => previousValue + item.Quantity,
            0
          );
        }
      } catch (error) {
        console.log(error);
      }
    },

    async getOrderData() {
      try {
        const res = await orderService.getById(this.$route.params.id);
        if (res.status === 200) {
          this.order = res.data;
          this.orderReplica = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện hủy đơn hàng khi click btn hủy đơn hàng
     * @author LQHUY(13/04/2024)
     */
    async handleCancelOrder() {
      try {
        this.$emitter.emit("toggleShowLoading", true);
        const res = await orderService.CancelOrder(this.order);
        if (res.status === 200) {
          this.$emitter.emit(
            "onShowToastMessage",
            this.$Resource[this.$languageCode].ToastMessage.Type.Success,
            "Đơn hàng đã được hủy.",
            this.$Resource[this.$languageCode].ToastMessage.Status.Success
          );
          this.$emitter.emit("toggleShowLoading", false);
          //load lại dữ liệu đơn hàng
          await this.getOrderData();
        }
      } catch (error) {
        this.$emitter.emit("toggleShowLoading", false);
        console.log(error);
      }
    },
    orderStatusString(orderStatus) {
      switch (orderStatus) {
        case this.$Enum.ORDER_STATUS.WAIT_FOR_CONFIRMATION:
          return this.resource.ENUM_ORDER_STATUS.waitForConfirmation;
        case this.$Enum.ORDER_STATUS.CONFIRMED:
          return this.resource.ENUM_ORDER_STATUS.confirmed;
        case this.$Enum.ORDER_STATUS.SHIPPING:
          return this.resource.ENUM_ORDER_STATUS.shipping;
        case this.$Enum.ORDER_STATUS.DELIVERED:
          return this.resource.ENUM_ORDER_STATUS.delivered;
        case this.$Enum.ORDER_STATUS.CANCELLED:
          return this.resource.ENUM_ORDER_STATUS.cancelled;
        default:
          return "";
      }
    },

    /**
     * Thực hiện đặt lại hàng khi nhấn btn đặt lại hàng
     * @author LQHUY(14/04/2024)
     */
    handleReOder() {
      this.orderDetails.forEach(async (item) => {
        const formData = new FormData();
        item.CartId = this.userInfo.CartId;
        item.ProvisionalMoney = item.Price * item.Quantity;
        formData.append("dataJson", JSON.stringify(item));
        //gọi api thêm mới cartItem
        const res = await cartItemService.post(formData);
        if (res.status === 201) {
          //gọi api lấy cartItem theo cartID
          const res = await cartItemService.getByCartId(this.userInfo.CartId);
          if (res.status === 200) {
            //gán lại cart vào storage
            cartLocalStorageService.setCartToLocalStorage(res.data);
            //lấy ra danh sác cart có book id vừa thêm
            const cartItem = res.data.filter((cartItem) => {
              return cartItem.BookId === item.BookId;
            });
            //lấy ra cartItem được chon và gán lại vào storage
            this.orderIdReOrder.push(cartItem[0].CartItemId);
            localStorageService.setItemToLocalStorage(
              "itemSelected",
              this.orderIdReOrder
            );
            //Update lại tổng số lượng sản phẩm trong cart
            this.$emitter.emit("getQuantityOfCart");
            location.href = "http://localhost:8080/cart";
          }
        }
      });
    },
  },
};
</script>
<style>
@import url(./order.css);
</style>
