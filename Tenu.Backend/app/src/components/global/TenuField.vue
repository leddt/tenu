<template>
  <div class="flex flex-col py-4 border-b">
    <label :for="id">{{ label }}</label>
    <div class="my-1 flex flex-col"><slot :id="id" /></div>
    <p v-if="hint" class="opacity-50">{{ hint }}</p>
  </div>
</template>
<script>
let counter = 0;

export default {
  props: {
    label: {
      type: String
    },
    hint: {
      type: String
    }
  },
  data() {
    return {
      id: `field-${++counter}`
    };
  }
};
</script>
