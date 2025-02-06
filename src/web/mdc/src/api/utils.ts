import axios from 'axios'

axios.defaults.baseURL = 'http://remote.championimage.com.cn:5279/MDC/'

export const get = async (suffix: string) => {
  return axios.get(suffix).then(
    (response) => {
      console.log(response.data)
      return response.data
    },
    (err) => {
      console.log(`异常:${err.message}`)
    }
  )
}

export const post = (suffix: string, param: any) => {
  return axios.post(suffix, param).then(
    (response) => {
      console.log(response.data)
    },
    (err) => {
      console.log(`异常:${err.message}`)
    }
  )
}
