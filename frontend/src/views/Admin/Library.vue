<template>
  <form @submit.prevent="onSubmit">
    <input type="text" v-model="model.name" placeholder="Nazwa" />
    <input-error v-bind:errors="err.Name" />
    <select v-model="model.locationId" :options="locations" class="f-left">
      <option
        v-for="loc in locations"
        v-bind:value="loc.locationId"
        :key="loc.locationId"
      >{{ loc.name }}, {{ loc.zipCode }}, {{ loc.street }}</option>
    </select>
    <input type="submit" value="Zapisz zmiany" class="m8" />
  </form>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import LibrariesService, {
  LibraryFormModel
} from "@/services/LibrariesService";
import LocationsService, {
  LocationFormModel
} from "@/services/LocationsService";
import InputError from "@/components/InputError.vue";

@Component({
  components: {
    InputError
  }
})
export default class AdminLibrary extends Vue {
  private err: any;
  private model: LibraryFormModel = { libraryId: 0, name: "", locationId: 0 };
  private locations: LocationFormModel[] = [];

  created() {
    this.err = {
      Name: []
    };
    this.loadLocations();
    this.loadData();
  }

  private get id(): number {
    return Number(this.$route.params.id || 0);
  }

  async loadLocations() {
    try {
      const response = await LocationsService.getList();
      this.locations = response;
    } catch (ex) {
      console.log(ex);
    }
  }

  async loadData() {
    try {
      if (this.id != 0) {
        const response = await LibrariesService.getLibrary(this.id);
        this.model = response;
      }
    } catch (ex) {
      console.log(ex);
    }
  }

  async onSubmit() {
    this.err = {
      Name: []
    };
    try {
      if (this.id == 0) {
        const response = await LibrariesService.postLibrary(this.model);
        this.model = response;
        this.$router.push(`/admin/libraries/${this.model.libraryId}`);
      } else {
        await LibrariesService.putLibrary(this.model, this.id);
      }
    } catch (ex) {
      this.err = ex.errors;
    }
    this.$forceUpdate();
  }
}
</script>

<style scoped>
form > input,
form > div {
  margin-bottom: 8px;
}
</style>