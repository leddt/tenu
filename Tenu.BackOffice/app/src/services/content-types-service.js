import api from "./api";

let cachedContentTypes = null;

async function getAll(options = { cache: true }) {
  if (options.cache && cachedContentTypes)
    return Promise.resolve(cachedContentTypes);

  const result = await api.get("ContentTypes");
  cachedContentTypes = result;
  return result;
}

async function getByAlias(alias, { cache = true }) {
  if (cache && cachedContentTypes) {
    const result = cachedContentTypes.find(x => x.alias === alias);
    if (result) return result;
  }

  return await api.get(`ContentTypes/${alias}`);
}

export default {
  getAll,
  getByAlias
};
