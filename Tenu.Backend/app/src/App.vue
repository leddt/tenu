<template>
  <div>
    <top-nav />

    <multipane
      :style="{ height: this.mainHeight + 'px' }"
      @paneResizeStop="saveSidebarWidth"
    >
      <div
        class="bg-gray-200 overflow-auto border-r border-gray-500"
        style="min-width: 250px; max-width: 50%"
        :style="{ width: sidebarWidth }"
      >
        <router-view name="sidebar" />
      </div>
      <multipane-resizer />
      <div ref="main" class="flex-1 bg-gray-100">
        <router-view />
      </div>
    </multipane>
  </div>
</template>

<script>
import { Multipane, MultipaneResizer } from "vue-multipane";
import TopNav from "@/components/layout/TopNav";

const SIDEBAR_WIDTH = "tenu:layout:sidebar";

export default {
  components: { Multipane, MultipaneResizer, TopNav },
  data() {
    return {
      mainTop: 0,
      mainHeight: 0,
      contentHeight: 0,
      sidebarWidth: localStorage[SIDEBAR_WIDTH] || "400px"
    };
  },
  methods: {
    onResize() {
      this.mainHeight = window.innerHeight - this.$refs.main.offsetTop;
    },
    saveSidebarWidth(sidebar, handle, width) {
      localStorage[SIDEBAR_WIDTH] = width;
    }
  },
  mounted() {
    this.onResize();
    window.addEventListener("resize", this.onResize);
  },
  destroyed() {
    window.removeEventListener("resize", this.onResize);
  }
};
</script>
