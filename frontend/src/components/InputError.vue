<template>
    <div>
        <ul class="p4 error-list" v-if="errors">
            <li v-for="error in errors" :key="error" class="warning font-xs error-list-item">{{ error }}</li>
        </ul>
    </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';

@Component({
    name: "input-error",
    inheritAttrs: false
})

export default class InputError extends Vue {
    @Prop() errors!: [];
}
</script>

<style scoped>
.error-list {
    margin: 0px;
}
.error-list-item {
    text-align: left;
}
</style>