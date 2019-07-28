<template>
  <div>
    <ul>
      <li v-if="value.length === 0" class="my-1"><em>The list is empty</em></li>
      <li v-for="(item, idx) in value" :key="idx" class="my-1 flex flex-row">
        <component
          :is="of"
          v-bind="withProps"
          v-model="value[idx]"
          class="flex-1"
        />
        <tenu-button flat @click="removeItem(idx)">❌</tenu-button>
      </li>
    </ul>
    <tenu-button @click="addItem">Add</tenu-button>
  </div>
</template>
<script>
export default {
  props: {
    of: { required: true },
    withProps: {
      type: Object
    },
    value: {
      type: Array,
      required: true
    },
    template: {
      type: Function,
      required: true
    }
  },
  methods: {
    addItem() {
      this.$emit("input", [...this.value, this.template()]);
    },
    removeItem(idx) {
      this.$emit("input", [...this.value.filter((_, i) => i !== idx)]);
    }
  }
};
</script>
