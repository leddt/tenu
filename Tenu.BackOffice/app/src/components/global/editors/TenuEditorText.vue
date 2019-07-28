<template>
  <tenu-textbox
    :id="id"
    :required="prop.required"
    v-model="content.properties[prop.alias].rawValue"
  />
</template>
<script>
export default {
  props: {
    id: {
      type: String,
      required: true
    },
    prop: {
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
