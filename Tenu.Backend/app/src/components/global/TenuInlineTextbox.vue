<template>
  <input
    type="text"
    :value="value"
    :placeholder="placeholder"
    @input="update"
    class="border border-dashed hover:border-gray-600 focus:border-solid focus:border-gray-600 focus:shadow-inner bg-transparent focus:bg-white px-1 transition"
  />
</template>
<script>
export default {
  props: {
    value: {
      type: String
    },
    placeholder: {
      type: String
    }
  },
  methods: {
    update(ev) {
      this.$emit("input", ev.target.value);
    }
  }
};
</script>
