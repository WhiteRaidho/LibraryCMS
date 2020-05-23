import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";

Vue.use(VueAxios, axios);
Vue.axios.defaults.baseURL = process.env.VUE_APP_API_URL;
Vue.axios.interceptors.request.use(config => {
    // let url = new URL(`${config.baseURL}/${config.url}`);
    return config;
});

Vue.axios.interceptors.response.use(
    response => {
        return response;
    },
    error => {
        const ex = {
            code: 404,
            message: "Not found",
            data: null as any,
            inner: null as any
        };

        if(error.response)
        {
            ex.code = error.response.status;
            ex.data = (typeof error.response.data === "string")?{message: error.response.data}:error.response.data;
            ex.message = ex.data.message?ex.data.message:error.message;
            ex.inner = error;
        }

        return Promise.reject(ex);
    }
);