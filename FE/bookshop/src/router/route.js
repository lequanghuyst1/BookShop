import { createRouter, createWebHistory } from "vue-router";

// import LayoutUser from "@/components/user/layout/layout-default/LayoutUser.vue";
import LayoutAdmin from "@/components/admin/layout/layout-default/LayoutAdmin.vue";
import BookPage from "../pages/admin/book/Index.vue";
import CategoryPage from "../pages/admin/category/Index.vue";
import PulisherPage from "../pages/admin/publisher/Index.vue";
import LoginAdminPage from "../pages/admin/login/Thelogin.vue";
import HomeAdminPage from "../pages/admin/home/Index.vue";

import LayoutHeaderAndFooter from "@/components/user/layout/layout-default/LayoutHeaderAndFooter.vue";
import HomeUserPage from "../pages/user/home/Index.vue";
import CartPage from "../pages/user/cart/Index.vue";
import CategoryUserPage from "../pages/user/category/Index.vue";
import ProductPage from "../pages/user/product/Index.vue";
import UserAccountPage from "../pages/user/account/Index.vue";
import InfoAccountUserPage from "../pages/user/account/InfoAccount.vue";
import BookAddressPage from "../pages/user/account/BookAddress.vue";
import HomeAccountUserPage from "../pages/user/account/HomeAccount.vue";

import { checkInfoTokensInStorage } from "@/js/token/TokenService";
import localStorageService from "@/js/storage/LocalStorageService";
const routes = [
  {
    path: "/",
    component: LayoutHeaderAndFooter,
    children: [
      {
        path: "",
        components: {
          ViewRouterContainer: HomeUserPage,
        },
      },
      {
        path: "cart",
        components: {
          ViewRouterContainer: CartPage,
        },
      },
      {
        path: "collections/:id",
        name: "category",
        components: {
          ViewRouterContainer: CategoryUserPage,
        },
        props: true,
      },
      {
        path: "product/:id",
        name: "product",
        components: {
          ViewRouterContainer: ProductPage,
        },
        props: true,
      },
      {
        path: "customer/account",
        name: "account",
        components: {
          ViewRouterContainer: UserAccountPage,
        },
        children: [
          {
            path: "",
            name: "home-account",
            components: {
              ViewRouterMainContentAccount: HomeAccountUserPage,
            },
          },
          {
            path: "user-info",
            name: "edit-account",
            components: {
              ViewRouterMainContentAccount: InfoAccountUserPage,
            },
          },
          {
            path: "address-info",
            name: "address-account",
            components: {
              ViewRouterMainContentAccount: BookAddressPage,
            },
            children: {},
          },
        ],
      },
    ],
  },
  {
    path: "/admin/login",
    component: LoginAdminPage,
  },
  {
    path: "/admin",
    component: LayoutAdmin,
    meta: { requiresAuth: true },
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
      {
        path: "publisher-management",
        components: {
          ContentRouterView: PulisherPage,
        },
      },
      {
        path: "home",
        components: {
          ContentRouterView: HomeAdminPage,
        },
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const user = localStorageService.getItemEncodeFromLocalStorage("userInfo");

  if (
    to.meta.requiresAuth &&
    !checkInfoTokensInStorage() &&
    user?.RoleName === "User"
  ) {
    next("/admin/login");
  } else if (
    to.path === "/admin/login" &&
    user?.RoleName === "Admin" &&
    checkInfoTokensInStorage()
  ) {
    next();
  } else {
    next();
  }
});
export default router;
