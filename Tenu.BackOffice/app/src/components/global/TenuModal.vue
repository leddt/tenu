<template>
  <transition name="fade">
    <div
      v-if="visible"
      class="fixed top-0 right-0 bottom-0 left-0 z-50 flex items-center justify-center"
      style="background-color: #0006"
      @click.self="dismiss"
    >
      <div
        class="bg-white rounded-lg shadow-2xl m-8 max-w-3xl max-h-full flex flex-col"
      >
        <div class="border-b p-4 text-xl">
          {{ title }}
        </div>
        <div class="p-4 flex-1 overflow-auto">
          <slot></slot>
        </div>
        <div class="border-t p-4 text-right">
          <slot name="actions">
            <tenu-button @click="dismiss">OK</tenu-button>
          </slot>
        </div>
      </div>
    </div>
  </transition>
</template>
<script>
export default {
  props: {
    title: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      visible: false
    };
  },
  methods: {
    show() {
      this.visible = true;
    },
    dismiss() {
      this.visible = false;
    }
  }
};
</script>
<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: all 0.2s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
