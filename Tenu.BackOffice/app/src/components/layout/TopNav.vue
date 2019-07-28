<template>
  <nav class="px-6 py-1 bg-gray-700 text-white flex items-center">
    <router-link to="/" class="text-4xl font-hairline no-underline">
      Tenu
    </router-link>
    <div class="ml-16 text-gray-400">
      <router-link
        to="/content"
        class="mx-4 p-2 no-underline rounded text-lg hover:bg-gray-900"
        active-class="font-semibold text-white bg-gray-900"
      >
        Content
      </router-link>
      <router-link
        to="/media"
        class="mx-4 p-2 no-underline rounded text-lg hover:bg-gray-900"
        active-class="font-semibold text-white bg-gray-900"
      >
        Media
      </router-link>
    </div>
  </nav>
</template>
