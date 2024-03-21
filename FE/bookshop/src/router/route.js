import { createRouter, createWebHistory } from "vue-router";

import LayoutUser from "@/components/user/layout/layout-default/LayoutUser.vue";
import LayoutAdmin from "@/components/admin/layout/layout-default/LayoutAdmin.vue";
import BookPage from "../pages/admin/book/Index.vue";
import CategoryPage from "../pages/admin/category/Index.vue";
import LoginAdminPage from "../pages/admin/login/Thelogin.vue";
const routes = [
  {
    path: "/",
    component: LayoutUser,
  },
  {
    path: "/admin/login",
    component: LoginAdminPage,
  },
  {
    path: "/admin",
    component: LayoutAdmin,
    children: [
      {
        path: "book-management",
        components: {
          ContentRouterView: BookPage,
        },
      },
      {
        path: "category-management",
        components: {
          ContentRouterView: CategoryPage,
        },
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
