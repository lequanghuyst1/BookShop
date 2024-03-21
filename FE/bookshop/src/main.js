import { createApp } from 'vue';
import App from './App.vue';

import router from './router/route';

import MButton from "./components/base/button/MButton.vue";
import MButtonNoText from "./components/base/button/MButtonNoText.vue";
import MDialog from "./components/base/dialog/MDialog.vue";
import MDialogNotice from "./components/base/dialog/MDialogNotice.vue";
import MForm from "./components/base/form/MForm.vue";
import MToast from "./components/base/toast/MToast.vue";
import MLoading from "./components/base/loading/MLoading.vue";
import MLoadingData from "./components/base/loading/MLoadingData.vue";
import MTable from "./components/base/table/MTable.vue";
import MPagination from "./components/base/pagination/MPagination.vue";
import MInput from "./components/base/input/MInput.vue";
import MInputIcon from "./components/base/input/MInputIcon.vue";
import MGenderRadio from "./components/base/radio-group/MGenderRadio.vue";
import MCombobox from "./components/base/combobox/MCombobox.vue";
import MDropdown from "./components/base/dropdown/MDropdown.vue";
import MTextarea from "./components/base/textarea/MTextarea.vue";

import tinyEmitter from "tiny-emitter/instance";
import Enum from './js/resource/enum';
import Resource from './js/resource/resource';
import helper from './js/helper';
import httpRequest from './utils/request';

import VueTippy from 'vue-tippy'
import 'tippy.js/dist/tippy.css' 
import 'tippy.js/animations/scale.css'
import 'tippy.js/animations/scale-subtle.css'
import 'tippy.js/animations/scale-extreme.css'
const app = createApp(App);


app.component("MButton", MButton);
app.component("MButtonNoText", MButtonNoText);
app.component("MDialog", MDialog);
app.component("MForm", MForm);
app.component("MDialogNotice", MDialogNotice);
app.component("MToast", MToast);
app.component("MLoading", MLoading);
app.component("MTable", MTable);
app.component("MPagination", MPagination);
app.component("MInput", MInput);
app.component("MGenderRadio", MGenderRadio);
app.component("MInputIcon", MInputIcon);
app.component("MCombobox", MCombobox);
app.component("MDropdown", MDropdown);
app.component("MLoadingData", MLoadingData);
app.component("MTextarea", MTextarea);

app.config.globalProperties.$emitter = tinyEmitter;
app.config.globalProperties.$Enum = Enum;
app.config.globalProperties.$Resource = Resource;
app.config.globalProperties.$languageCode = "VN";
app.config.globalProperties.$helper = helper;
app.config.globalProperties.$httpRequest = httpRequest;

app.use(
    VueTippy,
    // optional
    {
      directive: 'tippy', // => v-tippy
      component: 'tippy', // => <tippy/>
      componentSingleton: 'tippy-singleton', // => <tippy-singleton/>,
      defaultProps: {
        placement: 'auto-end',
        allowHTML: true,
      }, // => Global default options * see all props
    }
  )


app.use(router);


app.mount('#app')
