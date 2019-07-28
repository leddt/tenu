<template>
  <select
    :id="id"
    :value="value"
    :required="required"
    @change="update"
    class="shadow-inner border rounded px-2 py-1 focus:shadow-outline"
  >
    <option v-if="placeholder || !required" :disabled="required" value="">
      {{ placeholder }}
    </option>
    <option
      v-for="choice in choices"
      :key="getValue(choice)"
      :value="getValue(choice)"
    >
      {{ choiceLabel(choice) }}
    </option>
  </select>
</template>
<script>
export default {
  props: {
    value: {},
    id: {
      type: String
    },
    required: {
      type: Boolean
    },
    placeholder: {
      type: String
    },
    choices: {
      type: Array,
      required: true
    },
    choiceValue: {
      type: Function
    },
    choiceLabel: {
      type: Function
    }
  },
  methods: {
    getLabel(choice) {
      if (this.choiceLabel) return this.choiceLabel(choice);
      return choice.toString();
    },
    getValue(choice) {
      if (this.choiceValue) return this.choiceValue(choice);
      return choice;
    },
    update(ev) {
      this.$emit("input", ev.target.value);
    }
  }
};
</script>
