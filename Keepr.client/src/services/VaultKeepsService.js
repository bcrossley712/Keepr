import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
  async getVaultsKeeps(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log('[getVaultsKeeps]', res.data)
    AppState.keeps = res.data
  }
  async createVaultKeep(vaultKeepData) {
    const res = await api.post(`api/vaultkeeps`, vaultKeepData)
    logger.log('[createVaultKeep]', res.data)
    AppState.vaultKeeps.push(res.data)
  }
  // NOTE I'm trying to accomplish gathering all the account's vault-keeps and replacing the keep in the Appstate with a keepViewModel so it has a vaultKeep Id on it to make for easy checking on the front end.  We'll see... it works but why is is needed? I don't think it is. Time to rework it.
  async getAllMyVaultsKeeps() {
    AppState.myVaults.forEach(async v => {
      const res = await api.get(`api/vaults/${v.id}/keeps`)
      // logger.log('[getMyVaultsKeeps]', res.data)
      res.data.forEach(vk => {
        const found = AppState.keeps.find(fv => fv.id == vk.id)
        if (found) {
          const index = AppState.keeps.findIndex(k => k.id == found.id)
          AppState.keeps.splice(index, 1, vk)
        }
      })
    })
  }
  async deleteVaultKeep(keepId) {
    const found = AppState.keeps.find(k => k.id = keepId)
    debugger
    logger.log(found.vaultKeepId)
    if (found) {
      const res = await api.delete(`api/vaultkeeps/${found.vaultKeepId}`)
      logger.log('[deleteVaultKeep]', res.data)
      AppState.keeps = AppState.keeps.filter(k => k.id != keepId)
    }
  }
}
export const vaultKeepsService = new VaultKeepsService()