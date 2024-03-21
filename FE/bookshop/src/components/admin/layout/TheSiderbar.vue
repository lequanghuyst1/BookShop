<template>
  <div class="sidebar">
    <div class="sidebar__list">
      <router-link
        v-for="(item, index) in sidebarList"
        :key="index"
        :to="item.path"
        class="sidebar__item"
      >
        <div class="sidebar__item-icon">
          <i :class="item.icon"></i>
        </div>
        <p v-if="isShowTitle" class="sidebar__item-tilte">{{ item.title }}</p>
      </router-link>
    </div>
    <div @click="toggleSidebar" class="sidbar__action">
      <div v-if="isShowTitle" class="sidebar__item sidbar__action-collsape">
        <div class="sidebar__item-icon">
          <div class="m-icon-prev"></div>
        </div>
        <p v-if="isShowTitle" class="sidebar__item-tilte">Thu gọn</p>
      </div>
      <div
        v-if="isShowTitle == false"
        class="sidebar__item sidbar__action-expand"
      >
        <div class="sidebar__item-icon">
          <div class="m-icon-next"></div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import menu from "@/js/data/siderbar";
export default {
  name: "TheSidebar",
  data() {
    return {
      sidebarList: menu,
      isShowSideBar: true,
      isShowTitle: true,
    };
  },
};
</script>
<style scoped>
@import url(../css/sidebar.css);
</style>
