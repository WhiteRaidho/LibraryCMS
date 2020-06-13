<template>
    <div class="star-container" :class="{'star-background': !noBackground}">
        <span v-for="(index) in 5" :key="index" v-on:click="change(index)">
            <i :class="['fas', 'fa-star', {'star': changeable}, {'star-checked': (index <= Math.round(rate))}]"></i>
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
    @Prop() noBackground: boolean;

    change(i: number) {
        if(this.changeable)
            this.$emit("changeRating", i);
    }
}
</script>

<style scoped>
.star-checked {
    color: orange;
}

.star:hover {
    color: yellow;
    cursor: pointer;
}

.star-container {
    height: 1.05em;
    padding:2px 6px;
    border-radius: 30px;
    display: inline-block;
    white-space: nowrap;
}

.star-background {
    background-color: var(--main-dark-white-color);
}

</style>