<template>
  <input
    type="text"
    :value="value"
    :placeholder="placeholder"
    :required="required"
    @input="update"
    class="shadow-inner border rounded px-2 py-1 focus:shadow-outline"
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
    },
    required: {
      type: Boolean
    }
  },
  methods: {
    update(ev) {
      this.$emit("input", ev.target.value);
    }
  }
};
</script>
