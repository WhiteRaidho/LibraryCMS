<template>
  <table class="content-table semi-transparent">
    <thead>
      <tr>
        <th v-for="(header, index) in headers" :key="index">
          {{ header.name }}
        </th>
      </tr>
    </thead>
    <tbody v-if="items.length > 0">
      <tr v-for="(item, index) in items" :key="index">
        <td v-for="(field, index) in headers" :key="index">
          <router-link :to="generateLink(field.link, item)" v-if="field.link">
            {{ item[field.fieldName] }}
          </router-link>
          <span v-else>
            {{ item[field.fieldName] }}
          </span>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';

@Component({
  name: 'content-table',
  inheritAttrs: false
})
export default class ContentTable extends Vue{
  @Prop() private items!: any[];
  @Prop() headers!: any[];

  generateLink(link: string, item: any): string{
    const toReplace = /[{]\w+[}]/g.exec(link);
    if(!toReplace) return link;
    toReplace.forEach(a => {
      const field = a.replace("{", "").replace("}", "");
      link = link.replace(a, item[field]);
    });
    return link;
  }
}
</script>

<style lang="scss">
.content-table {
  border-collapse: collapse;
  margin: auto;
  margin-top: 20px;
  font-size: 0.9em;
  min-width: 700px;
  border-radius: 8px 8px 0 0;
  overflow: hidden;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
  padding: 20px;
}
.content-table thead tr {
  background-color: var(--info-color);
  color: var(--main-white-color);
  text-align: left;
  font-weight: bold;
}
.content-table th,
.content-table td {
  padding: 12px 15px;
  text-align: left;
}
.content-table tbody tr {
  border-bottom: 1px solid #dddddd;
}
.content-table tbody tr:nth-of-type(even) {
  background-color: #f3f3f340;
}
.content-table tbody tr:last-of-type {
  border-bottom: 2px solid var(--info-color);
}
</style>
