<template>
  <div v-if="active"><slot /></div>
</template>
<script>
export default {
  props: {
    title: {
      type: String,
      required: true
    }
  },
  computed: {
    active() {
      return this.$parent.current === this;
    }
  },
  created() {
    this.$parent.register(this);
  },
  destroyed() {
    this.$parent.unregister(this);
  }
};
</script>
