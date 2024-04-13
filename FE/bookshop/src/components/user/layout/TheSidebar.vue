<template>
  <div class="sidebar w-100 flex-column justify-content-start flex-grow-0">
    <div class="widget widget-menu flex-shrink-0">
      <div class="sidebar__heading d-flex align-items-center">
        <div class="sidebar__heading-icon">
          <i class="fa-solid fa-list"></i>
        </div>
        <span class="sidebar__heading-title"> Danh mục </span>
      </div>
      <div class="sidebar__menu">
        <ul class="list-unstyled mb-0">
          <li
            v-for="item in categories"
            :key="item.CategoryId"
            class="sidbar__menu-item"
          >
            <router-link
              class="text-decoration-none"
              @click="goToCategoryPage"
              :to="{
                name: 'category',
                path: 'collecttion',
                params: { slug: item.CategorySlug },
              }"
              >{{ item.CategoryName }}</router-link
            >
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>
<script>
import categoryService from "@/utils/CategoryService";
export default {
  name: "TheSidebarUser",
  created() {
    this.loadDataCategory();
  },
  methods: {
    async loadDataCategory() {
      try {
        const res = await categoryService.getAll();
        switch (res.status) {
          case 200:
            this.categories = res.data;
            break;
        }
      } catch (error) {
        this.$emitter.emit("handleApiError", error);
      }
    },
    goToCategoryPage() {},
  },
  data() {
    return {
      categories: [],
    };
  },
};
</script>
<style scoped>
.sidebar {
  background-color: #fff;
  height: auto;
  border-radius: 8px;
  box-shadow: 0 1px 2px 0 rgba(60,64,67,.1), 0 2px 6px 2px rgba(60,64,67,.15);
}
.widget {
  padding: 10px 18px;
  background-color: #fff;
  border-radius: 4px;
}
.sidebar__heading {
  margin: 8px 0px 16px;
  font-size: 18px;
}
.sidebar__heading-icon {
  margin-right: 12px;
  font-weight: 900;
}
.sidebar__heading-title {
  font-weight: 600;
}
.sidbar__menu-item {
  border-top: 1px solid #eeeeee;
  padding: 10px 0;
  position: relative;
  /* animation: rightOut ease-in 0.5s; */
}
.sidbar__menu-item a {
  font-size: 14px;
  color: #333;
  display: block;
}
.sidbar__menu-item:hover a {
  color: #ff5653;
  animation: rightIn linear 0.5s forwards;
}
@keyframes rightIn {
  to {
    transform: translateX(12px);
  }
}
@keyframes rightOut {
  from {
    transform: translateX(12px);
  }
  to {
    transform: translateX(0);
  }
}
.widget-title {
  font-size: 16px;
  font-weight: 600;
  padding-bottom: 10px;
  border-bottom: 2px solid #eeeeee;
  position: relative;
}
.widget-title::before {
  display: block;
  content: "";
  width: 100px;
  height: 2px;
  position: absolute;
  left: 0;
  bottom: -1px;
  background-color: #417505;
}
.product-item {
  padding: 20px 0;
  border-bottom: 1px solid #eeeeee;
}
.product-img {
  width: 70px;
  flex-shrink: 0;
  position: relative;
}
.procduct-discount {
  background: #f03737;
  color: #fff;
  padding: 3px 4px;
  font-size: 12px;
  width: 36px;
  text-align: center;
  border-radius: 2px;
  position: absolute;
  top: -12px;
  left: 0;
  z-index: 1;
}
.product-link {
  text-decoration: none;
}
.product-title {
  width: 100%;
}
.product-title a {
  color: #4b4848;
  font-weight: 600;
  font-size: 14px;
  display: block;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.product-title a:hover {
  color: #ec8000;
}
.product-content {
  width: calc(100% - 100px);
}
.product-price {
  font-size: 14px;
  margin-top: 4px;
}
.product-pirce--discount {
  color: #f6001a;
  font-size: 15px;
}
.product-pirce--original {
  color: #999;
}
</style>
