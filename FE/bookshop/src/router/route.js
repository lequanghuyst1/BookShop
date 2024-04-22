import { createRouter, createWebHistory } from "vue-router";

// import LayoutUser from "@/components/user/layout/layout-default/LayoutUser.vue";
import LayoutAdmin from "@/components/admin/layout/layout-default/LayoutAdmin.vue";
import BookPage from "../pages/admin/book/BookPage.vue";
import CategoryPage from "../pages/admin/category/CategoryPage.vue";
import PulisherPage from "../pages/admin/publisher/PublisherPage.vue";
import LoginAdminPage from "../pages/admin/login/Thelogin.vue";
import HomeAdminPage from "../pages/admin/home/Index.vue";
import OrderManegementPage from "../pages/admin/order/OrderPage.vue";
import OrderDetailAdminPage from "@/pages/admin/order/OrderDetail.vue";

import LayoutHeaderAndFooter from "@/components/user/layout/layout-default/LayoutHeaderAndFooter.vue";
import HomeUserPage from "../pages/user/home/HomePage.vue";
import CartPage from "../pages/user/cart/CartPage.vue";
import CategoryUserPage from "../pages/user/category/CategoryPage.vue";
import OnlinePayment from "@/pages/user/pay/OnlinePayment.vue";
import ProductPage from "../pages/user/product/Product.vue";
import UserAccountPage from "../pages/user/account/Index.vue";
import InfoAccountUserPage from "../pages/user/account/InfoAccount.vue";
import BookAddressPage from "../pages/user/account/BookAddress.vue";
import HomeAccountUserPage from "../pages/user/account/HomeAccount.vue";
import PayUserPage from "@/pages/user/pay/Pay.vue";
import OrderPage from "@/pages/user/order/OrderPage.vue";
import OrderDatail from "@/pages/user/order/OrderDetail.vue";

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
        path: "collections/:slug",
        name: "category",
        components: {
          ViewRouterContainer: CategoryUserPage,
        },
        props: true,
      },
      {
        path: "/:slug",
        name: "product",
        components: {
          ViewRouterContainer: ProductPage,
        },
        props: true,
      },
      {
        path: "customer",
        name: "customer",
        components: {
          ViewRouterContainer: UserAccountPage,
        },
        children: [
          {
            path: "home",
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
          },
          {
            path: "order",
            name: "order",
            components: {
              ViewRouterMainContentAccount: OrderPage,
            },
          },
          {
            path: "order/order-detail/:id",
            name: "order-detail",
            components: {
              ViewRouterMainContentAccount: OrderDatail,
            },
          },
        ],
      },
    ],
  },
  {
    path: "/payment/vnpay/transaction",
    component: OnlinePayment,
  },
  {
    path: "/admin/login",
    component: LoginAdminPage,
  },
  {
    path: "/checkout",
    component: PayUserPage,
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
      {
        path: "order-manegement",
        components: {
          ContentRouterView: OrderManegementPage,
        },
        children: [],
      },
      {
        path: "order-manegement/:id",
        components: {
          ContentRouterView: OrderDetailAdminPage,
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
  const user = localStorageService.getItemFromLocalStorage("userInfo");

  if (to.meta.requiresAuth && !checkInfoTokensInStorage()) {
    next("/admin/login");
  } else if (
    to.meta.requiresAuth &&
    to.path === "/admin/login" &&
    user?.RoleName === "Admin" &&
    checkInfoTokensInStorage()
  ) {
    next();
  } else {
    console.log(to);
    next();
  }
});
export default router;
