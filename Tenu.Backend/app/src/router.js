import Vue from "vue";
import Router from "vue-router";

import StubView from "@/views/StubView";

import ContentHome from "@/views/content/Home";
import ContentAdd from "@/views/content/Add";
import ContentEdit from "@/views/content/Edit";
import ContentTree from "@/components/sidebar/ContentTree";

import MediaHome from "@/views/media/Home";

Vue.use(Router);

export default new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "home",
      redirect: "/content"
    },
    {
      path: "/content",
      components: {
        default: StubView,
        sidebar: ContentTree
      },
      children: [
        {
          path: "",
          component: ContentHome
        },
        {
          path: "add",
          component: ContentAdd
        },
        {
          path: ":id",
          name: "content-edit",
          components: {
            default: ContentEdit
          }
        }
      ]
    },
    {
      path: "/media",
      components: {
        default: StubView
      },
      children: [
        {
          path: "",
          component: MediaHome
        }
      ]
    }

    // {
    //   path: "/about",
    //   name: "about",
    //   // route level code-splitting
    //   // this generates a separate chunk (about.[hash].js) for this route
    //   // which is lazy-loaded when the route is visited.
    //   component: () =>
    //     import(/* webpackChunkName: "about" */ "./views/About.vue")
    // }
  ]
});
