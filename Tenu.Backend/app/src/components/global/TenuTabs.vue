<template>
  <div>
    <ul class="flex flex-row">
      <template v-for="tab in tabs">
        <li :key="tab.title" class="border-b border-gray-500 w-2"></li>
        <li
          :key="tab.title"
          class="border border-gray-500 rounded-t"
          :class="{ 'border-b-0': tab.active, 'bg-gray-400': !tab.active }"
        >
          <span
            v-if="tab.active"
            class="inline-block px-2 py-1 cursor-default"
            >{{ tab.title }}</span
          >
          <button v-if="!tab.active" @click="select(tab)" class="px-2 py-1">
            {{ tab.title }}
          </button>
        </li>
      </template>
      <li class="border-b border-gray-500 flex-1"></li>
    </ul>
    <slot />
  </div>
</template>
<script>
export default {
  data() {
    return {
      tabs: [],
      current: null
    };
  },
  methods: {
    select(tab) {
      this.current = tab;
    },
    register(tab) {
      this.tabs.push(tab);

      if (this.tabs.length === 1) {
        this.select(tab);
      }
    },
    unregister(tab) {
      this.tabs = this.tabs.filter(t => t !== tab);
      if (this.current === tab) {
        this.current = this.tabs[0];
      }
    }
  }
};
</script>
