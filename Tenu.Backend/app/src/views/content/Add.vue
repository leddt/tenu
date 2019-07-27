<template>
  <form @submit.prevent="handleSubmit()">
    <tenu-view>
      <template #toolbar>
        <tenu-toolbar>
          <h1>Add content</h1>
          <div>
            <tenu-button primary submit>Create</tenu-button>
          </div>
        </tenu-toolbar>
      </template>

      <div>
        <div v-if="!contentTypes"><em>loading...</em></div>
        <div v-if="contentTypes">
          <tenu-field
            label="Content name"
            hint="The name for this piece of content"
            v-slot="{ id }"
          >
            <tenu-textbox
              :id="id"
              v-model="content.name"
              placeholder="Content name"
              required
            />
          </tenu-field>

          <tenu-field
            label="Content type"
            hint="The content type determines which properties and templates are available for this piece of content"
            v-slot="{ id }"
          >
            <tenu-dropdown
              :id="id"
              v-model="content.contentTypeAlias"
              placeholder="Select content type"
              required
              :choices="contentTypes"
              :choice-value="x => x.alias"
              :choice-label="x => x.name"
              @input="contentTypeChanged"
            />
          </tenu-field>
        </div>

        <div v-if="contentType">
          <content-editor
            :contentType="contentType"
            :content="content"
            class="mt-4"
          />
        </div>
      </div>
    </tenu-view>
  </form>
</template>
<script>
import api from "@/services/api";
import ContentTypesService from "@/services/content-types-service";
import ContentEditor from "@/components/content/ContentEditor";

export default {
  components: {
    ContentEditor
  },
  data() {
    return {
      content: this.getNewContent(this.$route),
      contentTypes: null
    };
  },
  computed: {
    contentType() {
      if (!this.contentTypes) return null;

      return this.contentTypes.find(
        x => x.alias === this.content.contentTypeAlias
      );
    }
  },
  mounted() {
    ContentTypesService.getAll()
      .then(result => (this.contentTypes = result))
      .catch(err => console.log(err));
  },
  beforeRouteUpdate(to, from, next) {
    this.content = this.getNewContent(to);
    next();
  },
  methods: {
    getNewContent(route) {
      return {
        parentId: route.query.parent,
        properties: {}
      };
    },
    contentTypeChanged() {
      for (let prop of this.contentType.properties) {
        if (!this.content.properties[prop.alias]) {
          this.content.properties = {
            ...this.content.properties,
            [prop.alias]: {
              propertyTypeAlias: prop.propertyTypeAlias,
              rawValue: ""
            }
          };
        }
      }
    },
    async handleSubmit() {
      let newContent = await api.post("content", this.content);
      this.$router.push(`/content/${newContent.id}`);
    }
  }
};
</script>
