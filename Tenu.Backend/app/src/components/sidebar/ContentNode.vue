<template>
  <li>
    <div class="flex items-center hover:bg-gray-100 rounded">
      <tenu-button flat no-pad @click="toggleNode(node.id)" class="w-4">
        <span v-if="!node.expanded">+</span>
        <span v-if="node.expanded">-</span>
      </tenu-button>
      <router-link
        :to="{ name: 'content-edit', params: { id: node.id } }"
        class="truncate flex-1 mx-1 no-underline"
        active-class="font-bold"
      >
        {{ node.name }}
      </router-link>
      <div>
        <tenu-button
          flat
          no-pad
          v-if="node.expanded"
          @click="refreshChildren(node.id)"
        >
          🔄
        </tenu-button>
        <tenu-link-button flat no-pad :to="`/content/add?parent=${node.id}`">
          ➕
        </tenu-link-button>
      </div>
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
