<template>
  <li>
    <div class="flex items-center">
      <span class="flex flex-1">
        <span class="w-4">
          <button @click="toggleNode(node.id)" class="w-full">
            <span v-if="!node.expanded">+</span>
            <span v-if="node.expanded">-</span>
          </button>
        </span>
        <router-link :to="{ name: 'content-edit', params: { id: node.id } }">
          {{ node.name }}
        </router-link>
      </span>
      <span>
        <tenu-button
          flat
          v-if="node.expanded"
          @click="refreshChildren(node.id)"
        >
          🔄
        </tenu-button>
        <tenu-link-button flat :to="`/content/add?parent=${node.id}`">
          ➕
        </tenu-link-button>
      </span>
    </div>
    <ul v-if="node.expanded" class="ml-4">
      <li v-if="!node.children"><em>loading...</em></li>
      <li v-if="node.children && node.children.length === 0"><em>empty</em></li>
      <content-node
        v-for="child in node.children || []"
        :key="child.id"
        :node="child"
      ></content-node>
    </ul>
  </li>
</template>
<script>
import { mapActions } from "vuex";

export default {
  name: "content-node",
  props: {
    node: {
      type: Object,
      required: true
    }
  },
  methods: {
    ...mapActions("contentTree", ["toggleNode", "refreshChildren"])
  }
};
</script>
