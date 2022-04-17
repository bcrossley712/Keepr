import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
  async getAccountVaults() {
    const res = await api.get('account/vaults')
    logger.log('[getAccountVaults]', res.data)
    AppState.myVaults = res.data
  }
  async getProfilesVaults(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log('[getProfilesVaults]', res.data)
    AppState.vaults = res.data
  }
  async createVaultKeep(vaultKeepData) {
    const res = await api.post(`api/vaultkeeps`, vaultKeepData)
    logger.log('[createVaultKeep]', res.data)
    AppState.vaultKeeps.push(res.data)
  }
}
export const vaultsService = new VaultsService()
