<template>
  <button
    class="py-1 rounded active:shadow-outline focus:outline-none transition"
    :class="{
      'px-2': !noPad,
      [`hover:bg-${baseColor}-400 active:bg-${baseColor}-500`]: true,
      [`bg-${baseColor}-200 border border-${baseColor}-600 hover:shadow`]: !flat
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
    },
    noPad: {
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
