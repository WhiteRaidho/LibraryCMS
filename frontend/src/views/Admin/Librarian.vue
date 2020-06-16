<template>
  <form @submit.prevent="onSubmit">
    <select v-model="model.userId" :options="users" class="m8">
      <option v-for="user in users" v-bind:value="user.userId" :key="user.userId">
        {{ user.firstName }} {{ user.lastName }}, {{ user.userName}}, {{ user.email }}
      </option>
    </select>
    <input-error v-bind:errors="err.UserId" />
    <select v-model="model.libraryId" :options="libraries" class="m8">
      <option v-for="lib in libraries" v-bind:value="lib.libraryId" :key="lib.libraryId">
        {{ lib.name }}, {{lib.locationName}}, {{ lib.locationStreet }}
      </option>
    </select>
    <input-error v-bind:errors="err.LibraryId" />
    <select v-model="model.userRole" :options="roles" class="m8">
      <option v-for="role in roles" v-bind:value="role.value" :key="role.value">
        {{ role.text }}
      </option>
    </select>
    <input-error v-bind:errors="err.UserRole" />
    <input type="submit" value="Zapisz zmiany" class="m8" />
  </form>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import RolesService, { RoleFormModel } from "@/services/RolesService";
import LibrariesService, { LibraryListItem } from "@/services/LibrariesService";
import AuthService, { AuthModel } from "@/services/AuthService";

import InputError from "@/components/InputError.vue";

@Component({
  components: {
    InputError
  }
})
export default class AdminLibrarian extends Vue {
  private err: any;

  private users: AuthModel[] = [];
  private libraries: LibraryListItem[] = [];
  private roles: {value: number, text: string}[] = [
    {value: 0, text: "Użytkownik"},
    {value: 1, text: "Bibliotekarz"},
    {value: 2, text: "Manager"}
  ];

  private model: RoleFormModel = { roleId: 0, userId: "", libraryId: 0, userRole: 0 };

  created() {
    this.err = {
      UserId: [],
      LibraryId: [],
      UserRole: []
    };
    this.loadLibraries();
    this.loadUsers();
    this.loadData();
  }

  async loadLibraries() {
    try {
      const response = await LibrariesService.getList();
      this.libraries = response;
    } catch (ex) {
      this.libraries = [];
      console.log(ex);
    }
  }

  async loadUsers() {
    try {
      const response = await AuthService.getUserList();
      this.users = response;
    } catch (ex) {
      this.users = [];
      console.log(ex);
    }
  }

  private get id(): number {
    return Number(this.$route.params.id || 0);
  }

  async loadData() {
    try {
      if (this.id != 0) {
        const response = await RolesService.getRole(this.id);
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
        const response = await RolesService.postRole(this.model);
        this.model = response;
        this.$router.push(`/admin/librarians/${this.model.roleId}`);
      } else {
        await RolesService.putRole(this.model, this.id);
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