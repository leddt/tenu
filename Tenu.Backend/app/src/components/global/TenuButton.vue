<template>
  <button
    class="py-1 px-2 rounded active:shadow-outline focus:outline-none transition"
    :class="{
      [`hover:bg-${baseColor}-400 active:bg-${baseColor}-500`]: true,
      [`bg-${baseColor}-200 border border-gray-600 hover:shadow`]: !flat
    }"
    @click="$emit('click')"
  >
    <slot />
  </button>
</template>
<script>
export default {
  props: {
    flat: {
      type: Boolean
    },
    primary: {
      type: Boolean
    }
  },
  computed: {
    baseColor() {
      return this.primary ? "green" : "gray";
    }
  }
};
</script>
