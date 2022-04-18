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
  async getById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    logger.log('[getVaultById]', res.data)
    AppState.activeVault = res.data
    return res.data
  }
  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    logger.log('[createVault]', res.data)
    AppState.myVaults.push(res.data)
  }
  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    logger.log('[deleteVault]', res.data)
    AppState.myVaults = AppState.myVaults.filter(v => v.id != vaultId)
    AppState.activeVault = {}
  }
}
export const vaultsService = new VaultsService()
