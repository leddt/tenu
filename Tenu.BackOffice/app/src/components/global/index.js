import Vue from "vue";

const requireComponent = require.context(
  ".",
  true,
  /\.\/(editors\/)?Tenu[\w]+\.vue$/
);

requireComponent.keys().forEach(filename => {
  const componentConfig = requireComponent(filename);
  const componentName = filename
    .replace(/^\.\/(editors\/)?/, "")
    .replace(/\.\w+$/, "");

  Vue.component(componentName, componentConfig.default || componentConfig);
});
