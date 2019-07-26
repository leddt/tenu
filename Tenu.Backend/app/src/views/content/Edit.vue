<template>
  <form @submit.prevent="updateContent">
    <tenu-view>
      <template #toolbar>
        <tenu-toolbar>
          <h1>Edit content</h1>
          <div>
            <tenu-button primary>Update</tenu-button>
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
  </form>
</template>
<script>
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
    async loadNode(id) {
      this.content = null;
      this.content = await api.get(`content/${id}`);
      this.contentType = await api.get(
        `contentTypes/${this.content.contentTypeAlias}`
      );
    },
    updateContent() {
      api.put(`content/${this.content.id}`, this.content);
    }
  }
};
</script>
