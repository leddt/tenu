<template>
  <tenu-tabs>
    <tenu-tab title="Data">
      <tenu-field
        v-for="prop in contentType.properties"
        :key="prop.alias"
        :label="prop.name"
        :hint="prop.hint"
        v-slot="{ id }"
      >
        <component
          :is="`tenu-editor-${prop.propertyTypeAlias}`"
          :id="id"
          :prop="prop"
          :content="content"
        />
      </tenu-field>

      {{ content }}
    </tenu-tab>
    <tenu-tab title="URLs"> </tenu-tab>
  </tenu-tabs>
</template>
<script>
export default {
  props: {
    contentType: {
      type: Object,
      required: true
    },
    content: {
      type: Object,
      required: true
    }
  }
};
</script>
