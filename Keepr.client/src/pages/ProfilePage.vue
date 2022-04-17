<template>
  <div class="container-fluid">
    <div class="row px-md-3 my-4">
      <div class="col-12 px-md-3 mt-4">
        <img :src="profile.picture" alt="" />
      </div>
    </div>
    <h2 class="mb-0 mt-2 px-3">
      Vaults <i class="mdi mdi-plus text-primary"></i>
    </h2>
    <div v-if="account.id == route.params.id" class="px-md-3 px-sm-1 masonry">
      <div v-for="v in myVaults" :key="v.id" class="">
        <Vault :vault="v" />
      </div>
    </div>
    <div v-else class="px-md-3 px-sm-1 masonry">
      <div v-for="v in vaults" :key="v.id" class="">
        <Vault :vault="v" />
      </div>
    </div>
    <h2 class="mb-0 mt-2 px-3">
      Keeps <i class="mdi mdi-plus text-primary"></i>
    </h2>
    <div class="px-md-3 px-sm-1 masonry">
      <div v-for="k in keeps" :key="k.id" class="">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { useRoute } from "vue-router"
import { vaultsService } from "../services/VaultsService"
import { accountService } from "../services/AccountService"
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await accountService.getProfile(route.params.id)
        await keepsService.getProfilesKeeps(route.params.id)
        await vaultsService.getProfilesVaults(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      route,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      myVaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.keeps),
      profile: computed(() => AppState.activeProfile)
    }
  }
}
</script>

<style scoped lang="scss">
img {
  max-width: 100px;
}
.masonry {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>
