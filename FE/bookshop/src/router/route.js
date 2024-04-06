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
import ProdcutPage from "../pages/user/product/Index.vue";
import UserAccountPage from "../pages/user/account/Index.vue";
import InfoAccountUserPage from "../pages/user/account/InfoAccount.vue";
import BookAddressPage from "../pages/user/account/BookAddress.vue";

import { checkInfoTokensInStorage } from "@/js/token/TokenService";
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
          ViewRouterContainer: ProdcutPage,
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
  if (to.meta.requiresAuth && !checkInfoTokensInStorage()) {
    next("/admin/login");
  } else if (
    to.path === "Login" &&
    checkInfoTokensInStorage()
  ) {
    next();
    location.reload();
  } else {
    next();
  }
});
export default router;
