<template>
    <div class="container">
        <span v-for="(index) in 5" :key="index" v-on:click="change(index)">
            <i :class="['fas', 'fa-star', {'star': changeable}, {'checked': (index <= rate)}]"></i>
        </span>
    </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';

@Component({
    name: "stars",
    inheritAttrs: false
})

export default class Stars extends Vue {
    @Prop() rate!: number;
    @Prop() changeable!: boolean;

    change(i: number) {
        if(this.changeable)
            this.$emit("changeRating", i);
    }
}
</script>

<style scoped>
.checked {
    color: orange;
}

.star:hover {
    color: yellow;
    cursor: pointer;
}

.container {
    display: inline-block;
    white-space: nowrap;
}

</style>