<template>
  <tenu-view>
    <template v-slot:toolbar>
      <tenu-toolbar>
        <tenu-link-button to="/content/add">
          ➕ Add content
        </tenu-link-button>
        <tenu-button flat @click="refresh">
          🔄
        </tenu-button>
      </tenu-toolbar>
    </template>

    <ul v-if="rootNodes !== null">
      <content-node v-for="node in rootNodes" :key="node.id" :content="node" />
    </ul>
  </tenu-view>
</template>
<script>
import api from "@/services/api";
import ContentNode from "@/components/sidebar/ContentNode";

export default {
  components: {
    ContentNode
  },
  data() {
    return {
      rootNodes: null
    };
  },
  mounted() {
    this.refresh();
  },
  methods: {
    refresh() {
      this.rootNodes = null;
      return api.get("content").then(rootNodes => {
        this.rootNodes = rootNodes;
      });
    }
  }
};
</script>
