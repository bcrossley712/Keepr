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
  async getProfilesKeeps(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log('[getProfilesKeeps]', res.data)
    AppState.keeps = res.data
  }
  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)
    logger.log('[createKeep]', res.data)
    AppState.keeps.push(res.data)
  }
  async deleteKeep(keepId) {
    const res = await api.delete(`api/keeps/${keepId}`)
    logger.log('[deleteKeep]', res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id != keepId)
  }
}
export const keepsService = new KeepsService()

