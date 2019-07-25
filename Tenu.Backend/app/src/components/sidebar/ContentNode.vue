<template>
  <li>
    <div class="flex items-center">
      <span class="flex flex-1">
        <span class="w-4">
          <button @click="toggle" class="w-full">
            <span v-if="collapsed">+</span>
            <span v-if="!collapsed">-</span>
          </button>
        </span>
        <router-link
          :to="{ name: 'content-edit', params: { id: this.content.id } }"
        >
          {{ this.content.name }}
        </router-link>
      </span>
      <span>
        <tenu-button flat v-if="!collapsed" @click="refresh">
          🔄
        </tenu-button>
        <tenu-link-button flat :to="`/content/add?parent=${this.content.id}`">
          ➕
        </tenu-link-button>
      </span>
    </div>
    <ul v-if="!collapsed" class="ml-4">
      <li v-if="!children"><em>loading...</em></li>
      <li v-if="children && children.length === 0"><em>empty</em></li>
      <content-node
        v-for="child in children || []"
        :key="child.id"
        :content="child"
      ></content-node>
    </ul>
  </li>
</template>
<script>
import api from "@/services/api";

export default {
  name: "content-node",
  props: {
    content: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      collapsed: true,
      children: null
    };
  },
  methods: {
    addChild() {
      let name = prompt("Node name");
      if (!name) return;

      return api
        .post("content", { name, parentId: this.content.id })
        .then(() => this.refresh());
    },
    refresh() {
      this.children = null;
      return api.get(`content/${this.content.id}/children`).then(children => {
        this.children = children;
        this.collapsed = false;
      });
    },
    toggle() {
      this.collapsed = !this.collapsed;
      if (!this.collapsed && !this.children) {
        return this.refresh();
      }
    }
  }
};
</script>
