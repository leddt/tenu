import api from "@/services/api";

function contentToNode(content) {
  return {
    id: content.id,
    name: content.name,
    expanded: false,
    children: null
  };
}

function registerNodes(state, nodes) {
  for (let node of nodes) {
    state.nodeById[node.id] = node;
  }
}

export default {
  namespaced: true,

  state: {
    rootNodes: null,
    nodeById: {}
  },

  mutations: {
    setRootNodes(state, rootNodes) {
      state.rootNodes = rootNodes;
      registerNodes(state, rootNodes);
    },

    setChildren(state, { parentId, children }) {
      const parent = state.nodeById[parentId];
      if (parent) {
        parent.children = children;
      }

      registerNodes(state, children);
    },

    setNodeExpanded(state, { nodeId, expanded }) {
      state.nodeById[nodeId].expanded = expanded;
    }
  },

  actions: {
    async loadRootNodes({ commit }) {
      const results = await api.get("content");

      commit("setRootNodes", results.map(contentToNode));
    },

    async refreshChildren({ commit }, parentId) {
      const results = await api.get(`content/${parentId}/children`);

      commit("setChildren", {
        parentId,
        children: results.map(contentToNode)
      });
    },

    toggleNode({ state, commit, dispatch }, nodeId) {
      const node = state.nodeById[nodeId];
      if (!node) return;

      const expanded = !node.expanded;
      commit("setNodeExpanded", { nodeId, expanded });

      if (expanded && node.children === null) {
        dispatch("refreshChildren", nodeId);
      }
    }
  },

  getters: {}
};
