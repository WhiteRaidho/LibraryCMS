declare module "*.vue" {
  import Vue from "vue";
  export default Vue;
}

declare module 'global'
{
  import Vue from 'vue';

  module 'vue/types/vue'
  {
    interface VueConstructor
    {
      router: any;
    }
  }
}