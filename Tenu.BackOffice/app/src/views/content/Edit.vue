<template>
  <form @submit.prevent="updateContent">
    <tenu-view>
      <template #toolbar>
        <tenu-toolbar>
          <h1>Edit content</h1>
          <div class="flex flex-row">
            <tenu-button danger @click="$refs.deleteModal.show()" class="mr-2">
              Delete
            </tenu-button>
            <tenu-button primary submit>
              Update
            </tenu-button>
          </div>
        </tenu-toolbar>
      </template>
      <div>
        <div v-if="!content"><em>loading...</em></div>

        <div v-if="content">
          <div class="flex flex-col">
            <tenu-inline-textbox
              v-model="content.name"
              class="text-xl"
              required
              placeholder="Content name"
            />
          </div>

          <content-editor
            v-if="contentType"
            :content="content"
            :contentType="contentType"
            class="mt-4"
          />
        </div>
      </div>
    </tenu-view>

    <tenu-modal ref="deleteModal" title="Delete content">
      <p>Are you sure that you want to delete this content?</p>
      <p><strong>Warning:</strong> This action cannot be undone.</p>

      <template #actions>
        <tenu-button @click="$refs.deleteModal.dismiss()" class="mr-2">
          Cancel
        </tenu-button>
        <tenu-button danger @click="deleteContent">
          Delete
        </tenu-button>
      </template>
    </tenu-modal>
  </form>
</template>
<script>
import { mapActions } from "vuex";
import api from "@/services/api";
import ContentEditor from "@/components/content/ContentEditor";

export default {
  components: { ContentEditor },
  data() {
    return {
      content: null,
      contentType: null
    };
  },
  mounted() {
    this.loadNode(this.$route.params.id);
  },
  beforeRouteUpdate(to, from, next) {
    this.loadNode(to.params.id);
    next();
  },
  methods: {
    ...mapActions("contentTree", ["updateNode", "removeNode"]),

    async loadNode(id) {
      this.content = null;
      this.content = await api.get(`content/${id}`);
      this.contentType = await api.get(
        `contentTypes/${this.content.contentTypeAlias}`
      );
    },
    async updateContent() {
      this.content = await api.put(`content/${this.content.id}`, this.content);
      this.updateNode(this.content);
    },
    async deleteContent() {
      await api.delete(`content/${this.content.id}`);
      this.removeNode(this.content.id);
      this.$router.push("/content");
    }
  }
};
</script>
