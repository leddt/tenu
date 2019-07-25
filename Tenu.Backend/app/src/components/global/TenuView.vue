<template>
  <div class="flex flex-col h-full">
    <slot name="toolbar" />
    <div class="flex-1 p-4 overflow-auto">
      <slot />
    </div>
  </div>
</template>
