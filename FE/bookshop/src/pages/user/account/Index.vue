<template>
    <div class="layout-account py-3">
        <div class="container ">
            <div class="row">
                <div class="col-3">
                    <SidebarAccount></SidebarAccount>
                </div>
                <div class="col-9">
                    <MainContentAccount></MainContentAccount>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import MainContentAccount from './MainContentAccount.vue'
import SidebarAccount from './SidebarAccount.vue'
export default{
    name: "UserAccountPage",
    components: {MainContentAccount,SidebarAccount}
}
</script>
<style scoped>
.layout-account{
    background-color: #F0F0F0;
}
.layout-account .container{
}
</style>