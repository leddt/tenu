import Vue from "vue";
import api from "@/services/api";

function contentToNode(content) {
  return {
    id: content.id,
    parentId: content.parentId,
    name: content.name,
    expanded: false,
    children: null
  };
}

function registerNodes(state, nodes) {
  for (let node of nodes) {
    Vue.set(state.nodeById, node.id, node);
  }
}

export default {
  namespaced: true,

  state: {
    rootNodes: null,
    nodeById: {}
  },

  mutations: {
    rootNodesReceived(state, rootNodes) {
      state.rootNodes = rootNodes;
      registerNodes(state, rootNodes);
    },

    childrenReceived(state, { parentId, children }) {
      const parent = state.nodeById[parentId];
      if (parent) {
        parent.children = children;
      }

      registerNodes(state, children);
    },

    nodeExpandedChanged(state, { nodeId, expanded }) {
      state.nodeById[nodeId].expanded = expanded;
    },

    nodeNameChanged(state, { nodeId, name }) {
      if (!state.nodeById[nodeId]) return;
      state.nodeById[nodeId].name = name;
    },

    nodeRemoved(state, nodeId) {
      Vue.set(state.nodeById, nodeId, undefined);
    }
  },

  actions: {
    async loadRootNodes({ commit }) {
      const results = await api.get("content");

      commit("rootNodesReceived", results.map(contentToNode));
    },

    async refreshChildren({ commit }, parentId) {
      const results = await api.get(`content/${parentId}/children`);

      commit("childrenReceived", {
        parentId,
        children: results.map(contentToNode)
      });
    },

    toggleNode({ state, commit, dispatch }, nodeId) {
      const node = state.nodeById[nodeId];
      if (!node) return;

      const expanded = !node.expanded;
      commit("nodeExpandedChanged", { nodeId, expanded });

      if (expanded && node.children === null) {
        dispatch("refreshChildren", nodeId);
      }
    },

    updateNode({ commit }, content) {
      commit("nodeNameChanged", { nodeId: content.id, name: content.name });
    },

    refreshParent({ state, dispatch }, parentId) {
      if (parentId == null) {
        dispatch("loadRootNodes");
      } else {
        const parentNode = state.nodeById[parentId];
        if (parentNode) {
          if (parentNode.expanded) {
            dispatch("refreshChildren", parentId);
          } else {
            dispatch("toggleNode", parentId);
          }
        }
      }
    },

    removeNode({ state, commit, dispatch }, nodeId) {
      const node = state.nodeById[nodeId];
      if (!node) return;

      commit("nodeRemoved", nodeId);
      dispatch("refreshParent", node.parentId);
    }
  },

  getters: {}
};
