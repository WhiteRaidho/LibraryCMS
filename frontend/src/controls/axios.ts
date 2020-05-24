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
            inner: null as any,
            errors: {}
        };

        if(error.response)
        {
            ex.code = error.response.status;
            ex.data = (typeof error.response.data === "string")?{message: error.response.data}:error.response.data;
            ex.message = ex.data.message?ex.data.message:error.message;
            ex.inner = error;
            ex.errors = ex.data.errors?ex.data.errors:error.message;
        }

        if(ex.code == 400 && !ex.data.message)
        {
            ex.message = "Ups, coś poszło nie tak";
        }
        

        return Promise.reject(ex);
    }
);