<template>
  <form @submit.prevent="onSubmit">
    <input type="text" v-model="model.name" placeholder="Nazwa" />
    <input-error v-bind:errors="err.Name" />
    <input type="text" v-model="model.zipCode" placeholder="Kod pocztowy (format: 00-000)" />
    <input-error v-bind:errors="err.ZipCode" />
    <input type="text" v-model="model.street" placeholder="Adres" />
    <input-error v-bind:errors="err.Street" />
    <input type="submit" value="Zapisz zmiany" />
  </form>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import LocationsService, {
  LocationFormModel
} from "@/services/LocationsService";

import InputError from "@/components/InputError.vue";
import { warn } from "vue-class-component/lib/util";

@Component({
  components: {
    InputError
  }
})
export default class AdminLibrary extends Vue {
  private err: any;
  private model: LocationFormModel = {
    locationId: 0,
    name: "",
    zipCode: "",
    street: ""
  };

  created() {
    this.err = {
      Name: [],
      Street: [],
      ZipCode: []
    };
    this.loadData();
  }

  private get id(): number {
    return Number(this.$route.params.id || 0);
  }

  async loadData() {
    try {
      if (this.id != 0) {
        const response = await LocationsService.getLocation(this.id);
        this.model = response;
      }
    } catch (ex) {
      console.log(ex);
    }
  }

  async onSubmit() {
    try {
      this.err = {
        Name: [],
        Street: [],
        ZipCode: []
      };
      this.$forceUpdate();
      if (this.id == 0) {
        const response = await LocationsService.postLocation(this.model);
        this.model = response;
        this.$router.push(`/admin/locations/${this.model.locationId}`);
      } else {
        await LocationsService.putLocation(this.model, this.id);
      }
    } catch (ex) {
      this.err = ex.errors;
      this.$forceUpdate();
    }
  }
}
</script>

<style scoped>
form > input,
form > div {
  margin-bottom: 8px;
}
</style>