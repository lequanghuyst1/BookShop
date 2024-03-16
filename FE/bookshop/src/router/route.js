
import { createRouter, createWebHistory } from "vue-router";

import LayoutUser from "@/components/user/layout/layout-default/LayoutUser.vue";
import LayoutAdmin from "@/components/admin/layout/layout-default/LayoutAdmin.vue";

const routes = [
    {
        path: "/",
        component: LayoutUser,
    },
    {
        path: "/admin",
        component: LayoutAdmin,
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes,
  });

export default router;
