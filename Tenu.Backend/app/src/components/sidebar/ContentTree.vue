<template>
  <tenu-view>
    <template v-slot:toolbar>
      <tenu-toolbar>
        <tenu-link-button to="/content/add">
          ➕ Add content
        </tenu-link-button>
        <tenu-button flat @click="loadRootNodes">
          🔄
        </tenu-button>
      </tenu-toolbar>
    </template>

    <ul v-if="rootNodes !== null">
      <content-node v-for="node in rootNodes" :key="node.id" :node="node" />
    </ul>
  </tenu-view>
</template>
<script>
import { mapState, mapActions } from "vuex";
import ContentNode from "@/components/sidebar/ContentNode";

export default {
  components: {
    ContentNode
  },
  computed: {
    ...mapState("contentTree", ["rootNodes"])
  },
  mounted() {
    this.loadRootNodes();
  },
  methods: {
    ...mapActions("contentTree", ["loadRootNodes"])
  }
};
</script>
