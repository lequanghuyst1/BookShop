import { createRouter, createWebHistory } from "vue-router";

// import LayoutUser from "@/components/user/layout/layout-default/LayoutUser.vue";
import LayoutHeaderAndFooter from "@/components/user/layout/layout-default/LayoutHeaderAndFooter.vue";
import LayoutAdmin from "@/components/admin/layout/layout-default/LayoutAdmin.vue";
import BookPage from "../pages/admin/book/Index.vue";
import CategoryPage from "../pages/admin/category/Index.vue";
import PulisherPage from "../pages/admin/publisher/Index.vue";
import LoginAdminPage from "../pages/admin/login/Thelogin.vue";
import HomeUserPage from "../pages/user/home/Index.vue";
import CartPage from "../pages/user/cart/Index.vue";
import CategoryUserPage from "../pages/user/category/Index.vue";
import ProdcutPage from "../pages/user/product/Index.vue";
import UserAccountPage from "../pages/user/account/Index.vue";
import InfoAccountUserPage from "../pages/user/account/InfoAccount.vue";

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
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  next();
});
export default router;
