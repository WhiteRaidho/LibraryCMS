<template>
    <form @submit.prevent="onSubmit" class="search-bar">
      <input type="text" :placeholder="text" v-model="search" />
      <button type="submit"><i class="fas fa-search"></i></button>
    </form>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";

@Component({
    name: 'search-bar',
    inheritAttrs: false
})
export default class SeachBar extends Vue {
    @Prop() private text!: string;
    private search = "";

    onSubmit()
    {
        this.$emit("submit", this.search);
    }
}
</script>

<style scoped>
form.search-bar input[type=text] {
  padding: 10px;
  font-size: 17px;
  /* border: solid 1px; */
  border-right: solid 1px var(--main-gray-color);
  border-radius: 8px;
  border-top-right-radius: 0px;
  border-bottom-right-radius: 0px;
  float: left;
  width: calc(100% - 50px);
  outline: none;
}

form.search-bar button {
  float: left;
  width: 50px;
  padding: 10px;
  font-size: 17px;
  border-radius: 8px;
  border-top-left-radius: 0px;
  border-bottom-left-radius: 0px;
  cursor: pointer;
  outline: none;
}

form.search-bar::after {
  content: "";
  clear: both;
  display: table;
}

form.search-bar button:hover {
  background: var(--info-color);
}
</style>