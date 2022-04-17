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

  // NOTE Alright this is a bunch of confusing logic that even started to get me questioning my sanity by the end of it.  I'm trying to accomplish gathering all the account's vault-keeps and replacing the keep in the Appstate with a keepViewModel so it has a vaultKeep Id on it to make for easy checking on the front end.  We'll see... YEAH IT WORKS
  async getMyVaultsKeeps() {
    AppState.myVaults.forEach(async v => {
      const res = await api.get(`api/vaults/${v.id}/keeps`)
      logger.log('[getMyVaultsKeeps]', res.data)
      res.data.forEach(vk => {
        const found = AppState.keeps.find(fv => fv.id == vk.id)
        const index = AppState.keeps.findIndex(k => k.id == found.id)
        AppState.keeps.splice(index, 1, vk)
      })
    })
  }
  async createVaultKeep(vaultKeepData) {
    const res = await api.post(`api/vaultkeeps`, vaultKeepData)
    logger.log('[createVaultKeep]', res.data)
    AppState.vaultKeeps.push(res.data)
  }
}
export const vaultsService = new VaultsService()
