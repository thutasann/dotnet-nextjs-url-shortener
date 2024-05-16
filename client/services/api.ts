import axios from 'axios'

const axiosInstance = axios.create({
  baseURL: 'http://localhost:5255/api',
})

export default axiosInstance
