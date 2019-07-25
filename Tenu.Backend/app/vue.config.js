module.exports = {
  productionSourceMap: false,
  publicPath: "/admin/",
  chainWebpack: config => {
    config.plugins.delete("hmr");
  }
};
