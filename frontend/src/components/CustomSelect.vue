<template>
  <div class="custom-select" @click="showOptions = !showOptions">
    <div class>
      obecna
      <span class="c-arrow-down" v-if="!showOptions"></span>
      <span class="c-arrow-up" v-if="showOptions"></span>
    </div>
    <div class="custom-select-options" v-if="showOptions">
      <div class="custom-select-option" v-for="(opt, index) in options" :key="index">
          <label> <input type="checkbox" :value="opt"/> {{opt.name}}</label>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";

@Component({
  name: "custom-select",
  inheritAttrs: false
})
export default class CustomSelect extends Vue {
  @Prop() value!: any;
  @Prop() options!: [];
  @Prop() show: string;

  private showOptions = false;
}
</script>

<style scoped>
.custom-select {
  width: 100%;
  padding: 12px 20px;
  box-sizing: border-box;
  border: solid 0px;
  border-radius: 4px;
  background-color: var(--main-white-color);
  font-size: 16px;
  outline: none;
  display: grid;
  grid-template-columns: 10fr 1fr;
}

.custom-select-options {
  position: absolute;
  border-top: none;
  overflow: auto;
  width: 100%;
  z-index: 1500;
  margin: 0;
  padding: 0;
  border-radius: 8px;
}

.custom-select-option {
  height: 35px;
  display: grid;
  align-content: center;
  padding: 0 0 0 25px;
  background-color: var(--main-white-color);
  border-bottom: 1px solid #dddddd;
}

.custom-select-option:last-child {
  border-bottom: none;
}

 .custom-select-option input{
   display: none;
 }
</style>