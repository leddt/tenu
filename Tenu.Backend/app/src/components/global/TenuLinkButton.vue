<template>
  <router-link
    :to="to"
    class="no-underline py-1 px-2 rounded active:shadow-outline focus:outline-none transition inline-block"
    :class="{
      [`hover:bg-${baseColor}-400 active:bg-${baseColor}-500`]: true,
      [`bg-${baseColor}-200 border border-gray-600 hover:shadow`]: !flat
    }"
  >
    <slot />
  </router-link>
</template>
<script>
export default {
  props: {
    to: {
      required: true
    },
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
