import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async getProfile(profileId) {
    const res = await api.get(`api/profiles/${profileId}`)
    logger.log('[getProfile]', res.data)
    AppState.activeProfile = res.data
  }
}

export const accountService = new AccountService()
