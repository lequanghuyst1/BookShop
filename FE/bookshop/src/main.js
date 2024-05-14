import { createApp } from "vue";
import App from "./App.vue";
import PrimeVue from "primevue/config";
//in main.js
import "primevue/resources/themes/aura-light-green/theme.css";
import "primevue/resources/primevue.min.css";

import router from "./router/route";
import axios from "axios";

import MButton from "./components/base/button/MButton.vue";
import MButtonNoText from "./components/base/button/MButtonNoText.vue";
import MDialog from "./components/base/dialog/MDialog.vue";
import MForm from "./components/base/form/MForm.vue";
import MToast from "./components/base/toast/MToast.vue";
import MLoading from "./components/base/loading/MLoading.vue";
import MLoadingData from "./components/base/loading/MLoadingData.vue";
import MTable from "./components/base/table/MTable.vue";
import MPagination from "./components/base/pagination/MPagination.vue";
import MInput from "./components/base/input/MInput.vue";
import MInputIcon from "./components/base/input/MInputIcon.vue";
import MInputImage from "./components/base/input/MInputImage.vue";
import MGenderRadio from "./components/base/radio-group/MGenderRadio.vue";
import MCombobox from "./components/base/combobox/MCombobox.vue";
import MDropdown from "./components/base/dropdown/MDropdown.vue";
import MTextarea from "./components/base/textarea/MTextarea.vue";

import Calendar from "primevue/calendar";

import tinyEmitter from "tiny-emitter/instance";
import Enum from "./js/resource/enum";
import Resource from "./js/resource/resource";
import helper from "./js/helper";
import httpRequest from "./utils/request";

import VueTippy from "vue-tippy";
import "tippy.js/dist/tippy.css";
import "tippy.js/animations/scale.css";
import "tippy.js/animations/scale-subtle.css";
import "tippy.js/animations/scale-extreme.css";

// Import Bootstrap and BootstrapVue CSS files (order is important)
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

const app = createApp(App);

app.component("MButton", MButton);
app.component("MButtonNoText", MButtonNoText);
app.component("MDialog", MDialog);
app.component("MForm", MForm);
app.component("MToast", MToast);
app.component("MLoading", MLoading);
app.component("MTable", MTable);
app.component("MPagination", MPagination);
app.component("MInput", MInput);
app.component("MInputImage", MInputImage);
app.component("MGenderRadio", MGenderRadio);
app.component("MInputIcon", MInputIcon);
app.component("MCombobox", MCombobox);
app.component("MDropdown", MDropdown);
app.component("MLoadingData", MLoadingData);
app.component("MTextarea", MTextarea);
app.component("GCalendar", Calendar);

app.config.globalProperties.$emitter = tinyEmitter;
app.config.globalProperties.$Enum = Enum;
app.config.globalProperties.$Resource = Resource;
app.config.globalProperties.$languageCode = "VN";
app.config.globalProperties.$helper = helper;
app.config.globalProperties.$httpRequest = httpRequest;
app.config.globalProperties.$axios = axios;

import { createStore } from "vuex";

// import stores from "@/js/store/index.js"
// Create a new store instance.
const store = createStore({
  state() {
    return {
      count: 1000,
      seachString: "",
      globalErrorMsg: [],
    };
  },
  getters: {
    getCount(state) {
      return state.count;
    },
    searchString: (state) => state.seachString,
    globalErrorMsg: (state) => state.globalErrorMsg,
  },
  mutations: {
    increment(state) {
      state.count++;
    },
    setSearchString(state, string) {
      state.seachString = string;
    },
    // Thực hiện thêm 1 lỗi vào globalErrorMsg
    SET_GLOBAl_VALIDATE_ERROR(state, error) {
      if (state.globalErrorMsg.length > 0) {
        state.globalErrorMsg = state.globalErrorMsg.filter(
          (item) => item.name !== error.name
        );
      }
      state.globalErrorMsg.push(error);
      console.log(state.globalErrorMsg)
    },
    CHECK_ERROR_EMPTY(state, name) {
      if (state.globalErrorMsg.length > 0) {
        state.globalErrorMsg = state.globalErrorMsg.filter(
          (item) => item.name !== name
        );
      }
    },
    //Thực hiện xóa tất cả các lỗi
    SET_GLOBAL_VALIDATE_DEFAULT(state) {
      state.globalErrorMsg = [];
    },
  },
  actions: {
    setSearchString({ commit }, string) {
      commit("setSearchString", string);
    },
    setSearchStringDefault({ commit }) {
      commit("setSearchString", "");
    },

    //Thực hiện lưu thông báo lỗi validate
    setGlobalValidateError({ commit }, error) {
      commit("SET_GLOBAl_VALIDATE_ERROR", error);
    },
    //kiểm tra thông báo lỗi validate
    checkErrorEmpty({ commit }, name) {
      commit("CHECK_ERROR_EMPTY", name);
    },
    //thực hiện xóa tất cả các lỗi
    setGlobalValidateDefault({ commit }) {
      commit("SET_GLOBAL_VALIDATE_DEFAULT");
    },
  },
});

// Install the store instance as a plugin
app.use(store);
app.use(
  VueTippy,
  // optional
  {
    directive: "tippy", // => v-tippy
    component: "tippy", // => <tippy/>
    componentSingleton: "tippy-singleton", // => <tippy-singleton/>,
    defaultProps: {
      placement: "auto-end",
      allowHTML: true,
    }, // => Global default options * see all props
  }
);
app.use(PrimeVue);
app.use(router);
// Make BootstrapVue available throughout your project
// Optionally install the BootstrapVue icon components plugin
// Optionally install the BootstrapVue icon components plugin

app.mount("#app");
