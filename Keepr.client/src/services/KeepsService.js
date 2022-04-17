import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps')
    logger.log('[getKeeps]', res.data)
    AppState.keeps = res.data
  }
  async getKeepById(id) {
    const res = await api.get(`api/keeps/${id}`)
    logger.log('[getKeepById]', res.data)
    AppState.activeKeep = res.data
  }
}
export const keepsService = new KeepsService()

