import { createRouter, createWebHistory } from "vue-router";

// import LayoutUser from "@/components/user/layout/layout-default/LayoutUser.vue";
import LayoutAdmin from "@/components/admin/layout/layout-default/LayoutAdmin.vue";
import BookPage from "../pages/admin/book/BookPage.vue";
import CategoryPage from "../pages/admin/category/CategoryPage.vue";
import PulisherPage from "@/pages/admin/publisher/PublisherPage.vue";
import LoginAdminPage from "../pages/admin/login/Thelogin.vue";
import OverviewPage from "../pages/admin/home/OverviewPage.vue";
import OrderManegementPage from "../pages/admin/order/OrderPage.vue";
import OrderDetailAdminPage from "@/pages/admin/order/OrderDetail.vue";
import RevenueStatisticsPage from "@/pages/admin/revenue-statistics/RevenueStatisticsPage.vue";
import CustomerPage from "@/pages/admin/customer/CustomerPage.vue";

import LayoutHeaderAndFooter from "@/components/user/layout/layout-default/LayoutHeaderAndFooter.vue";
import HomeUserPage from "../pages/user/home/HomePage.vue";
import DealHotPage from "../pages/user/home/DealHotPage.vue";
import BookBestSellingPage from "../pages/user/home/BookBestSellingPage.vue";
import BookOutstanding from "../pages/user/home/BookOutstanding.vue";
import CartPage from "../pages/user/cart/CartPage.vue";
import CategoryUserPage from "../pages/user/category/CategoryPage.vue";
import PaymentSuccess from "@/pages/user/pay/PaymentSuccess.vue";
import ProductPage from "../pages/user/product/Product.vue";
import UserAccountPage from "../pages/user/account/Index.vue";
import InfoAccountUserPage from "../pages/user/account/InfoAccount.vue";
import BookAddressPage from "../pages/user/account/BookAddress.vue";
import HomeAccountUserPage from "../pages/user/account/HomeAccount.vue";
import PayUserPage from "@/pages/user/pay/Pay.vue";
import OrderPage from "@/pages/user/order/OrderPage.vue";
import OrderList from "@/pages/user/order/OrderList.vue";
import OrderDatail from "@/pages/user/order/OrderDetail.vue";
import SearchEnginePage from "@/pages/user/search-engine/SearchEngine.vue";

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
        path: "deal-hot-page",
        components: {
          ViewRouterContainer: DealHotPage,
        },
      },
      {
        path: "book-best-selling",
        components: {
          ViewRouterContainer: BookBestSellingPage,
        },
      },
      {
        path: "book-new",
        components: {
          ViewRouterContainer: BookOutstanding,
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
            exact: true,

            children: [
              {
                path: "",
                name: "order-list",
                components: {
                  ViewRouterContentOrderPage: OrderList,
                },
                exact: true,
              },
              {
                path: "order-detail/:id",
                name: "order-detail",
                components: {
                  ViewRouterContentOrderPage: OrderDatail,
                },
                props: true,
                exact: true,
              },
            ],
          },
        ],
      },
      {
        path: "payment/success",
        name: "success",
        components: {
          ViewRouterContainer: PaymentSuccess,
        },
        props: true,
      },
      {
        path: "search-engine/:searchString",
        name: "search-engine",
        components: {
          ViewRouterContainer: SearchEnginePage,
        },
      },
    ],
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
          ContentRouterView: OverviewPage,
        },
      },
      {
        path: "order-management",
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
      {
        path: "revenue-statistics",
        components: {
          ContentRouterView: RevenueStatisticsPage,
        },
      },
      {
        path: "customer-management",
        components: {
          ContentRouterView: CustomerPage,
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
  }
  // else if (to.meta.requiresAuth && user?.RoleName !== "Admin") {
  //   next("/admin/login");
  // }
  // else if (to.meta.requiresAuth && user?.RoleName === "Admin") {
  //   next();
  // }
  else {
    next();
  }
});
export default router;
