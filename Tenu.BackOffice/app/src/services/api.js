import axios from "axios";

export default {
  async get(url) {
    const response = await axios.get(`/admin/api/${url}`);
    return response.data;
  },
  async post(url, data) {
    const response = await axios.post(`/admin/api/${url}`, data);
    return response.data;
  },
  async put(url, data) {
    const response = await axios.put(`/admin/api/${url}`, data);
    return response.data;
  },
  async delete(url) {
    const response = await axios.delete(`/admin/api/${url}`);
    return response.data;
  }
};
